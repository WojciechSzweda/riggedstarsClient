using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChatMessage : MonoBehaviour {
    private TextMeshProUGUI Message;
    public string Name;

    private void Awake() {
        Message = GetComponent<TextMeshProUGUI>();
    }

    public void SetMessage(string name, string message) {
        Name = name;
        Message.SetText(name + ": " + message);
    }
}
