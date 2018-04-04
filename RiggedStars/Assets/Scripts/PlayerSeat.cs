using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSeat : MonoBehaviour {

    [SerializeField] GameObject[] CardSlots;
    [SerializeField] Card cardPrefab;

    [SerializeField] TextMeshProUGUI ClientNameText;
    [SerializeField] TextMeshProUGUI StackText;

    private void Start() {
        ClientNameText.SetText(ClientInfo.Name);
    }

    public void SetStackText(int stack) {
        StackText.SetText(stack.ToString());
    }

    public void OwnCards(CardForm[] cards) {
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
}
