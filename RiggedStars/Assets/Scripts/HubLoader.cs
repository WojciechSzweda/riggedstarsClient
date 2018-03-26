using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class HubLoader : MonoBehaviour {

    public TextMeshProUGUI TokenCheck;
    public TextMeshProUGUI ErrorInfo;

    public GameObject RoomListContent;
    public RoomListElement RoomInfo;

    private const string route = "/game/roomList";

    void Start() {

        RefreshRoomList();
    }


    public void RefreshRoomList() {
        StartCoroutine(GetRoomList());
    }

    IEnumerator GetRoomList() {
        var request = new UnityWebRequest("http://" + ServerConfig.getServerURL() + route, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        var response = request.downloadHandler.text;

        var roomList = JsonConvert.DeserializeObject<List<RoomForm>>(response);

        foreach (Transform room in RoomListContent.transform) {
            Destroy(room.gameObject);
        }

        if (request.isHttpError || request.isNetworkError) {
            ErrorInfo.text = "Server Error";
            yield return null;
        }
        else {
            ErrorInfo.text = "";
        }


        foreach (var room in roomList) {
            var roomInfo = Instantiate<RoomListElement>(RoomInfo, RoomListContent.transform);
            roomInfo.SetData(room);
        }
    }

}
