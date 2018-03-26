using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
        var request = new UnityWebRequest("http://" + ServerConfig.getServerURL() + "/user/login", "POST");
        request.chunkedTransfer = false;

        var jsonText = JsonUtility.ToJson(new UserForm { Name = loginInputField.text, Password = passwordInputField.text });
        Debug.Log(jsonText);
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonText);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        var jsonResponse = request.downloadHandler.text;
        Debug.Log("Response: " + jsonResponse);

        if (request.isHttpError) {
            if (request.responseCode == 401) {
                Debug.Log("http error" + request.error);
                response.SetText("Wrong login or password");
                loginInputField.ActivateInputField();
            }
            yield break;
        }
        if (request.isNetworkError) {
            Debug.Log("network error" + request.error);
            response.SetText("Connection error");
            yield break;
        }

        var responseData = JsonConvert.DeserializeObject<UserResponseForm>(jsonResponse);
        if (responseData.Status != 200) {
            Debug.Log("Response error");
            response.SetText("Response from server error");
            yield break;
        }
        if (responseData.Status == 401) {
            response.SetText("Wrong login or password");
            Debug.Log("response status 401");
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
