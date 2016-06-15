using UnityEngine;
using System.Collections;

public class Effect_Smoke : Effect {
    public float emissionRate;

    [HideInInspector]
    public ParticleSystem smoke;
	// Use this for initialization
	void Start () {
        smoke = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        smoke.emissionRate = emissionRate;
	}
}
