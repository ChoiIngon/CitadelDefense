using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public enum State {
		Setting,
		Play
	}
	public State state;
	// Use this for initialization
	void Start () {
		state = State.Setting;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
