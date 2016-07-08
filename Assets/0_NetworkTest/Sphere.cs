using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sphere : NetworkBehaviour {

	// Use this for initialization
	public override void OnStartServer () {
        float x = Random.Range(1.0f, 3.0f);
        iTween.MoveBy(gameObject, iTween.Hash("x", x, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
    }
}
