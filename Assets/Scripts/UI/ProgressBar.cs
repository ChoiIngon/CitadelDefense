using UnityEngine;
using UnityEngine.UI;

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
    public string text
    {
        set
        {
            transform.Find("Text").GetComponent<Text>().text = value;
        }
    }

    Transform _progress;
    Text _text;
    // Use this for initialization
    void Awake()
    {
        Util.EventSystem.Subscribe<GameManager>(EventID.GameStart, Init);
    }

    void Init(GameManager gameManager)
    {
        Debug.Log("init progress instance(name:" + gameObject.name + ")");
        _progress = transform.Find("Progress");
        if(null == _progress)
        {
            throw new System.Exception("can not find 'Progress' component");
        }
        /*
        Transform t = transform.Find("Text");
        if(null == t)
        {
            throw new System.Exception("can not find 'Text' component");
        }
        _text = t.GetComponent<Text>();
        if (null == _text)
        {
            throw new System.Exception("can not find 'Text' component");
        }
        */
    }
}
