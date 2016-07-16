using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMessageBox : MonoBehaviour {
	public float time;
	IEnumerator coroutine;
	public string message {
		set {
			if (null != coroutine) {
				return;
			}
			gameObject.SetActive (true);
			GetComponent<Text> ().text = value;
			coroutine = DestorySelf ();
			StartCoroutine (coroutine);
		}
	}
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}

	IEnumerator DestorySelf()
	{
		yield return new WaitForSeconds (time);
		gameObject.SetActive (false);
		coroutine = null;
	}
}
