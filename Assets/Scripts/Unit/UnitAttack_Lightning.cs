using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAttack_Lightning : UnitAttack {
	public ProceduralLightning lightningPrefab;
    public Effect sparkPrefab;
    public float interval;
    private float deltaTime;

    public override void Attack() {
		if (null == target) {
			return;
		}
        deltaTime = 0.0f;

        int count = Random.Range (3, 5);
		for (int i = 0; i < count; i++) {
			ProceduralLightning lightning = GameObject.Instantiate<ProceduralLightning> (lightningPrefab);
			lightning.start = transform.position;
			lightning.end = target.transform.position;
            lightning.sparkCount = (int)(data.time/interval);
			lightning.sparkWidth = Random.Range (0.01f, 0.05f);
            lightning.sparkDuration = interval;
			lightning.transform.SetParent (transform);
		}

        StartCoroutine(Lightning());
	}

    IEnumerator Lightning()
    {
        while (null != target && deltaTime <= data.time)
        {
            Damage(target);
            Effect spark = GameObject.Instantiate<Effect>(sparkPrefab);
            spark.transform.position = target.transform.position;
            yield return new WaitForSeconds(interval);
        }
    }
	public void Update()
	{
        deltaTime += Time.deltaTime;
		if (null == target || deltaTime > data.time) {
            return;
		}
		for (int i = 0; i < transform.childCount; i++) {
			ProceduralLightning lightning = transform.GetChild (i).GetComponent<ProceduralLightning> ();
			lightning.end = target.transform.position;
		}
	}
}
