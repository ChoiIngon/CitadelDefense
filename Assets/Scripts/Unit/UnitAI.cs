using UnityEngine;
using System.Collections;

public class UnitAI : MonoBehaviour {
	public enum ActionState
	{
		Idle,
		Move,
		Attack,
		Dead
	}
	public ActionState actionState = ActionState.Idle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
