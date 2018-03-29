﻿using UnityEngine;
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
        Debug.Log("Response: " + jsonResponse);


        if (request.responseCode == 400) {
            Debug.Log("http error" + request.error);
            ShowResponseScreen("Name already exist");
            yield break;
        }

        if (request.isHttpError || request.isNetworkError) {
            Debug.Log("error: " + request.error);
            ShowResponseScreen("Connection Error");
            yield break;
        }
        var responseData = JsonConvert.DeserializeObject<UserResponseForm>(jsonResponse);
        if (responseData.Status != 200) {
            Debug.Log("Response error");
            ShowResponseScreen("Response from server error");
            yield break;
        }

        Debug.Log("succes");
        ShowResponseScreen("Registration succesfull!");
    }


    public void RegisterButton() {
        StartCoroutine(Register());
    }
}
