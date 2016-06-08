using UnityEngine;
using System.Collections;

public class UserInputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click, casting ray.");

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 20.0f, 1 << LayerMask.NameToLayer("Citadel"));
			if (null != hit.collider)
			{
                if("UnitSlot" == hit.collider.gameObject.tag)
                {
                    if(GameManager.State.Play == GameManager.Instance.state)
                    {
                        UnitSlot slot = hit.collider.gameObject.GetComponent<UnitSlot>();
                        Transform tr = slot.transform.GetChild(0);
                        UnitTurret unit = tr.GetComponent<UnitTurret>();
                    }
                    else if(GameManager.State.Lobby == GameManager.Instance.state)
                    {

                    }
                }
			}
		}
	}
}
