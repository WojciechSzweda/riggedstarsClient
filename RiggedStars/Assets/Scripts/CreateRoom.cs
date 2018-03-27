using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class CreateRoom : MonoBehaviour {

    public TMP_InputField NameInput;
    public TMP_InputField MaxClientsInput;


    public void CreateRoomButton() {
        StartCoroutine(CreateRoomPost());
    }

    IEnumerator CreateRoomPost() {
        var request = new UnityWebRequest("http://" + ServerConfig.getServerURL() + "/game/createRoom", "POST");
        request.chunkedTransfer = false;
        int maxClients;
        var maxClientsParseError = int.TryParse(MaxClientsInput.text, out maxClients);

        if (maxClientsParseError == false) {
            //TODO: Show error msg
            yield return null;
        }

        var jsonText = JsonUtility.ToJson(new CreateRoomForm { Name = NameInput.text, MaxClients = maxClients });
        Debug.Log(jsonText);
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonText);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError) {
            //TODO: erro msg
            yield break;
        }

        Debug.Log("room created");
        FindObjectOfType<HubLoader>().RefreshRoomList();
        //TODO: make prefab
        gameObject.SetActive(false);
        NameInput.text = "";
        MaxClientsInput.text = "";
    }

}
