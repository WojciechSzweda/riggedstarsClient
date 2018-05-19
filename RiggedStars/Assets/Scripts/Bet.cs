using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bet : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI BetText;
    int betAmmount;

    public void SetBetSize(int size) {
        betAmmount += size;
        if (betAmmount != 0)
            BetText.SetText(betAmmount.ToString());
    }

    public void ClearBet() {
        betAmmount = 0;
        BetText.SetText("");
    }
}
