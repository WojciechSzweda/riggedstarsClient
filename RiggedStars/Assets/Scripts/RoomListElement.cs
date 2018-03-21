using UnityEngine;
using TMPro;

public class RoomListElement : MonoBehaviour {

    public TextMeshProUGUI RoomName;
    public TextMeshProUGUI ClientsCount;
    private int RoomID;



    public void SetData(Room roomInfo) {
        RoomName.text = roomInfo.Name;
        ClientsCount.text = roomInfo.ClientsCount.ToString() + "/" + roomInfo.MaxClients.ToString();
        RoomID = roomInfo.ID;
    }
}
