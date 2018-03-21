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

    private const string url = "http://127.0.0.1:3000/game/roomList";

    void Start() {

        TokenCheck.text = PlayerPrefs.GetString("Token");
        RefreshRoomList();
    }


    public void RefreshRoomList() {
        StartCoroutine(GetRoomList());
    }

    IEnumerator GetRoomList() {
        var request = new UnityWebRequest(url, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        var response = request.downloadHandler.text;

        var roomList = JsonConvert.DeserializeObject<List<Room>>(response);

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
            Debug.Log(room.Name);
        }
    }

}
