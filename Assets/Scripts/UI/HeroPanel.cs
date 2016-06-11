using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroPanel : MonoBehaviour {
    public HeroPanelElement element;
	// Use this for initialization
	void Start () 
	{
        Init();
	}

    void OnEnable()
    {
        Init();
    }

    void Init()
    {
		Transform content = transform.FindChild ("Viewport/Content");
        while(0 < content.childCount)
        {
            Transform child = content.GetChild(0);
            child.SetParent(null);
            DestroyObject(child.gameObject);
        }
        foreach(UnitHero hero in GameManager.Instance.heros)
        {
            HeroPanelElement element = (HeroPanelElement)GameObject.Instantiate<HeroPanelElement>(this.element);
            element.SetUnitHero(hero);
            element.transform.SetParent(content);
        }
    }
}
