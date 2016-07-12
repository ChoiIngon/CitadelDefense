using UnityEngine;
using System.Collections;

public class UnitAttack_Lightning : UnitAttack {
	// Use this for initialization
	public Vector3 start;
	public Vector3 end;
	private float distance;
	private float angle;
	public Sprite sprite;
	public Vector3[] points;
	public float lifeTime;
	void Start () {
		//SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		//spriteRenderer.sprite = sprite;
		//ebug.Log (spriteRenderer.sprite.rect.size / spriteRenderer.sprite.pixelsPerUnit);

		int pointCount = (int)distance * Random.Range (1, 3);
		//points = new Vector3 [pointCount];
		distance = Vector3.Distance (start, end);

		LineRenderer lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.sortingLayerName = "Effect";

		points = new Vector3 [21];
		Split (0, 20, 0, 10);
		lineRenderer.SetVertexCount (21);
		lineRenderer.SetPositions (points);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Attack()
	{
		
	}

	private void Split(int start, int end, float startPos, float endPos)
	{
		int center = start + (end - start)/ 2; 
		if (0 == center || points.Length - 1 == center) {
			return;
		}
		points[center].y = Mathf.Lerp(startPos, endPos, Random.Range(0.4f, 0.6f));
		Debug.Log ("index:" + center + ", start:" + startPos + ", end:" + endPos + ", pos:" + points [center].y);
		if (0 == (end - start) / 2) {
			return;
		}
		Split(start, center , startPos, points[center].y);
		Split(center + 1, end, points[center].y, endPos);
	}
}
