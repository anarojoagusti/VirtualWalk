using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<myClass> : MonoBehaviour where myClass : MonoBehaviour
{
    protected static myClass _instance;
    public static myClass instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<myClass>();

                if (_instance == null)
                {
                    _instance = new GameObject(typeof(myClass).Name).AddComponent<myClass>();
                }
            }
            return _instance;
        }
    }
}