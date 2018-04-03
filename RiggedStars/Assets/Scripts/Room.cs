using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Room : MonoBehaviour {


    int RoomID;

    [SerializeField]
    ChatManager Chat;
    [SerializeField]
    ClientListManager ClientList;
    [SerializeField]
    PlayerSeat PlayerSeatCards;
    [SerializeField]
    BetStage BettingStage;
    [SerializeField]
    TableCardsManager TableCards;

    private WebSocket webSocket;


    public void JoinRoom(int id) {
        RoomID = id;
        StartCoroutine(ConnectToWebSocket());
        Chat.OnSendMessage += SendMessageToWebSocket;
        BettingStage.OnClientAction += SendMessageToWebSocket;
        //TODO: better hub to room transition -> minimize to corner?
        FindObjectOfType<UIManager>().HideHUB();
    }

    public int GetRoomID() {
        return RoomID;
    }

    IEnumerator ConnectToWebSocket() {
        string ws = "ws://" + ServerConfig.getServerURL() + "/game?user=" + ClientInfo.ID.ToString() + "&roomId=" + RoomID.ToString();
        webSocket = new WebSocket(new Uri(ws));
        Debug.Log("Trying to connect to WebSocket: " + ws);
        yield return StartCoroutine(webSocket.Connect());
        Debug.Log("Connected to websocket: " + ws);
        while (true) {
            byte[] replyBytes = webSocket.Recv();
            if (replyBytes != null) {
                string reply = Encoding.ASCII.GetString(replyBytes);
                Debug.Log("Received: " + reply);
                var replyType = JsonConvert.DeserializeObject<WsForm>(reply);
                if (replyType.Type == "text") {
                    var replyMsg = JsonConvert.DeserializeObject<WsChatMessage>(reply);
                    Chat.MessageReceived(replyMsg.Name, replyMsg.Payload);
                }
                if (replyType.Type == "newUser") {
                    var replyMsg = JsonConvert.DeserializeObject<WsUserMessage>(reply);
                    ClientList.AddClient(replyMsg.Payload.ID, replyMsg.Payload.Name);
                }
                if (replyType.Type == "deleteUser") {
                    var replyMsg = JsonConvert.DeserializeObject<WsUserMessage>(reply);
                    ClientList.DeleteClient(replyMsg.Payload.ID);
                }
                if (replyType.Type == "ownCards") {
                    var replyMsg = JsonConvert.DeserializeObject<WsCardsForm>(reply);
                    PlayerSeatCards.OwnCards(replyMsg.Payload);
                }
                if (replyType.Type == "activePlayer") {
                    var replyMsg = JsonConvert.DeserializeObject<ActivePlayerForm>(reply);
                    //TODO: mark active player
                    //TODO: consider changing to id
                    if (replyMsg.Name == ClientInfo.Name) {
                        BettingStage.ActiveBet(replyMsg.MinBet);
                    }
                }
                if(replyType.Type == "tableCards") {
                    var replyMsg = JsonConvert.DeserializeObject<TableCardsForm>(reply);
                    TableCards.SetTableCards(replyMsg.Payload);
                }
                if(replyType.Type == "endRound") {
                    TableCards.ClearTable();
                    PlayerSeatCards.ClearCards();
                }
            }
            if (webSocket.error != null) {
                Debug.LogError("Error: " + webSocket.error);
                break;
            }
            yield return 0;
        }
        webSocket.Close();
    }

    public void SendMessageToWebSocket(string message) {
        webSocket.SendString(message);
    }

    private void OnApplicationQuit() {
        webSocket.Close();
    }

    public void LeaveRoom() {
        //TODO: leave -> minimize room
        webSocket.Close();
        FindObjectOfType<RoomManager>().DestroyRoom(RoomID);
    }
}


public class WsChatMessage {
    public string Type { get; set; }
    public string Name { get; set; }
    public string Payload { get; set; }
}



public class WsUserMessage {
    public ClientData Payload { get; set; }
}