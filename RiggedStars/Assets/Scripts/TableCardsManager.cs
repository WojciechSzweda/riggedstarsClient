using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCardsManager : MonoBehaviour {

    [SerializeField]
    GameObject[] FlopCardsSlot;
    [SerializeField]
    GameObject TurnCardSlot;
    bool TurnCardDown;
    [SerializeField]
    GameObject RiverCardSlot;
    [SerializeField]
    Card cardPrefab;

    private void Start() {
        TurnCardDown = false;
    }

    void Flop(CardForm[] cards) {
        for (int i = 0; i < 3; i++) {
            var card = Instantiate<Card>(cardPrefab, FlopCardsSlot[i].transform);
            card.CreateCard(cards[i]);
        }
    }

    void Turn(CardForm[] cards) {
        var card = Instantiate<Card>(cardPrefab, TurnCardSlot.transform);
        card.CreateCard(cards[0]);
        TurnCardDown = true;
    }

    void River(CardForm[] cards) {
        var card = Instantiate<Card>(cardPrefab, RiverCardSlot.transform);
        card.CreateCard(cards[0]);
    }


    public void SetTableCards(CardForm[] cards) {
        if (cards.Length > 1) {
            Flop(cards);
        }
        else if (TurnCardDown) {
            River(cards);
        }
        else {
            Turn(cards);
        }

    }
}
