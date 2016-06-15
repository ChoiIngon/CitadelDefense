using UnityEngine;
using System.Collections;

public class BasePlayerUnit : Unit {
	public class UnitState : ScriptableObject
	{
		public int	index; 
		public bool	purchased;
		public bool	equiped;
	}

	public int level;
	public int price;
	public UnitState state;

	// Use this for initialization
	void Start () {
		state = ScriptableObject.CreateInstance<UnitState> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
