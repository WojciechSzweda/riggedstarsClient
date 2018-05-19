using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI PotText;
    int PotSize;

    public void AddToPot(int ammount) {
        PotSize += ammount;
        PotText.SetText(PotSize.ToString());
    }

    public void ClearPot() {
        PotSize = 0;
        PotText.SetText("");
    }
}
