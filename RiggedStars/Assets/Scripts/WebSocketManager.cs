using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WebSocketManager : MonoBehaviour {

    private WebSocket webSocket;
    private Dictionary<string, Action<string>> WebsocketActions;


    public void StartWebSocket(Dictionary<string, Action<string>> websocketActions, string websocketPath) {
        WebsocketActions = websocketActions;
        StartCoroutine(ConnectToWebSocket(websocketPath));
    }


    IEnumerator ConnectToWebSocket(string ws) {
        webSocket = new WebSocket(new Uri(ws));
        Debug.Log("Trying to connect to WebSocket: " + ws);
        yield return StartCoroutine(webSocket.Connect());
        Debug.Log("Connected to websocket: " + ws);
        while (true) {
            byte[] replyBytes = webSocket.Recv();
            if (replyBytes != null) {
                string reply = Encoding.ASCII.GetString(replyBytes);
                Debug.Log("Received: " + reply);
                var replyType = JsonConvert.DeserializeObject<WsForm>(reply).Type;
                if (WebsocketActions.ContainsKey(replyType))
                    WebsocketActions[replyType](reply);
            }
            if (webSocket.error != null) {
                Debug.LogError("Error: " + webSocket.error);
                break;
            }
            yield return 0;
        }
        webSocket.Close();
    }

    public void SendMessageToWebSocket(string message) {
        webSocket.SendString(message);
    }

    private void OnApplicationQuit() {
        webSocket.Close();
    }

    public void Close() {
        webSocket.Close();
        WebsocketActions.Clear();
    }
}
