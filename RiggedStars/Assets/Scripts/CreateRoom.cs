using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class CreateRoom : MonoBehaviour {

    public TMP_InputField NameInput;
    public TMP_InputField MaxClientsInput;
    public GameObject ValidateErrorPanel;

    public void CreateRoomButton() {
        StartCoroutine(CreateRoomPost());
    }

    void ShowValidateError(string error) {
        ValidateErrorPanel.SetActive(true);
        ValidateErrorPanel.GetComponentInChildren<TextMeshProUGUI>().SetText(error);
    }

    IEnumerator CreateRoomPost() {
        int maxClients;
        var maxClientsParseError = int.TryParse(MaxClientsInput.text, out maxClients);

        if (maxClientsParseError == false || maxClients > 9 || maxClients < 2) {
            ShowValidateError("Max clients field has to be a number (2-9)");
            yield break;
        }

        UnityWebRequest request = REST.CreatePostRequest(
            "http://" + ServerConfig.getServerURL() + "/game/createRoom",
            new CreateRoomForm { Name = NameInput.text, MaxClients = maxClients }
            );
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError) {
            ShowValidateError("Connection Error");
            yield break;
        }

        Debug.Log("room created");
        FindObjectOfType<HubLoader>().RefreshRoomList();
        //TODO: make prefab of this gameobject?
        gameObject.SetActive(false);
        NameInput.text = "";
        MaxClientsInput.text = "";
    }

}
