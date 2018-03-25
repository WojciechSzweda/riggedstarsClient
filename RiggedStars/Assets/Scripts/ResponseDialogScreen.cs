using UnityEngine;
using TMPro;

public class ResponseDialogScreen : MonoBehaviour {

    public TextMeshProUGUI ReponseText;


    public void SetResponseText(string response) {
        ReponseText.SetText(response);
    }

    public void Close() {
        this.gameObject.SetActive(false);
        ReponseText.SetText("");
    }
}
