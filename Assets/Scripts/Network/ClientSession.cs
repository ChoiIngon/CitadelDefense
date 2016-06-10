using UnityEngine;
using System.Collections;
using XXField;

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

        RegisterHandler(MsgSvrCli_Field_RoomData_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            MsgSvrCli_Field_RoomData_Ntf ntf = new MsgSvrCli_Field_RoomData_Ntf();
            if (false == ntf.Load(buf))
            {
                throw new System.Exception("load fail");
            }
            roomData = ntf.RoomData;
        });

        RegisterHandler(MsgSvrCli_Field_StartGame_Ntf.MSG_ID, (Gamnet.Buffer buf) =>
        {
            Debug.Log("MsgSvrCli_Field_StartGame_Ntf()");
            MsgSvrCli_Field_StartGame_Ntf ntf = new MsgSvrCli_Field_StartGame_Ntf();
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
    void Update()
    {
        base.Update();
        if (XX_FIELD_STATE_TYPE.XX_FIELD_STATE_PLAY == fieldData.State)
        {
            /*
            elapsedTime += Time.deltaTime;
            if (0 == Random.Range(0, 300))
            {
                stopSend = true;
                return;
            }

            stopTime += Time.deltaTime;
            if (false == stopSend)
            {
                if (msgSendInterval > stopTime)
                {
                    return;
                }
                MsgCliSvr_Field_MoveButtonDown_Ntf ntf = new MsgCliSvr_Field_MoveButtonDown_Ntf();
                SendMsg(ntf);
                stopTime = 0.0f;
            }
            else
            {
                if (1.0f < stopTime)
                {
                    stopSend = false;
                    stopTime = 0.0f;
                }
            }
            */
        }
    }
        
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
        if (ClientType.Host == clientType)
        {
            StartCoroutine(Send_CreateField_Req(accountInfo.AccountID));
        }
        else
        {
            StartCoroutine(Send_JoinField_Req(accountInfo.AccountID, fieldSEQ));
        }
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

    public IEnumerator Send_CreateField_Req(string sAccountID)
    {
        MsgCliSvr_Field_CreateField_Req req = new MsgCliSvr_Field_CreateField_Req();
        MsgSvrCli_Field_CreateField_Ans ans = null;
        req.AccountID = sAccountID;

        SendMsg<MsgSvrCli_Field_CreateField_Ans>(req, (Gamnet.Buffer buf) => {
            ans = new MsgSvrCli_Field_CreateField_Ans();
            if (false == ans.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Field_CreateField_Ans() load fail");
            }

            if (XX_ERROR_CODE.XX_ERROR_SUCCESS != ans.ErrorCode)
            {
                return;
            }
            accountInfo = ans.AccountInfo;
            fieldInfo = ans.FieldInfo;
            fieldData = ans.FieldData;
            playerStatData = ans.PlayerStatData;
        }, 300);
        while (null == ans)
        {
            yield return null;
        }
        Debug.Log("MsgSvrCli_Field_CreateField_Ans");
        MsgCliSvr_Field_ReadyGame_Ntf ntf = new MsgCliSvr_Field_ReadyGame_Ntf();
        SendMsg(ntf);
    }

    public IEnumerator Send_JoinField_Req(string sAccountID, uint fieldSEQ)
    {
        MsgCliSvr_Field_JoinField_Req req = new MsgCliSvr_Field_JoinField_Req();
        MsgSvrCli_Field_JoinField_Ans ans = null;
        req.AccountID = sAccountID;
        req.FieldSEQ = fieldSEQ;

        SendMsg<MsgSvrCli_Field_JoinField_Ans>(req, (Gamnet.Buffer buf) =>
        {
            ans = new MsgSvrCli_Field_JoinField_Ans();
            if (false == ans.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Raid_JoinRoom_Ans() load fail");
            }

            if (XX_ERROR_CODE.XX_ERROR_SUCCESS != ans.ErrorCode)
            {
                return;
            }

            accountInfo = ans.AccountInfo;
            fieldInfo = ans.FieldInfo;
            fieldData = ans.FieldData;
            playerStatData = ans.PlayerStatData;

        }, 300);
        while (null == ans)
        {
            yield return null;
        }
        Debug.Log("MsgSvrCli_Field_JoinField_Ans");
        MsgCliSvr_Field_ReadyGame_Ntf ntf = new MsgCliSvr_Field_ReadyGame_Ntf();
        SendMsg(ntf);
    }
}
