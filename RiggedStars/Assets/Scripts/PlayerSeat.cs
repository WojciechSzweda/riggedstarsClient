using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeat : MonoBehaviour {

    [SerializeField]
    GameObject[] CardSlots;
    [SerializeField]
    Card cardPrefab;

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
