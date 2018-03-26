using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ClientListManager : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI ClientNamePrefab;
    [SerializeField]
    GameObject ClientListContent;

    Dictionary<int, TextMeshProUGUI> Clients;

    private void Start() {
        Clients = new Dictionary<int, TextMeshProUGUI>();
    }

    public void AddClient(int id, string name) {
        var clientName = Instantiate<TextMeshProUGUI>(ClientNamePrefab, ClientListContent.transform);
        clientName.SetText(name);
        Clients.Add(id, clientName);
    }

    public void DeleteClient(int id) {
        var clientName = Clients[id];
        Destroy(clientName.gameObject);
        Clients.Remove(id);
    }
}
