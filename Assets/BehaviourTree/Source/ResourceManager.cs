using UnityEngine;
using System.Collections;

public static class ResourceManager {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public class MemoryTexture
    {
        public string path;
        public Texture2D texture;
        public string[] modifications;

        public MemoryTexture(string texPath, Texture2D tex, params string[] mods)
        {
            path = texPath;
            texture = tex;
            modifications = mods;
        }
    }
}
