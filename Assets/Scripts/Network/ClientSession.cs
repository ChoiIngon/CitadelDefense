using UnityEngine;
using System.Collections;
using XXField;
using XXData;
using XXMessage;
using XXError;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ClientSession : Gamnet.StreamSession
{
    public string host;
    public int port;
    public uint msgSEQ;
    public float msgSendInterval;
    public int slowCount;
    public float slowTime;
    public float elapsedTime;
    public float minResponseTime;
    public float maxResponseTime;
    public uint fieldSEQ;

	public delegate void OnNetworkEvent();
    public delegate void OnErrorEvent(System.Exception e);

    public OnNetworkEvent onConnect;
    public OnNetworkEvent onReconnect;
    public OnNetworkEvent onClose;
    public OnErrorEvent onError;

    public XXAccountInfo accountInfo;
    public XXFieldInfo fieldInfo;
    public XXFieldData fieldData;
    public XXPlayerStatData playerStatData;
    public XXRoomData roomData;

    public enum ClientType
    {
        Host,
        Guest
    }
    public ClientType clientType;

    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        msgSEQ = 0;
        slowCount = 0;
        elapsedTime = 0.0f;
        minResponseTime = float.MaxValue;
        maxResponseTime = 0.0f;

        RegisterHandler(MsgSvrCli_Field_PlayerStatData_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            Debug.Log("MsgSvrCli_Field_PlayerStatData_Ntf()");
            MsgSvrCli_Field_PlayerStatData_Ntf ntf = new MsgSvrCli_Field_PlayerStatData_Ntf();
            if (false == ntf.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Field_PlayerStatData_Ntf() load fail");
            }
            for (int i = 0; i < fieldData.PlayerStatDatas.Count; i++)
            {
                XXPlayerStatData statData = fieldData.PlayerStatDatas[i];
                if (ntf.PlayerStatData.UserSEQ == statData.UserSEQ)
                {
                    fieldData.PlayerStatDatas[i] = ntf.PlayerStatData;
                    break;
                }
            }
        });

        RegisterHandler(MsgSvrCli_Field_PlayerStatDatas_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            Debug.Log("MsgSvrCli_Field_PlayerStatDatas_Ntf()");
            MsgSvrCli_Field_PlayerStatDatas_Ntf ntf = new MsgSvrCli_Field_PlayerStatDatas_Ntf();
            if (false == ntf.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Field_PlayerStatDatas_Ntf() load fail");
            }
            fieldData.PlayerStatDatas = ntf.PlayerStatDatas;
        });

        RegisterHandler(MsgSvrCli_Field_InitRoomData_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            Debug.Log("MsgSvrCli_Field_InitRoomData_Ntf()");
            MsgSvrCli_Field_InitRoomData_Ntf ntf = new MsgSvrCli_Field_InitRoomData_Ntf();
            if (false == ntf.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Field_InitRoomData_Ntf() load fail");
            }

            roomData = ntf.RoomData;

            MsgCliSvr_Field_CompleteRoomData_Ntf ntfToSvr = new MsgCliSvr_Field_CompleteRoomData_Ntf();
            SendMsg(ntfToSvr);
        });

        RegisterHandler(XXMsgSvrCli_Map_RoomData_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            XXMsgSvrCli_Map_RoomData_Ntf ntf = new XXMsgSvrCli_Map_RoomData_Ntf();
            if (false == ntf.Load(buf))
            {
                throw new System.Exception("load fail");
            }
        });

        RegisterHandler(XXMsgSvrCli_Map_StartGame_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            Debug.Log("MsgSvrCli_Field_StartGame_Ntf()");
            XXMsgSvrCli_Map_StartGame_Ntf ntf = new XXMsgSvrCli_Map_StartGame_Ntf();
            if (false == ntf.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Raid_StartGame_Ntf() load fail");
            }
            fieldData.State = XX_FIELD_STATE_TYPE.XX_FIELD_STATE_PLAY;
        });

        RegisterHandler(MsgSvrCli_Field_Kickout_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            Debug.Log("MsgSvrCli_Field_Kickout_Ntf()");
            MsgSvrCli_Field_Kickout_Ntf ntf = new MsgSvrCli_Field_Kickout_Ntf();
            if (false == ntf.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Raid_StartGame_Ntf() load fail");
            }
            Close();
        });

        RegisterHandler(MsgSvrCli_Field_PlayerData_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            Debug.Log("MsgSvrCli_Field_MoveButtonDown_Ntf()");
            MsgSvrCli_Field_PlayerData_Ntf ntf = new MsgSvrCli_Field_PlayerData_Ntf();
            if (false == ntf.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Raid_StartGame_Ntf() load fail");
            }
        });
    }

    bool stopSend = false;
    float stopTime = 0.0f;
        
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Msg SEQ"), GUILayout.Width(150));
        GUILayout.Label(new GUIContent(msgSEQ.ToString()));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Slow Count"), GUILayout.Width(150));
        GUILayout.Label(new GUIContent(slowCount.ToString()));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Elapsed Time"), GUILayout.Width(150));
        GUILayout.Label(new GUIContent(elapsedTime.ToString()));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Min Response Time"), GUILayout.Width(150));
        GUILayout.Label(new GUIContent(minResponseTime.ToString()));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Max Response Time"), GUILayout.Width(150));
        GUILayout.Label(new GUIContent(maxResponseTime.ToString()));
        GUILayout.EndHorizontal();
    }

    public override void OnConnect()
    {
        StartCoroutine(Send_Login(accountInfo.AccountID));
    }
    public override void OnReconnect()
    {
    }
    public override void OnClose()
    {
    }
    public override void OnError(System.Exception e)
    {
    }

    public void CreateField()
    {
        StartCoroutine(Send_CreateField_Req());
    }
    public IEnumerator Send_Login(string userID)
    {
        XXMsgCliSvr_User_Login_Req req = new XXMsgCliSvr_User_Login_Req();
        XXMsgSvrCli_User_Login_Ans ans = null;
        req.AccountID = userID;
        req.AccountType = XX_ACCOUNT_TYPE.XX_ACCOUNT_GUEST;
        req.AccessToken = "OLujVj5GzG2ipazmuVKAG1SDUR2CV3PEWGv8720zW1aJE7/yDFiNvrulIrT1/8O9RNW53KTChHT7ERvn0PuiRczvxPBboR0qBMDu7d7D4X4=";
        req.LoginType = XX_LOGIN_TYPE.XX_LOGIN_ACCESS_TOKEN;

        SendMsg<XXMsgSvrCli_User_Login_Ans>(req, (Gamnet.Buffer buf) => {
            ans = new XXMsgSvrCli_User_Login_Ans();
            if (false == ans.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Field_CreateField_Ans() load fail");
            }

            if (XX_ERROR_CODE.XX_ERROR_SUCCESS != ans.Error.Code)
            {
                return;
            }
            accountInfo = ans.AccountInfo;
        }, 300);
        while (null == ans)
        {
            yield return null;
        }
        Debug.Log("MsgSvrCli_Field_CreateField_Ans");
    }
    public IEnumerator Send_CreateField_Req()
    {
        XXMsgCliSvr_Map_CreateField_Req req = new XXMsgCliSvr_Map_CreateField_Req();
        XXMsgSvrCli_Map_CreateField_Ans ans = null;

        req.RoomGroupIDs.Add("ROOM_GROUP_1");
        req.RoomGroupIDs.Add("ROOM_GROUP_2");
        req.RoomGroupIDs.Add("ROOM_GROUP_5");
        req.RoomGroupIDs.Add("ROOM_GROUP_6");

        SendMsg<XXMsgSvrCli_Map_CreateField_Ans>(req, (Gamnet.Buffer buf) => {
            ans = new XXMsgSvrCli_Map_CreateField_Ans();
            if (false == ans.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Field_CreateField_Ans() load fail");
            }

            if (XX_ERROR_CODE.XX_ERROR_SUCCESS != ans.Error.Code)
            {
                return;
            }
        }, 300);
        while (null == ans)
        {
            yield return null;
        }
        Debug.Log("MsgSvrCli_Field_CreateField_Ans");
        XXMsgCliSvr_Map_ReadyGame_Ntf ntf = new XXMsgCliSvr_Map_ReadyGame_Ntf();
        SendMsg(ntf);
    }
    public IEnumerator Send_JoinField_Req(uint fieldSEQ)
    {
        XXMsgCliSvr_Map_JoinField_Req req = new XXMsgCliSvr_Map_JoinField_Req();
        XXMsgSvrCli_Map_JoinField_Ans ans = null;
        req.FieldSEQ = fieldSEQ;

        SendMsg<XXMsgSvrCli_Map_JoinField_Ans>(req, (Gamnet.Buffer buf) =>
        {
            ans = new XXMsgSvrCli_Map_JoinField_Ans();
            if (false == ans.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Raid_JoinRoom_Ans() load fail");
            }

            if (XX_ERROR_CODE.XX_ERROR_SUCCESS != ans.Error.Code)
            {
                return;
            }
        }, 300);
        while (null == ans)
        {
            yield return null;
        }
        Debug.Log("MsgSvrCli_Field_JoinField_Ans");
        XXMsgCliSvr_Map_ReadyGame_Ntf ntf = new XXMsgCliSvr_Map_ReadyGame_Ntf();
        SendMsg(ntf);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ClientSession))]
public class ClientSessionEditor : Editor
{
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
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Move Left"))
                {

                }

                if (GUILayout.Button("Idle"))
                {

                }

                if (GUILayout.Button("Move Right"))
                {

                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Jump"))
                {
                }
            }
        }
    }
}
#endif