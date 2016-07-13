using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(LineRenderer))]
public class ProceduralLightning : MonoBehaviour {
	// Use this for initialization
	public Vector3 start;
	public Vector3 end {
		set {
			_end = value;
			if (null == vertices) {
				return;
			}
			vertices [vertices.Length - 1] = _end;
			lineRenderer.SetPosition(vertices.Length - 1, _end);
		}
		get {
			return _end;
		}
	}
	public Vector3 _end;
	public int sparkCount = 0; // if 0, infinity
	public float sparkDuration = 0.1f;
	public float sparkWidth = 0.1f;

	private Vector3[] vertices;
	private LineRenderer lineRenderer
	{
		get { 
			if(null == _lineRenderer)
			{
				_lineRenderer = GetComponent<LineRenderer>();
			}
			return _lineRenderer;
		}
	}
	private LineRenderer _lineRenderer;

	[SerializeField]
	public string sortingLayerName {
		get {
			return lineRenderer.sortingLayerName;
		}
		set {
			lineRenderer.sortingLayerName = value;
		}
	}
	[SerializeField]
	public int sortingOrder {
		get {
			return lineRenderer.sortingOrder;
		}
		set {
			lineRenderer.sortingOrder = value;
		}
	}
	[SerializeField]
	public int sortingLayerID {
		get {
			return lineRenderer.sortingLayerID;
		}
		set {
			lineRenderer.sortingLayerID = value;
		}
	}

	void Start () {
		StartCoroutine(DrawLightning());
	}
		
	private IEnumerator DrawLightning()
	{
		int increment = (0 == sparkCount ? 0 : 1);
		int loopCount = Mathf.Max (sparkCount, 1);
		for(int i=0; i<loopCount; i += increment)
		{
			float distance = Vector3.Distance(start, end);
			int verticeCount = (int)distance * Random.Range(1, 4) + 3;
			vertices = new Vector3[verticeCount];
			vertices[0] = start;
			vertices[verticeCount - 1] = end;

			SetVertices(0, verticeCount - 1, start, end);

			LineRenderer lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.SetVertexCount(verticeCount);
			lineRenderer.SetPositions(vertices);
			lineRenderer.SetWidth(sparkWidth, sparkWidth);
			yield return new WaitForSeconds(sparkDuration);
		}
		transform.SetParent (null);
		DestroyObject (gameObject);
	}

	private void SetVertices(int start, int end, Vector3 startPos, Vector3 endPos)
	{
		int center = start + (end - start)/ 2; 
		if (0 == center || vertices.Length - 1 == center) {
			return;
		}
		Vector3 point = Vector3.Lerp(startPos, endPos, Random.Range(0.4f, 0.6f));
		float displacement = Vector3.Distance(startPos, endPos) / 10;
		vertices[center] = new Vector3(point.x + Random.Range(-displacement, displacement), point.y + Random.Range(-displacement, displacement), point.z);
		if (0 == (end - start) / 2) {
			return;
		}
		SetVertices(start, center , startPos, vertices[center]);
		SetVertices(center + 1, end, vertices[center], endPos);
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(ProceduralLightning))]
public class CustomInspector : Editor {
	public override void OnInspectorGUI () 
	{
		base.OnInspectorGUI();
		ProceduralLightning lightning = (ProceduralLightning)target;
		// 레이블과 슬라이드
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Sorting Layer Name");
		lightning.sortingLayerName = EditorGUILayout.TextField (lightning.sortingLayerName);
		EditorGUILayout.EndHorizontal();
	}
}
#endif