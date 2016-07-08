using UnityEngine;
using System.Collections;

public class UIHostName : MonoBehaviour {
    System.Net.IPAddress[] addr;
    // Use this for initialization
    void Start () {
        string hostName = System.Net.Dns.GetHostName();
        System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostName);
        addr = ipEntry.AddressList;
    }
	
	void OnGUI()
    {
        for (int i = 0; i < addr.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent(addr[i].ToString()), GUILayout.Width(150));
            GUILayout.EndHorizontal();
        }
    }
}
