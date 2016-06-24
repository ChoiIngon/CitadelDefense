using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPopupDialog : MonoBehaviour {
	public Text _title;
	public Text _content;
	public string title {
		set { _title.text = value; }
	}
	public string content {
		set { _content.text = value; }
	}
}
