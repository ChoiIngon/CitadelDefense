using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelHeroShop : MonoBehaviour {
	public ContentHeroShop contentPrefab;
	public Transform content;

	void OnEnable()
    {
        Init();
    }

	void OnDisable()
	{
		if (null == GameManager.Instance) {
			return;
		}
		foreach(CitadelParts parts in GameManager.Instance.citadel.citadelParts)
		{
			parts.slot.selected = false;
		}
	}

    void Init()
    {
		while(0 < content.childCount)
        {
            Transform child = content.GetChild(0);
            child.SetParent(null);
            Object.Destroy(child.gameObject);
        }
		foreach(var itr in GameManager.Instance.citadel.heros)
        {
			HeroUnit unit = itr.Value;
			ContentHeroShop contentHeroShop = GameObject.Instantiate<ContentHeroShop>(contentPrefab);
			contentHeroShop.SetUnit(unit);
			contentHeroShop.transform.SetParent(content);
			contentHeroShop.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
        }
    }
}
