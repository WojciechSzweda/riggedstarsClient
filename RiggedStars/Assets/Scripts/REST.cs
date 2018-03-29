using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public static class REST {

	public static UnityWebRequest CreatePostRequest(string url, object body) {
        var request = new UnityWebRequest(url, "POST") {
            chunkedTransfer = false
        };

        var jsonText = JsonUtility.ToJson(body);
        Debug.Log(jsonText);
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonText);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    public static UnityWebRequest CreateGetRequest(string url) {
        var request = new UnityWebRequest(url, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        return request;
    }
}
