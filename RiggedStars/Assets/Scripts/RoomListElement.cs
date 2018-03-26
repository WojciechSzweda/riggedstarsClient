using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class RoomListElement : MonoBehaviour {

    public TextMeshProUGUI RoomName;
    public TextMeshProUGUI ClientsCount;
    private int RoomID;
    private RoomManager RoomManager;

    private void Start() {
        RoomManager = FindObjectOfType<RoomManager>();
    }

    public void SetData(RoomForm roomInfo) {
        RoomName.text = roomInfo.Name;
        ClientsCount.text = roomInfo.ClientsCount.ToString() + "/" + roomInfo.MaxClients.ToString();
        RoomID = roomInfo.ID;
    }

    public void JoinButton() {
        RoomManager.CreateRoom(RoomID);
        //TODO: Hide HUB
        //TODO: error handling, joining room error
    }

}
