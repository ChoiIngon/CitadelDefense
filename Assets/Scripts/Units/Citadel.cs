using UnityEngine;
using System.Collections;

public class Citadel : MonoBehaviour {
    public AutoRecoveryInt hp;
	// Use this for initialization
	void Start () {
        hp = new AutoRecoveryInt();
        hp.max = 100;
        hp.value = 10;
        hp.interval = 2;
        hp.recovery = 1;
        hp.time = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log((int)hp);
	}
}
