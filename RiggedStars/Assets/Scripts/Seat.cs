using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Seat : MonoBehaviour {

    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI PlayerStackText;
    public PlayerInfo Player;
    [SerializeField]
    GameObject[] CardSlots;
    [SerializeField]
    Card cardPrefab;


    public void ShowCards(CardForm[] cards) {
        for (int i = 0; i < CardSlots.Length; i++) {
            var card = Instantiate<Card>(cardPrefab, CardSlots[i].transform);
            card.CreateCard(cards[i]);
        }
    }

    public void ClearCards() {
        for (int i = 0; i < CardSlots.Length; i++) {
            for (int j = 0; j < CardSlots[i].transform.childCount; j++) {
                Destroy(CardSlots[i].transform.GetChild(j).gameObject);
            }
        }
    }

    public void SeatPlayer(PlayerInfo player) {
        Player = player;
        PlayerNameText.SetText(Player.Name);
        PlayerStackText.SetText(Player.Stack.ToString());
    }

    public void RemovePlayer() {
        Player = null;
        PlayerNameText.SetText(string.Empty);
        PlayerStackText.SetText(string.Empty);
    }
}
