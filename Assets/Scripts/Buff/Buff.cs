using UnityEngine;
using System.Collections;

public abstract class Buff : MonoBehaviour {
    [System.Serializable]
    public class BuffInfo
    {
        public string id;
        public string name;
        public string description;
        public Sprite icon;
    }

	public BuffInfo info;
	public int level;

    [HideInInspector]
    public Unit unit;
	// Use this for initialization
	public virtual void Start () {
        if (unit.buffs.ContainsKey(info.id))
        {
            unit = null;
            Destroy(gameObject);
            return;
        }

        unit.buffs.Add(info.id, this);
	}

    void OnDestroy()
    {
        if(null == unit)
        {
            return;
        }
        if (unit.buffs.ContainsKey(info.id))
        {
            unit.buffs.Remove(info.id);
        }
    }

	public abstract void Upgrade ();
}
