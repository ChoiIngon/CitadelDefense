using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Cube : NetworkBehaviour
{
    public GameObject prefab;
    // Use this for initialization
    
    void Start()
    {
        
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * 0.1f;
        var y = Input.GetAxis("Vertical") * 0.1f;

        transform.Translate(x, y, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cmd_Spawn();
        }
    }

    [Command]
    void Cmd_Spawn()
    {
        GameObject obj = (GameObject)Instantiate(prefab);
        obj.transform.position = transform.position;
        NetworkServer.Spawn(obj);
    }

    
}
