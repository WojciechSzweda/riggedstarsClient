using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(WebSocketManager))]
public class Room : MonoBehaviour {




    [SerializeField] ChatManager Chat;
    [SerializeField] ClientListManager ClientList;
    [SerializeField] Seat PlayerSeatSlot;
    [SerializeField] BetStage BettingStage;
    [SerializeField] TableCardsManager TableCards;
    [SerializeField] PlayersSeatStructure[] PlayersSeatStructures;
    [SerializeField] Bet PlayerBetField;
    [SerializeField] PotManager Pot;
    [SerializeField] WinnerNotifier WinnerNotifier;

    private int RoomID;
    private PlayersSeatStructure CurrentSeatStructure;
    private WebSocketManager _WebSocketManager;

    public void JoinRoom(int id, int maxClients) {
        RoomID = id;
        string wsPath = "ws://" + ServerConfig.getServerURL() + "/game?user=" + ClientInfo.ID.ToString() + "&roomId=" + RoomID.ToString();
        _WebSocketManager = GetComponent<WebSocketManager>();
        _WebSocketManager.StartWebSocket(MakeWebsocketActions(), wsPath);
        Chat.OnSendMessage += _WebSocketManager.SendMessageToWebSocket;
        BettingStage.OnClientAction += _WebSocketManager.SendMessageToWebSocket;
        //TODO: better hub to room transition -> minimize to corner?
        FindObjectOfType<UIManager>().HideHUB();
        CreateSeats(maxClients);
        PlayerSeatSlot.SeatPlayer(new PlayerInfo { ID = ClientInfo.ID, Name = ClientInfo.Name });
    }

    void CreateSeats(int maxClients) {
        var mappedSeat = PlayersSeatStructures.FirstOrDefault(x => x.PlayerSeatSlots.Count() == maxClients - 1);
        if (mappedSeat != null)
            CurrentSeatStructure = Instantiate<PlayersSeatStructure>(mappedSeat, PlayerSeatSlot.transform.parent.transform);
        else {
            Debug.LogError("there is no seat structure with " + maxClients + "players");
        }
    }

    void ActivePlayerBorder(int id) {
        CurrentSeatStructure.ActivePlayerBorder(id);
        PlayerSeatSlot.SetActiveBorder(id == ClientInfo.ID);
    }

    Dictionary<string, Action<string>> MakeWebsocketActions() {
        var WebsocketActions = new Dictionary<string, Action<string>>();
        WebsocketActions.Add("text", (reply) => {
            var replyMsg = JsonConvert.DeserializeObject<WsChatMessage>(reply);
            Chat.MessageReceived(replyMsg.Name, replyMsg.Payload);
        });
        WebsocketActions.Add("newUser", (reply) => {
            var replyMsg = JsonConvert.DeserializeObject<WsUserMessage>(reply);
            ClientList.AddClient(replyMsg.Payload.ID, replyMsg.Payload.Name);
        });
        WebsocketActions.Add("deleteuser", (reply) => {
            var replyMsg = JsonConvert.DeserializeObject<WsUserMessage>(reply);
            ClientList.DeleteClient(replyMsg.Payload.ID);
        });
        WebsocketActions.Add("ownCards", (reply) => {
            var replyMsg = JsonConvert.DeserializeObject<WsCardsForm>(reply);
            PlayerSeatSlot.ShowCards(replyMsg.Payload);
        });
        WebsocketActions.Add("activePlayer", (reply) => {
            var replyMsg = JsonConvert.DeserializeObject<ActivePlayerForm>(reply);
            ActivePlayerBorder(replyMsg.ID);
            //TODO: consider changing to id
            if (replyMsg.Name == ClientInfo.Name) {
                BettingStage.ActiveBet(replyMsg.MinBet);
            }
        });
        WebsocketActions.Add("tableCards", (reply) => {
            var replyMsg = JsonConvert.DeserializeObject<TableCardsForm>(reply);
            TableCards.SetTableCards(replyMsg.Payload);
        });
        WebsocketActions.Add("startRound", (reply) => {
            Pot.ClearPot();
            var replyMsg = JsonConvert.DeserializeObject<StartRoundForm>(reply);
            CurrentSeatStructure.FillSeatsWithPlayers(replyMsg.Players);
            PlayerSeatSlot.SetStack(replyMsg.Players[CurrentSeatStructure.GetClientIndex(replyMsg.Players)].Stack);
            //TODO: button player mark
        });
        WebsocketActions.Add("endRound", (reply) => {
            TableCards.ClearTable();
            PlayerSeatSlot.ClearCards();
            var replyMsg = JsonConvert.DeserializeObject<EndRoundMessageForm>(reply);
            WinnerNotifier.ShowWinnerMessage(replyMsg.Winners);
            foreach (var winner in replyMsg.Winners) {
                if (winner.Winner.ID == ClientInfo.ID) {
                    PlayerSeatSlot.ChangeStack(winner.Ammount);
                }
                CurrentSeatStructure.SetClientBet(winner.Winner.ID, winner.Ammount);
            }

        });
        WebsocketActions.Add("bet", (reply) => {
            var replyMsg = JsonConvert.DeserializeObject<BetForm>(reply);
            if (replyMsg.ID == ClientInfo.ID) {
                PlayerBetField.SetBetSize(replyMsg.Ammount);
                PlayerSeatSlot.ChangeStack(-replyMsg.Ammount);
                return;
            }
            CurrentSeatStructure.SetClientBet(replyMsg.ID, replyMsg.Ammount);

        });
        WebsocketActions.Add("endBetStage", (reply) => {
            var replyMsg = JsonConvert.DeserializeObject<EndBetStageForm>(reply);
            PlayerBetField.ClearBet();
            CurrentSeatStructure.ClearAllBets();
            Pot.AddToPot(replyMsg.Pot);
        });
        WebsocketActions.Add("fold", (reply) => {
            //TODO: change color of folded player

        });
        return WebsocketActions;
    }


    public void LeaveRoom() {
        //TODO: leave -> minimize room
        _WebSocketManager.Close();
        FindObjectOfType<RoomManager>().DestroyRoom(RoomID);
    }
}


