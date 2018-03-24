using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginCanvasHandler : MonoBehaviour {


    void Start() {
        system = EventSystem.current;

    }
    EventSystem system;
#pragma warning disable 618
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            FindObjectOfType<LoginManager>().LoginButton();
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            Selectable next = null;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (next == null)
                    next = system.lastSelectedGameObject.GetComponent<Selectable>();
            }
            else {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next == null)
                    next = system.firstSelectedGameObject.GetComponent<Selectable>();
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
