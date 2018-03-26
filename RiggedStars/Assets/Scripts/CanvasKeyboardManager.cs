using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class ButtonEvent : UnityEvent { }

public class CanvasKeyboardManager : MonoBehaviour {

    public GameObject FirstSelected;
    public ButtonEvent ButtonClick;
    void Start() {
        system = EventSystem.current;
        system.SetSelectedGameObject(FirstSelected.gameObject, new BaseEventData(system));
    }

    public void Test() {
        Debug.Log("CLICK");
    }

    EventSystem system;
#pragma warning disable 618
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            ButtonClick.Invoke();
            //FindObjectOfType<LoginManager>().LoginButton();
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            Selectable next = null;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (next == null)
                    next = FirstSelected.GetComponent<Selectable>();
            }
            else {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next == null)
                    next = FirstSelected.GetComponent<Selectable>();
            }

            if (next != null) {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }
#pragma warning restore 618
}
