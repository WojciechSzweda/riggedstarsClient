using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayersSeatStructure : MonoBehaviour {

    public PlayerSeatSlot[] PlayerSeatSlots;
    [SerializeField] Seat SeatPrefab;
    private int maxPlayers;

    private void Start() {
        maxPlayers = PlayerSeatSlots.Length + 1;
        foreach (var seat in PlayerSeatSlots) {
            seat.CurrentSeat = Instantiate<Seat>(SeatPrefab, seat.transform);
        }
    }

    public string GetClientIndex(Dictionary<string, PlayerInfo> players) {
        return players.FirstOrDefault(x => x.Value.ID == ClientInfo.ID).Key;
    }

    public void FillSeatsWithPlayers(Dictionary<string, PlayerInfo> players) {

        int clientIndex = int.Parse(GetClientIndex(players));

        for (int i = clientIndex + 1; i < clientIndex + maxPlayers; i++) {
            int playerIndex = i % maxPlayers;
            int seatIndex = i - (clientIndex + 1);
            if (players.ContainsKey(playerIndex.ToString())) {
                PlayerSeatSlots[seatIndex].CurrentSeat.SeatPlayer(players[playerIndex.ToString()]);
                PlayerSeatSlots[seatIndex].CurrentSeat.SetStack(players[playerIndex.ToString()].Stack);
            }
            else {
                PlayerSeatSlots[seatIndex].CurrentSeat.RemovePlayer();
            }
        }
    }

    public void ActivePlayerBorder(int id) {
        foreach (var slot in PlayerSeatSlots) {
            if (slot.CurrentSeat.Player != null) {
                slot.CurrentSeat.SetActiveBorder(false);
            }
        }
        var currentPlayer = PlayerSeatSlots.Where(x => x.CurrentSeat.Player != null).FirstOrDefault(x => x.CurrentSeat.Player.ID == id);
        if (currentPlayer != null) {
            currentPlayer.CurrentSeat.SetActiveBorder(true);
        }
    }

    public void SetClientBet(int id, int ammount) {
        var clientSeat = PlayerSeatSlots.Where(x => x.CurrentSeat.Player != null)
            .FirstOrDefault(x => x.CurrentSeat.Player.ID == id)
            .CurrentSeat;
        clientSeat.SetBet(ammount);
        clientSeat.ChangeStack(-ammount);

    }

    public void ClearAllBets() {
        foreach (var slot in PlayerSeatSlots) {
            if (slot.CurrentSeat.Player != null) {
                slot.CurrentSeat.ClearBet();
            }
        }
    }

    public void ShowClientCards(int id, CardForm[] cards) {
        var clientSeat = PlayerSeatSlots.Where(x => x.CurrentSeat.Player != null)
            .FirstOrDefault(x => x.CurrentSeat.Player.ID == id).CurrentSeat;
        clientSeat.ShowCards(cards);
    }

    public void ClearClientsCards() {
        foreach (var slot in PlayerSeatSlots) {
            if (slot.CurrentSeat.Player != null) {
                slot.CurrentSeat.ClearCards();
            }
        }
    }

    public void SetClientFoldCue(int id) {
        var clientSeat = PlayerSeatSlots.Where(x => x.CurrentSeat.Player != null)
            .FirstOrDefault(x => x.CurrentSeat.Player.ID == id).CurrentSeat;
        clientSeat.FoldCue();
    }

    public void DefaultActiveBorder() {
        foreach (var slot in PlayerSeatSlots) {
            if (slot.CurrentSeat.Player != null) {
                slot.CurrentSeat.SetActiveBorder(false);
            }
        }
    }
}
