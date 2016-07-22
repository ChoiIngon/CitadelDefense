using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
    [System.Serializable]
    public class ItemInfo
    {
        public string name;
        public Sprite icon;
        public string description;
    }

    public ItemInfo info;
    public bool equipped;

    public Buff[] buffPrefabs;
}
