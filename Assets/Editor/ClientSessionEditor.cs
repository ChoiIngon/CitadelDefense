using UnityEngine;
using UnityEditor;
using System.Collections;
using XXField;
using XXData;

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

                if (GUILayout.Button("Create Field"))
                {
                    session.CreateField();
                }
            }
            
            if (XX_FIELD_STATE_TYPE.XX_FIELD_STATE_PLAY == session.fieldData.State)
            {
                if (GUILayout.Button("Move"))
                {
         
                }

                if (GUILayout.Button("Jump"))
                {
                }
            }
        }
	}
	
}
