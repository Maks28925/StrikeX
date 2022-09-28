using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    [Header("Variables")]
    public static float fps;


    void Start()
    {
        Application.targetFrameRate = 120;
        Screen.SetResolution(1280, 800, true);
    }


    void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        GUILayout.Label("FPS: " + (int)fps);
    }
}