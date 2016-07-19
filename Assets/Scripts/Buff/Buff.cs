using UnityEngine;
using System.Collections;

public class Buff : MonoBehaviour {
    public string id;
    [HideInInspector]
    public Unit unit;
	// Use this for initialization
	public virtual void Start () {
        if (unit.buffs.ContainsKey(id))
        {
            unit = null;
            Destroy(gameObject);
            return;
        }

        unit.buffs.Add(id, this);
	}

    void OnDestroy()
    {
        if(null == unit)
        {
            return;
        }
        if (unit.buffs.ContainsKey(id))
        {
            unit.buffs.Remove(id);
        }
    }
}
