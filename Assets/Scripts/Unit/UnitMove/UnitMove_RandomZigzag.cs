using UnityEngine;
using System.Collections;

public class UnitMove_RandomZigzag : UnitMove {
	RandomZigzagPath path;
	public override void Init(Vector3 end, float altitude = 0.0f)
	{
		base.Init (end, altitude);
		path = new RandomZigzagPath (transform.position, end);
		StartCoroutine (Move ());
	}
		
	IEnumerator Move()
	{
		for (int i = 1; i < path.vertices.Length; i++) {
			float interpolate = 0.0f;
			Vector3 position = transform.position;
			distance = Vector3.Distance (position, path.vertices [i]);
			while (1.0f >= interpolate) {
				interpolate += speed / distance * Time.deltaTime;
				transform.position = Vector3.Lerp (position, path.vertices [i], interpolate);
				yield return null;
			}
		}
		_interpolate = 1.0f;
	}
}
