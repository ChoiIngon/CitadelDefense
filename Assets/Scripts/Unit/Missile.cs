using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	[HideInInspector]
	public float power;
	[HideInInspector]
	public Unit target;
	public UnitHit hitPrefab;
	protected UnitMove move;
	// Use this for initialization
	public void Init (Vector3 start, Unit target, float power) {
		move = GetComponent<UnitMove> ();
		move.Init (start, target.transform.position);
		this.target = target;
		this.power = power;
	}

	void Start()
	{
		move = GetComponent<UnitMove> ();
	}

	// Update is called once per frame
	void Update () {
		if (1.0f <= move.interpolate)
		{
			UnitHit hit = GameObject.Instantiate<UnitHit>(hitPrefab);
			hit.Init (move.end, power);
			DestroyObject (gameObject);
		}
	}}
