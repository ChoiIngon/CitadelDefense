using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HeroPanelElement : MonoBehaviour {
	public UnitHero hero;

	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {

        });
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
			//portrait.sprite = hero.unitSprite.sprite;
		}
		{
			Transform t = transform.FindChild ("Skill");
		}
		{
			Transform t = transform.FindChild ("Name");
            Text text = t.GetComponent<Text>();
            text.text = hero.name;
		}
		{
			Transform t = transform.FindChild ("Equip");
            if(true == hero.equiped)
            {
                t.gameObject.SetActive(true);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
		}
		{
			Transform t = transform.FindChild ("Level");
            Text text = t.GetComponent<Text>();
            text.text = hero.level.ToString();
		}
		{
			Transform t = transform.FindChild ("Price");
            if (false == hero.purchased)
            {
                Text text = t.GetComponent<Text>();
                text.text = hero.price.ToString();
            }
            else
            {
                t.gameObject.SetActive(false);
            }
		}
	}
}
