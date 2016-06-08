using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HeroPanelElement : MonoBehaviour {
	public UnitHero hero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetUnitHero(UnitHero hero)
	{
		this.hero = hero;
		{
			Transform t = transform.FindChild ("Portrait");
			Image portrait = t.GetComponent<Image> ();
			portrait.sprite = hero.unitSprite.sprite;
		}
		{
			Transform t = transform.FindChild ("Skill");
		}
		{
			Transform t = transform.FindChild ("Name");
		}
		{
			Transform t = transform.FindChild ("Equip");
		}
		{
			Transform t = transform.FindChild ("Level");
		}
		{
			Transform t = transform.FindChild ("Price");
		}
	}
}
