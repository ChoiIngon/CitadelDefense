using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	// Use this for initialization
	void Start () {
        iTween.MoveBy(gameObject, new Vector3(5, 0, 0), 10);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	public void AnimationEvent(AnimationEvent e)
	{
        
	}
}
