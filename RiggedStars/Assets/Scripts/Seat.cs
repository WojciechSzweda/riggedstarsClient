using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Seat : MonoBehaviour {

    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI PlayerStackText;
    public PlayerInfo Player;
    [SerializeField] GameObject[] CardSlots;
    [SerializeField] Card cardPrefab;
    [SerializeField] Image ActiveBorder;
    [SerializeField] Color ActiveBorderColor;
    [SerializeField] Bet BetPrefab;
    [SerializeField] TextMeshProUGUI StackField;
    Bet Bet;

    private Color activeBorderColorDefault;

    private void Start() {
        activeBorderColorDefault = ActiveBorder.color;
        if (transform.parent.name != "PlayerSeats")
            Bet = Instantiate<Bet>(BetPrefab, transform.parent.Find("BetHolder").transform);

    }

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

    public void SetStack(int stack) {
        Player.Stack = stack;
        PlayerStackText.SetText(stack.ToString());
        if (StackField != null)
            StackField.SetText(stack.ToString());
    }

    public void ChangeStack(int ammount) {
        SetStack(Player.Stack + ammount);
    }

    public void SetActiveBorder(bool active) {
        if (active) {
            ActiveBorder.color = ActiveBorderColor;
        }
        else {
            ActiveBorder.color = activeBorderColorDefault;
        }
    }

    public void SetBet(int ammount) {
        Bet.SetBetSize(ammount);
    }

    public void ClearBet() {
        Bet.ClearBet();
    }
}
