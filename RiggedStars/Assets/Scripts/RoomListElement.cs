using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class RoomListElement : MonoBehaviour {

    public TextMeshProUGUI RoomName;
    public TextMeshProUGUI ClientsCount;
    private int RoomID;
    private WebSocketManager WSManager;

    private void Start() {
        WSManager = FindObjectOfType<WebSocketManager>();
    }

    public void SetData(Room roomInfo) {
        RoomName.text = roomInfo.Name;
        ClientsCount.text = roomInfo.ClientsCount.ToString() + "/" + roomInfo.MaxClients.ToString();
        RoomID = roomInfo.ID;
    }

    public void JoinButton() {

        JoinRoom();
        FindObjectOfType<UIManager>().JoinRoom();

        //TODO: error handling, joining room error
    }

    void JoinRoom() {
        var msg = JsonConvert.SerializeObject(new WsMessage { Type = "joinRoom", Payload = RoomID.ToString() });
        WSManager.SendMessageToWebSocket(msg);
    }


}
