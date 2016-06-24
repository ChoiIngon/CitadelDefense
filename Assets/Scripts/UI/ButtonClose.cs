using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClose : MonoBehaviour {
	public GameObject closeTarget;
	void Start () {
		Button button = GetComponent<Button> ();
		if (null == button) {
			throw new System.Exception ("fail to load \'Button\'");
		}

		button.onClick.AddListener (() => {
			closeTarget.SetActive(false);
		});
	}
}
