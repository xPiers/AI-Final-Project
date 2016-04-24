﻿using UnityEngine;
using System.Collections;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>{

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject (typeof (T).ToString (), typeof (T)).GetComponent<T> ();
                instance.Init ();
            }

            return instance;
        }
    }

    public virtual void Init ()
    {

    }

    private void Awake ()
    {
        if (instance == null)
            instance = this as T;
        Init ();
    }
}
