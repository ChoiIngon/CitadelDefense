using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClose : MonoBehaviour {
	
	void Start () {
		Button button = GetComponent<Button> ();
		if (null == button) {
			throw new System.Exception ("fail to load \'Button\'");
		}

		//RectTransform rectTransform = GetComponent<RectTransform> ();
		//rectTransform.localPosition = Vector3.zero;
		button.onClick.AddListener (() => {
			transform.parent.gameObject.SetActive(false);
		});
	}
}
