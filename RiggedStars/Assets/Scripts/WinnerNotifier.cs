using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerNotifier : MonoBehaviour {
    [SerializeField] TextMeshProUGUI WinnerText;

    public void SetWinnerMessage(string[] clientName, int pot) {
        WinnerText.SetText(string.Join(" and ", clientName) + " won the pot\n" + pot.ToString());
    }

    public void ClearWinnerMessage() {
        WinnerText.SetText("");
    }

    IEnumerator ShowMessage(string[] clientName, int pot) {
        this.transform.localScale = Vector3.one;
        SetWinnerMessage(clientName, pot);
        yield return new WaitForSeconds(2);
        ClearWinnerMessage();
        this.transform.localScale = Vector3.zero;
    }

    public void ShowWinnerMessage(string[] clientName, int pot) {
        StartCoroutine(ShowMessage(clientName, pot));
    }
}
