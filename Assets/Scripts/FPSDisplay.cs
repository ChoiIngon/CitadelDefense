using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour {
    float deltaTime = 0.0f;
	void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}
    void OnGUI()
    {

        GUIStyle style = new GUIStyle();
        
        Rect rect = new Rect(0, 0, Screen.width, Screen.height * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = (int)rect.height;
        style.normal.textColor = Color.white;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);

        style.normal.textColor = Color.black;
        rect.x += 1;
        rect.y += 1;
        rect.width -= 1;
        rect.height -= 1;
        GUI.Label(rect, text, style);
    }
}
