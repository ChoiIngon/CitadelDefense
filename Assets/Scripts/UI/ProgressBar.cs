using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {
    public float progress
    {
        set
        {
            float scale = value;
            if(0.0f > scale)
            {
                scale = 0.0f;
            }
            if(1.0f < scale)
            {
                scale = 1.0f;
            }
			if (null == _progress) {
				return;
			}
            _progress.localScale = new Vector3(scale, _progress.localScale.y, _progress.localScale.z);
        }
    }
    Transform _progress;
	// Use this for initialization
	void Start () {
        _progress = transform.FindChild("Progress");
	}
}
