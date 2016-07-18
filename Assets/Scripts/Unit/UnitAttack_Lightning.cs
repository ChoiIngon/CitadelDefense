using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAttack_Lightning : UnitAttack {
	public ProceduralLightning lightningPrefab;
	public override void Attack() {
		if (null == target) {
			return;
		}
		int count = Random.Range (3, 5);
		for (int i = 0; i < count; i++) {
			ProceduralLightning lightning = GameObject.Instantiate<ProceduralLightning> (lightningPrefab);
			lightning.start = transform.position;
			lightning.end = target.transform.position;
			lightning.sparkWidth = Random.Range (0.01f, 0.05f);
			lightning.transform.SetParent (transform);
		}

		Damage (target);
	}

	public void Update()
	{
		if (null == target) {
			return;
		}
		for (int i = 0; i < transform.childCount; i++) {
			ProceduralLightning lightning = transform.GetChild (i).GetComponent<ProceduralLightning> ();
			lightning.end = target.transform.position;
		}
	}
}
