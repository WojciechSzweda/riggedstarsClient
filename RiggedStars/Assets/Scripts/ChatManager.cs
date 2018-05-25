using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChatManager : MonoBehaviour {

    public ChatMessage ChatMsgPrefab;
    public GameObject ChatContent;
    public TMP_InputField ChatInput;


    public delegate void SendChatMessageEvent(string message);
    public event SendChatMessageEvent OnSendMessage;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if(EventSystem.current.currentSelectedGameObject == ChatInput.gameObject) {
                SendChatMessage();
                ChatInput.ActivateInputField();
            }
        }
    }

    public void SendChatMessage() {
        var message = ChatInput.text;

        CreateMessageObject(ClientInfo.Name, message);

        var jsonMsg = JsonConvert.SerializeObject(new WsChatMessage { Type = "text", Payload = message });
        OnSendMessage(jsonMsg);
        ChatInput.text = "";
    }

    void CreateMessageObject(string name, string message) {
        var msg = Instantiate<ChatMessage>(ChatMsgPrefab, ChatContent.transform);
        msg.SetMessage(name, message);
    }

    public void MessageReceived(string name, string message) {
        CreateMessageObject(name, message);
    }
}
