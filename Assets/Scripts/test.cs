using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    public Vector3 start;
    public Vector3 end;
	// Use this for initialization
	void Start () {
        Vector3 from = end - start;
        float angle = Vector3.Angle(from, new Vector3(from.x, 0.0f, 0.0f));
        if(end.y < start.y)
        {
            angle = 360 - angle;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
        Debug.Log(angle);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 from = end - start;
        float angle = Vector3.Angle(from, new Vector3(from.x, 0.0f, 0.0f));
        if (0.0f > from.y)
        {
            angle = 360 - angle;
        }
        if(0.0f > from.x)
        {
            angle = 180 - angle;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
    }
}
