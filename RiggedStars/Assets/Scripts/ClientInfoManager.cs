using UnityEngine;
using TMPro;

public class ClientInfoManager : MonoBehaviour {

    public TextMeshProUGUI ClientNameText;

    private void Start() {
        RefreshClientInfoOnUI();
    }


    public void RefreshClientInfoOnUI() {
        ClientNameText.SetText(ClientInfo.Name);
    }
}
