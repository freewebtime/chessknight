using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorUtils
{
    [MenuItem("Assets/Create/Scriptable Object")]
    public static void CreateScriptableObject()
    {
        Debug.Log("Create scriptable object called");
    }
}
