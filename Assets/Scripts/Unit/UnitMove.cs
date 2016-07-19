using UnityEngine;
using System.Collections;

public class UnitMove : MonoBehaviour {
	public float speed;
	//[HideInInspector]
	public Vector3 start = Vector3.zero;
	//[HideInInspector]
	public Vector3 end = Vector3.zero;
    public float interpolate
    {
        get { return _interpolate; }
    }

    protected float _interpolate;
    protected float distance = 0.0f;
	public delegate float BuffSpeed(float interpolate);
	public BuffSpeed buff;
	public virtual void Init(Vector3 start, Vector3 end)
	{
		transform.position = start;
		this.start = start;
		this.end = end;
		distance = Vector3.Distance (start, end);
		_interpolate = 0.0f;
		if (0.0f >= distance) {
			_interpolate = 1.00001f;
		}
	}
}
