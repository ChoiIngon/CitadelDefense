using UnityEngine;
using UnityEditor;
using System.Collections;
using XXField;

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
            /*
            if (XX_FIELD_STATE_TYPE.XX_FIELD_STATE_WAIT == session.fieldData.State)
            {
                if (GUILayout.Button("Ready"))
                {
                    session.Send_ReadyGame_Ntf();
                }
            }
            */
        }
	}
	
}
