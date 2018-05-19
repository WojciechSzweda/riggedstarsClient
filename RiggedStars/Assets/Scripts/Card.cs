using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Card : MonoBehaviour {

    public CardForm cardForm;
    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI SuitText;

    public void CreateCard(CardForm card) {
        cardForm = card;
        SuitColor suit = SuitConverter(card.Suit);
        SuitText.SetText(suit.Symbol);
        SuitText.color = suit.color;
        ValueText.SetText(ValueConverter(card.Value));
        ValueText.color = suit.color;
    }

    struct SuitColor {
        public string Symbol;
        public Color color;
    }


    SuitColor SuitConverter(string suit) {
        if (suit == "clubs")
            return new SuitColor { Symbol = "♣", color = new Color(0,200,0) };
        else if (suit == "spades")
            return new SuitColor { Symbol = "♠", color = Color.black };
        else if (suit == "diamonds")
            return new SuitColor { Symbol = "♦", color = Color.blue };
        else if (suit == "hearts")
            return new SuitColor { Symbol = "♥", color = Color.red };
        return new SuitColor { Symbol = "?", color = Color.black };
    }

    string ValueConverter(int value) {
        if (value == 1)
            return "A";
        else if (value == 11)
            return "J";
        else if (value == 12)
            return "Q";
        else if (value == 13)
            return "K";
        return value.ToString();
    }
}
