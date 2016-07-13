using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class UnitAttack_Lightning : UnitAttack {
	// Use this for initialization
	public Vector3 start;
	public Vector3 end;
    public float sparkTime;
    public float width;
	private Vector3[] points;

	void Start () {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Effect";
        StartCoroutine(DrawLightning());
    }

    // Update is called once per frame
    void Update () {
	
	}

    IEnumerator DrawLightning()
    {
        //for(int i=0; i<20; i++)
        while(true)
        {
            float distance = Vector3.Distance(start, end);
            int verticeCount = (int)distance * Random.Range(1, 4) + 3;
            points = new Vector3[verticeCount];
            points[0] = start;
            points[verticeCount - 1] = end;

            SetVertices(0, verticeCount - 1, start, end);

            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetVertexCount(verticeCount);
            lineRenderer.SetPositions(points);
            lineRenderer.SetWidth(width, width);
            yield return new WaitForSeconds(sparkTime);
        }
    }
	public override void Attack()
	{
		
	}

	private void SetVertices(int start, int end, Vector3 startPos, Vector3 endPos)
	{
		int center = start + (end - start)/ 2; 
		if (0 == center || points.Length - 1 == center) {
			return;
		}
		Vector3 point = Vector3.Lerp(startPos, endPos, Random.Range(0.4f, 0.6f));
        float displacement = Vector3.Distance(startPos, endPos) / 10;
        points[center] = new Vector3(point.x + Random.Range(-displacement, displacement), point.y + Random.Range(-displacement, displacement), point.z);
		if (0 == (end - start) / 2) {
			return;
		}
        SetVertices(start, center , startPos, points[center]);
        SetVertices(center + 1, end, points[center], endPos);
	}
}
