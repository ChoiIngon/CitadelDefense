using UnityEngine;
using System.Collections;

public class TestAnimationChange : Unit {
    public AnimationClip moveAnim;
	// Use this for initialization
	void Start () {
        base.Start();
        unitAnimation.ChangeAnimationClip("unit_knight_move", moveAnim);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
