using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HeroPanelElement : MonoBehaviour {
	public UnitHero hero;
	public HeroInfoPanel heroInfoPanel;

	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
			GameManager.Instance.heroInfoPanel.unit = hero;
			hero.ShowInfo(GameManager.Instance.heroInfoPanel);
			GameManager.Instance.heroInfoPanel.gameObject.SetActive(true);
        });
	}

	public void SetUnitHero(UnitHero hero)
	{
		this.hero = hero;
		{
			//Transform t = transform.FindChild ("Portrait");
			//Image portrait = t.GetComponent<Image> ();
			//portrait.sprite = hero.unitSprite.sprite;
		}
		{
			//Transform t = transform.FindChild ("Skill");
		}
		{
			Transform t = transform.FindChild ("Name");
            Text text = t.GetComponent<Text>();
            text.text = hero.name;
		}
		{
			Transform t = transform.FindChild ("Equip");
			if(0 != hero.slotIndex)
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
