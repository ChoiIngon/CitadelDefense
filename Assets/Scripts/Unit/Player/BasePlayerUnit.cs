using UnityEngine;
using System.Collections;

public class BasePlayerUnit : Unit {
	[System.Serializable]
	public class UnitState /*: ScriptableObject*/
	{
		public int	index; 
		public bool	purchased;
		public bool	equiped;
		public int  level;
	}

	public int price;
	public int maxLevel;
	public UnitState state;

    public virtual void Levelup() { }
}
