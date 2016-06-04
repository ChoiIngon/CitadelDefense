using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {
    [SerializeField]
    public float progress
    {
        set
        {
            Vector3 scale = _progress.localScale;
            scale.x = value;
            if(0.0f > scale.x)
            {
                scale.x = 0.0f;
            }
            if(1.0f < scale.x)
            {
                scale.x = 1.0f;
            }
            _progress.localScale = scale;
        }
    }
    Transform _progress;
	// Use this for initialization
	void Start () {
        _progress = transform.FindChild("Progress");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
