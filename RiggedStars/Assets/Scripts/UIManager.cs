using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject HUB;
    [SerializeField]
    private GameObject Room;

    private void Start() {
    }

    public void JoinRoom() {
        HUB.SetActive(false);
        Room.SetActive(true);
    }

    public void JoinHub() {
        HUB.SetActive(true);
        Room.SetActive(false);
    }



}
