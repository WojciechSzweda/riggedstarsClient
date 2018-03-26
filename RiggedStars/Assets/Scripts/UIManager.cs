using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject HUB;

    private void Start() {
    }

    public void HideHUB() {
        HUB.SetActive(false);
    }

    public void ShowHUB() {
        HUB.SetActive(true);
    }



}
