using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Registration : MonoBehaviour {
    public TMP_InputField NameInput;
    public TMP_InputField PasswordInput;
    public ResponseDialogScreen ResponseScreen;

    void ShowResponseScreen(string response) {
        ResponseScreen.gameObject.SetActive(true);
        ResponseScreen.SetResponseText(response);
    }

    IEnumerator Register() {
        UnityWebRequest request = REST.CreatePostRequest(
            "http://" + ServerConfig.getServerURL() + "/user",
            new UserForm { Name = NameInput.text, Password = PasswordInput.text }
            );

        yield return request.SendWebRequest();
        var jsonResponse = request.downloadHandler.text;


        if (request.responseCode == 400) {
            ShowResponseScreen("Name already exist");
            yield break;
        }

        if (request.isHttpError || request.isNetworkError) {
            ShowResponseScreen("Connection Error");
            yield break;
        }
        var responseData = JsonConvert.DeserializeObject<UserResponseForm>(jsonResponse);
        if (responseData.Status != 200) {
            ShowResponseScreen("Response from server error");
            yield break;
        }

        ShowResponseScreen("Registration succesfull!");
    }


    public void RegisterButton() {
        StartCoroutine(Register());
    }
}
