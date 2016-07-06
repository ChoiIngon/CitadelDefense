using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public UnitHit hitPrefab;

	[HideInInspector]
	public float power;

	protected UnitMove move;
	private Vector3 start;
	private Vector3 end;
	// Use this for initialization
	public void Init (Vector3 start, Vector3 end, float power) {
		this.start = start;
		this.end = end;
		move = GetComponent<UnitMove> ();
		move.Init (start, end);
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
	}
}
