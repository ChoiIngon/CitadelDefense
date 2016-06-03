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
    
	public delegate void OnNetworkEvent();
    public delegate void OnErrorEvent(System.Exception e);

    public OnNetworkEvent onConnect;
    public OnNetworkEvent onReconnect;
    public OnNetworkEvent onClose;
    public OnErrorEvent onError;

    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        msgSEQ = 0;
        slowCount = 0;
        elapsedTime = 0.0f;
        minResponseTime = float.MaxValue;
        maxResponseTime = 0.0f;
        Connect(host, port);

        RegisterHandler(MsgSvrCli_Field_StressTest_Ans.MSG_ID, (Gamnet.Buffer buf) =>
        {
            MsgSvrCli_Field_StressTest_Ans ans = new MsgSvrCli_Field_StressTest_Ans();
            if(false == ans.Load(buf))
            {
                throw new System.Exception("MsgSvrCli_Field_StressTest_Ans() load fail");
            }

            float responseTime = Time.realtimeSinceStartup - ans.SendTime;
            if(minResponseTime > responseTime)
            {
                minResponseTime = responseTime;
            }

            if(maxResponseTime < responseTime)
            {
                maxResponseTime = responseTime;
            }

            if(slowTime < responseTime)
            {
                slowCount++;
            }
        });
    }

    bool stopSend = false;
    float stopTime = 0.0f;
    void Update()
    {
        base.Update();
        elapsedTime += Time.deltaTime;
        if(0 == Random.Range(0, 300))
        {
            stopSend = true;
            return;
        }

        stopTime += Time.deltaTime;    
        if (false == stopSend)
        {
            if(msgSendInterval > stopTime)
            {
                return;
            }
            MsgCliSvr_Field_StressTest_Req req = new MsgCliSvr_Field_StressTest_Req();
            req.MsgSEQ = msgSEQ++;
            req.SendTime = Time.realtimeSinceStartup;

            SendMsg(req);
            stopTime = 0.0f;
        }
        else
        {
            if(1.0f < stopTime)
            {
                stopSend = false;
                stopTime = 0.0f;
            }
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
}
