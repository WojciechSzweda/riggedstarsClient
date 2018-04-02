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
        BetSlider.value = value;
        Debug.Log("Input changed " + value.ToString());
    }

    public void BetSliderChanged() {
        BetInput.text = BetSlider.value.ToString();
        Debug.Log("Slider changed " + BetSlider.value.ToString());
    }

    public void ActiveBet(int minBet) {
        BetInput.text = minBet.ToString();
        //if (minBet == 0) {
        //    //can check, cant fold
        //    CheckButton.interactable = true;
        //    BetButton.interactable = true;
        //    FoldButton.interactable = false;
        //}
        //else if (minBet > 0) {
        //    CheckButton.interactable = false;
        //    BetButton.interactable = true;
        //    FoldButton.interactable = true;
        //}
    }

    public void Bet() {
        int bet = int.Parse(BetInput.text);
        SendBetMessage(bet);
    }

    void SendBetMessage(int bet = 0) {
        var betForm = new BetForm { Type = "bet", Payload = bet };
        var jsonMsg = JsonConvert.SerializeObject(betForm);
        OnClientAction(jsonMsg);
    }

    public void Check() {
        SendBetMessage();
    }

    public void Fold() {
        OnClientAction(JsonConvert.SerializeObject(new WsForm { Type = "fold" }));
    }

}
