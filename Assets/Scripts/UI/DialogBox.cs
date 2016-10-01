﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {
	public Image background;
	[HideInInspector]
	public RectTransform backgroundRect;
	public Button submit;
	public Button cancel;
	[HideInInspector]
	public Vector2 size {
		set {
			backgroundRect.sizeDelta = value;
		}
	}
	[HideInInspector]
	public Text text;

	// Use this for initialization
	void Start () {
		backgroundRect = background.gameObject.GetComponent<RectTransform> ();
		gameObject.SetActive (false);
		cancel.onClick.AddListener (Close);
	}

	public void Close() {
		text.text = "";
		submit.onClick.RemoveAllListeners();
		gameObject.SetActive(false);
	}
		
	public void Activate(string text, UnityEngine.Events.UnityAction action)
	{
		gameObject.SetActive (true);
		this.text.text = text;
		backgroundRect.sizeDelta = new Vector2 (backgroundRect.rect.width, this.text.preferredHeight + 130.0f);
		submit.onClick.RemoveAllListeners();
		submit.onClick.AddListener(action);
		submit.onClick.AddListener(Close);
	}
}