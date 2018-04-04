using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayersSeatStructure : MonoBehaviour {

    public Seat[] PlayerSeats;
    private int maxPlayers;

    private void Start() {
        maxPlayers = PlayerSeats.Length + 1;
    }

    public string GetClientIndex(Dictionary<string, PlayerInfo> players) {
        return players.FirstOrDefault(x => x.Value.ID == ClientInfo.ID).Key;
        //int clientIndex = 0;
        //for (int i = 0; i < maxPlayers; i++) {
        //    if (players.ContainsKey(i.ToString()))
        //        if (players[i.ToString()].ID == ClientInfo.ID) {
        //            clientIndex = i;
        //            break;
        //        }
        //}
        //return clientIndex;
    }

    public void FillSeatsWithPlayers(Dictionary<string, PlayerInfo> players) {

        int clientIndex = int.Parse(GetClientIndex(players));

        for (int i = clientIndex + 1; i < clientIndex + maxPlayers; i++) {
            int playerIndex = i % maxPlayers;
            int seatIndex = i - (clientIndex + 1);
            if (players.ContainsKey(playerIndex.ToString()))
                PlayerSeats[seatIndex].SeatPlayer(players[playerIndex.ToString()]);
            else {
                PlayerSeats[seatIndex].RemovePlayer();
            }
        }
    }
}
