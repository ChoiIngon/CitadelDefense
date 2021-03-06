﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}