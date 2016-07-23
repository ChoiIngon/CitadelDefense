using UnityEngine;
using System.Collections;

public class UnitMove : MonoBehaviour {
	public float altitude;
	public float speed {
		set { _speed = value; }
		get {
			if (null != buff) {
				float tmp = _speed;
				buff (ref tmp, _speed);
				return tmp;
			}
			return _speed;
		}
	}
	[SerializeField]
	private float _speed;
	[HideInInspector]
	public Vector3 start = Vector3.zero;
	[HideInInspector]
	public Vector3 end = Vector3.zero;
    public float interpolate
    {
        get { return _interpolate; }
    }

    protected float _interpolate;
    protected float distance = 0.0f;
	public delegate void BuffSpeed(ref float ret, float originalSpeed);
	public BuffSpeed buff;
	public virtual void Init(Vector3 end, float altitude)
	{
		this.start = transform.position;
		this.end = end;
		this.altitude = altitude;
		distance = Vector3.Distance (start, end);
		_interpolate = 0.0f;
        enabled = true;
		if (0.0f >= distance) {
			_interpolate = 1.00001f;
		}
	}
}
