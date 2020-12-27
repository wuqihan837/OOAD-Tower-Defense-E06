using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    private static Paths instance;
    private void Awake()
    {
        instance = this;
    }

    public static Paths GetInstance()
    {
        return instance;
    }

    public Path[] allPaths;
    [System.Serializable]
    public class Path
    {
        public Transform start;
        public Transform[] onePath;
    }
}


