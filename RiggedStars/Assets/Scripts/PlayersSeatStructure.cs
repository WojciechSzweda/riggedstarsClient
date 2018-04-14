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
            if (players.ContainsKey(playerIndex.ToString()))
                PlayerSeatSlots[seatIndex].CurrentSeat.SeatPlayer(players[playerIndex.ToString()]);
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
}
