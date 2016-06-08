using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ClientSession))]
public class ClientSessionEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ClientSession session = (ClientSession)target;

        if (EditorApplication.isPlaying)
        {
            if (ClientSession.State.Connected != session.state)
            {
                if (GUILayout.Button("Connect"))
                {
                    session.Connect(session.host, session.port);
                }
            }
            else
            {
                if (GUILayout.Button("Disconnect"))
                {
                    session.Close();
                }
            }
        }
	}
	
}
