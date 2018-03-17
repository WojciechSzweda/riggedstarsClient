using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Login : MonoBehaviour {

    const string IP_ADDRESS = "127.0.0.1";
    const string PORT = "3000";

    public TMP_InputField loginInputField;
    public TMP_InputField passwordInputField;
    public TextMeshProUGUI response;
    public TextMeshProUGUI responseDebug;
    public TextMeshProUGUI tokenDebug;

    EventSystem system;

    void Start() {
        system = EventSystem.current;

    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Selectable next = null;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (next == null)
                    next = system.lastSelectedGameObject.GetComponent<Selectable>();
            }
            else {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next == null)
                    next = system.firstSelectedGameObject.GetComponent<Selectable>();
            }

            if (next != null) {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }

        }
    }


    class LoginForm {
        public string Name;
        public string Password;
    }
#pragma warning disable 649
    class LoginResponseForm {
        public string Data;
        public string Token;
        public int Status;
    }
#pragma warning restore 649


    IEnumerator Post() {
        var request = new UnityWebRequest("http://127.0.0.1:3000/user/login", "POST");
        request.chunkedTransfer = false;

        var jsonText = JsonUtility.ToJson(new LoginForm { Name = loginInputField.text, Password = passwordInputField.text });
        Debug.Log(jsonText);
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonText);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");

        yield return request.SendWebRequest();
        var jsonResponse = request.downloadHandler.text;
        responseDebug.SetText(jsonResponse);
        Debug.Log("Response: " + jsonResponse);

        if (request.isHttpError) {
            if (request.responseCode == 401) {
                Debug.Log("http error" + request.error);
                response.SetText("Wrong login or password");
            }
            yield break;
        }
        if (request.isNetworkError) {
            response.SetText("Connection error");
            Debug.Log("network error" + request.error);
            responseDebug.SetText(request.error);
            yield break;
        }

        var responseData = JsonUtility.FromJson<LoginResponseForm>(jsonResponse);
        if (responseData.Status == 401) {
            response.SetText("Wrong login or password");
            Debug.Log("response status 401");
            yield break;
        }
        Debug.Log("succes");
        response.SetText("Login succesfull!");
        tokenDebug.SetText("Token: " + responseData.Token);

        //#TODO: Save token and load main scene

    }

    public void LoginButton() {
        StartCoroutine(Post());
    }

}
