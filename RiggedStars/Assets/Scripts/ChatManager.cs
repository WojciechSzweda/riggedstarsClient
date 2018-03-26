using Newtonsoft.Json;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour {

    public ChatMessage ChatMsgPrefab;
    public GameObject ChatContent;
    public TMP_InputField ChatInput;

    //private WebSocketManager WSManager;

    public delegate void SendChatMessageEvent(string message);
    public event SendChatMessageEvent OnSendMessage;

    private void Start() {
        //WSManager = FindObjectOfType<WebSocketManager>();
        //WSManager.OnMessageReceived += MessageReceived;
    }

    public void SendChatMessage() {
        var message = ChatInput.text;

        CreateMessageObject(ClientInfo.Name, message);

        var jsonMsg = JsonConvert.SerializeObject(new WsChatMessage { Type = "text", Payload = message });
        OnSendMessage(jsonMsg);
        //WSManager.SendMessageToWebSocket(jsonMsg);
        ChatInput.text = "";
    }

    void CreateMessageObject(string name, string message) {
        var msg = Instantiate<ChatMessage>(ChatMsgPrefab, ChatContent.transform);
        msg.SetMessage(name, message);
    }

    public void MessageReceived(string name, string message) {
        Debug.Log("Message: " + message);

        CreateMessageObject(name, message);
    }
}
