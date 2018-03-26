using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class WebSocketManager : MonoBehaviour {

    const string WebSocketRoute = "/game?user=";

    public delegate void MessageReceived(string message);
    public event MessageReceived OnMessageReceived;

    private WebSocket webSocket;

    void Start() {
        //StartCoroutine(StartWebSocket());
    }

    void Update() {

    }

    //IEnumerator StartWebSocket() {
    //    string ws = "ws://" + ServerConfig.getServerURL() + WebSocketRoute + ClientInfo.ID.ToString();
    //    webSocket = new WebSocket(new Uri(ws));
    //    Debug.Log("Trying to connect to WebSocket: " + ws);
    //    yield return StartCoroutine(webSocket.Connect());
    //    Debug.Log("Connected to websocket: " + ws);
    //    while (true) {
    //        byte[] replyBytes = webSocket.Recv();
    //        if (replyBytes != null) {
    //            string reply = Encoding.ASCII.GetString(replyBytes);
    //            Debug.Log(reply);
    //            Debug.Log("Received: " + reply);
    //            var replyMsg = JsonConvert.DeserializeObject<WsMessage>(reply);
    //            if (replyMsg.Type == "text") {
    //                OnMessageReceived(replyMsg.Payload);
    //            }
    //        }
    //        if (webSocket.error != null) {
    //            Debug.LogError("Error: " + webSocket.error);
    //            break;
    //        }
    //        yield return 0;
    //    }
    //    webSocket.Close();
    //}

    public void SendMessageToWebSocket(string message) {
        webSocket.SendString(message);
    }

    private void OnApplicationQuit() {
        webSocket.Close();
    }
}

//public class WsMessage {
//    public string Type;
//    public string Payload;
//}