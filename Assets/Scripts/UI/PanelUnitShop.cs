using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelUnitShop : MonoBehaviour {
	public enum ShopType
	{
		Hero,
		Tower,
		Building
	}

	public PanelUnitShopElement element;
	public ShopType type;
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
        foreach(HeroUnit unit in GameManager.Instance.heros)
        {
			PanelUnitShopElement element = (PanelUnitShopElement)GameObject.Instantiate<PanelUnitShopElement>(this.element);
			element.SetUnit(unit);
            element.transform.SetParent(content);
        }
    }
}
