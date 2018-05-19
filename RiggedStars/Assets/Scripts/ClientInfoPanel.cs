using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClientInfoPanel : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI ClientName;

	void Start () {
		ClientName.SetText(ClientInfo.Name);
	}
}
