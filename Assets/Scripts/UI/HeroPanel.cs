using UnityEngine;
using System.Collections;

public class HeroPanel : MonoBehaviour {
    public HeroPanelElement element;
	// Use this for initialization
	void Start () {
        Init();
	}

    void OnEnable()
    {
        Init();
    }
    void Init()
    {
        while(0 < transform.childCount)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(null);
            DestroyObject(child.gameObject);
        }
        foreach(UnitHero hero in GameManager.Instance.heros)
        {
            HeroPanelElement element = (HeroPanelElement)GameObject.Instantiate<HeroPanelElement>(this.element);
            element.SetUnitHero(hero);
            element.transform.SetParent(transform);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
