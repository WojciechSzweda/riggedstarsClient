using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WinnerNotifier : MonoBehaviour {
    [SerializeField] TextMeshProUGUI WinnerText;

    public void SetWinnerMessage(PotWinner[] winners) {
        WinnerText.SetText(string.Join("\n", winners.Select(w => string.Format("{0} won {1}.", w.Winner.Name, w.Ammount)).ToArray()));
    }

    public void ClearWinnerMessage() {
        WinnerText.SetText("");
    }

    IEnumerator ShowMessage(PotWinner[] winners) {
        this.transform.localScale = Vector3.one;
        SetWinnerMessage(winners);
        yield return new WaitForSeconds(2);
        ClearWinnerMessage();
        this.transform.localScale = Vector3.zero;
    }

    public void ShowWinnerMessage(PotWinner[] winners) {
        StartCoroutine(ShowMessage(winners));
    }
}
