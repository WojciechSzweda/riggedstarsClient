using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Newtonsoft.Json;

public class BetStage : MonoBehaviour {

    [SerializeField]
    Button CheckButton;
    [SerializeField]
    Button BetButton;
    [SerializeField]
    Button FoldButton;
    [SerializeField]
    TMP_InputField BetInput;
    [SerializeField]
    Slider BetSlider;

    public delegate void SendBetMessageEvent(string message);
    public event SendBetMessageEvent OnClientAction;

    public void BetInputChanged() {
        int value = int.Parse(BetInput.text);
        if (value < BetSlider.minValue) {
            BetInput.text = BetSlider.minValue.ToString();
            BetSlider.value = BetSlider.minValue;
        }
        else {
            BetSlider.value = value;
        }
    }

    public void BetSliderChanged() {
        BetInput.text = BetSlider.value.ToString();
    }

    public void ActiveBet(int minBet) {
        BetInput.text = minBet.ToString();
        if (minBet == 0) {
            //can check, cant fold
            CheckButton.interactable = true;
            BetButton.interactable = true;
            FoldButton.interactable = false;
            BetInput.interactable = true;
            BetSlider.interactable = true;
        }
        else if (minBet > 0) {
            //cant check, can bet/fold
            CheckButton.interactable = false;
            BetButton.interactable = true;
            FoldButton.interactable = true;
            BetInput.interactable = true;
            BetSlider.interactable = true;
            BetSlider.minValue = minBet;
        }
    }

    void ActionTaken() {
        CheckButton.interactable = false;
        BetButton.interactable = false;
        FoldButton.interactable = false;
        BetInput.interactable = false;
        BetSlider.interactable = false;
    }

    public void Bet() {
        int bet = int.Parse(BetInput.text);
        SendBetMessage(bet);
        ActionTaken();
    }

    void SendBetMessage(int bet = 0) {
        var betForm = new BetForm { Type = "bet", Payload = bet };
        var jsonMsg = JsonConvert.SerializeObject(betForm);
        OnClientAction(jsonMsg);
    }

    public void Check() {
        SendBetMessage();
        ActionTaken();
    }

    public void Fold() {
        OnClientAction(JsonConvert.SerializeObject(new WsForm { Type = "fold" }));
        ActionTaken();
    }

}
