using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public static class ResourceManager
    {
        public static string defaultResourcePath;

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

        private static List<MemoryTexture> loadedTextures = new List<MemoryTexture>();

        public static Texture2D LoadTexture(string texPath)
        {
            if (string.IsNullOrEmpty(texPath))
            {
                return null;
            }
            int index = loadedTextures.FindIndex((MemoryTexture memTex) => memTex.path == texPath);
            if (-1 != index)
            { // If we have this texture in memory already, return it
                if (null == loadedTextures[index].texture)
                {
                    loadedTextures.RemoveAt(index);
                }
                else
                {
                    return loadedTextures[index].texture;
                }
            }
            // Else, load up the texture and store it in memory
            Texture2D tex = LoadResource<Texture2D>(texPath);
            AddTextureToMemory(texPath, tex);
            return tex;
        }

        public static T LoadResource<T>(string path) where T : UnityEngine.Object
        {
            if (Application.isPlaying) // At runtime
            {
                path = path.Substring(0, path.LastIndexOf('.'));
                return UnityEngine.Resources.Load<T>(path);
            }
#if UNITY_EDITOR
            // In the editor
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(defaultResourcePath + path);
#else
			return null;
#endif
        }

        public static void AddTextureToMemory(string texturePath, Texture2D texture, params string[] modifications)
        {
            if (texture == null)
            {
                return;
            }
            loadedTextures.Add(new MemoryTexture(texturePath, texture, modifications));
        }
    }


}