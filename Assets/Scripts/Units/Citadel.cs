using UnityEngine;
using System.Collections;

public class Citadel : MonoBehaviour {
    public AutoRecoveryInt hp;
    public ProgressBar healthBar;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.progress = (float)hp.value / (float)hp.max;
	}
}
