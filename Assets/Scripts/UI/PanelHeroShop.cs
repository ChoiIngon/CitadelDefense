﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelHeroShop : MonoBehaviour {
	public ContentHeroShop contentPrefab;
	public Transform content;

	void Start () 
	{
        Init();
	}

    void OnEnable()
    {
        Init();
    }

	void OnDisable()
	{
		foreach(UnitSlot slot in GameManager.Instance.citadel.slots)
		{
			slot.Select(false);
		}
	}

    void Init()
    {
		while(0 < content.childCount)
        {
            Transform child = content.GetChild(0);
            child.SetParent(null);
            DestroyObject(child.gameObject);
        }
        foreach(HeroUnit unit in GameManager.Instance.heros)
        {
			ContentHeroShop contentHeroShop = GameObject.Instantiate<ContentHeroShop>(contentPrefab);
			contentHeroShop.SetUnit(unit);
			contentHeroShop.transform.SetParent(content);
			contentHeroShop.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
        }
    }
}