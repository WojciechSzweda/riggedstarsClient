using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class RoomListElement : MonoBehaviour {

    public TextMeshProUGUI RoomName;
    public TextMeshProUGUI ClientsCount;
    private int RoomID;
    private int MaxClients;
    private RoomManager RoomManager;

    private void Start() {
        RoomManager = FindObjectOfType<RoomManager>();
    }

    public void SetData(RoomForm roomInfo) {
        RoomName.text = roomInfo.Name;
        ClientsCount.text = roomInfo.ClientsCount.ToString() + "/" + roomInfo.MaxClients.ToString();
        RoomID = roomInfo.ID;
        MaxClients = roomInfo.MaxClients;
    }

    public void JoinButton() {
        RoomManager.CreateRoom(RoomID, MaxClients);
        //TODO: Hide HUB
        //TODO: error handling, joining room error
    }

}
