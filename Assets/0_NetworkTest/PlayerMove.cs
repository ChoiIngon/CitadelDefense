using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerMove : NetworkBehaviour
{
    public GameObject spherePrefab;
    [SyncVar]public GameObject sphere;
    public int spawnCount = 0;
    
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * 0.1f;
        var z = Input.GetAxis("Vertical") * 0.1f;

        transform.Translate(x, 0, z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdSpawnSphere();
        }
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    [Command]
    void CmdSpawnSphere()
    {
        GameObject obj = GameObject.Instantiate<GameObject>(spherePrefab);
        obj.transform.position = transform.position;
        spawnCount++;
        //NetworkServer.Spawn(obj);
        sphere = obj;
        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Spawn Count : "), GUILayout.Width(150));
        GUILayout.Label(new GUIContent(spawnCount.ToString()));
        GUILayout.EndHorizontal();
    }

}