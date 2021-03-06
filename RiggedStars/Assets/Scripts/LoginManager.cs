﻿using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;



public class LoginManager : MonoBehaviour {


    public TMP_InputField loginInputField;
    public TMP_InputField passwordInputField;
    public TextMeshProUGUI response;


    private void Start() {
        loginInputField.onValueChanged.AddListener(delegate { response.SetText(""); });
        passwordInputField.onValueChanged.AddListener(delegate { response.SetText(""); });
    }

    IEnumerator Post() {
        UnityWebRequest request = REST.CreatePostRequest(
            "http://" + ServerConfig.getServerURL() + "/user/login",
            new UserForm { Name = loginInputField.text, Password = passwordInputField.text }
            );
        yield return request.SendWebRequest();
        var jsonResponse = request.downloadHandler.text;
        if (request.responseCode == 401) {
            response.SetText("Wrong login or password");
            loginInputField.ActivateInputField();
            yield break;
        }
        if (request.isHttpError || request.isNetworkError) {
            response.SetText("Connection error");
            yield break;
        }

        var responseData = JsonConvert.DeserializeObject<UserResponseForm>(jsonResponse);
        if (responseData.Status != 200) {
            response.SetText("Response from server error");
            yield break;
        }


        response.SetText("Login succesfull!");

        ClientInfo.SetClientInfo(responseData.Data, responseData.Token);
        SceneManager.LoadScene("Main");
    }

    public void LoginButton() {
        StartCoroutine(Post());
    }

}
