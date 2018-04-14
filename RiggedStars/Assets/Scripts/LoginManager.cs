using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;



public class LoginManager : MonoBehaviour {


    public TMP_InputField loginInputField;
    public TMP_InputField passwordInputField;
    public TextMeshProUGUI response;


    void DebugAutoLogin() {
        loginInputField.text = "Wojtek";
        passwordInputField.text = "123456";

        LoginButton();
    }

    private void Start() {
        loginInputField.onValueChanged.AddListener(delegate { response.SetText(""); });
        passwordInputField.onValueChanged.AddListener(delegate { response.SetText(""); });

        DebugAutoLogin();
    }

    IEnumerator Post() {
        UnityWebRequest request = REST.CreatePostRequest(
            "http://" + ServerConfig.getServerURL() + "/user/login",
            new UserForm { Name = loginInputField.text, Password = passwordInputField.text }
            );
        //TODO: logging feedback
        yield return request.SendWebRequest();
        var jsonResponse = request.downloadHandler.text;
        Debug.Log("Response: " + jsonResponse);

        if (request.responseCode == 401) {
            Debug.Log("unathorized " + request.error);
            response.SetText("Wrong login or password");
            loginInputField.ActivateInputField();
            yield break;
        }
        if (request.isHttpError || request.isNetworkError) {
            Debug.Log("error: " + request.error);
            response.SetText("Connection error");
            yield break;
        }

        var responseData = JsonConvert.DeserializeObject<UserResponseForm>(jsonResponse);
        if (responseData.Status != 200) {
            Debug.Log("Response error");
            response.SetText("Response from server error");
            yield break;
        }


        Debug.Log("succes");
        response.SetText("Login succesfull!");

        ClientInfo.SetClientInfo(responseData.Data, responseData.Token);
        SceneManager.LoadScene("Main");
    }

    public void LoginButton() {
        StartCoroutine(Post());
    }

}
