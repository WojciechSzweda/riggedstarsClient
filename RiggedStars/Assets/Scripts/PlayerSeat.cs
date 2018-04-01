using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeat : MonoBehaviour {

    [SerializeField]
    GameObject[] CardSlots;
    [SerializeField]
    Card cardPrefab;

    public void OwnCards(CardForm[] cards) {
        for (int i = 0; i < 2; i++) {
            var card = Instantiate<Card>(cardPrefab, CardSlots[i].transform);
            card.CreateCard(cards[i]);
        }
    }
}
