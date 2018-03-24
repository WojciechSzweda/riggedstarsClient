using Newtonsoft.Json;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour {

    public ChatMessage ChatMsgPrefab;
    public GameObject ChatContent;
    public TMP_InputField ChatInput;

    private WebSocketManager WSManager;

    private void Start() {
        WSManager = FindObjectOfType<WebSocketManager>();
        WSManager.OnMessageReceived += MessageReceived;
    }

    public void SendChatMessage() {
        var message = ChatInput.text;

        CreateMessageObject(ClientInfo.Name, message);

        var jsonMsg = JsonConvert.SerializeObject(new WsMessage { Type = "text", Payload = message });
        WSManager.SendMessageToWebSocket(jsonMsg);
        ChatInput.text = "";
    }

    void CreateMessageObject(string name, string message) {
        var msg = Instantiate<ChatMessage>(ChatMsgPrefab, ChatContent.transform);
        msg.SetMessage(name, message);
    }

    public void MessageReceived(string message) {
        Debug.Log("Message Received Event");
        Debug.Log("Message: " + message);

        CreateMessageObject("find his name", message);
    }
}
