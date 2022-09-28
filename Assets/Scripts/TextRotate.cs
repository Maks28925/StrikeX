using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotate : MonoBehaviour
{
    [Header("GameObjects")]
    private Transform target;
    public GameObject MainCamera;
       

    void Start()
    {
        target = MainCamera.transform;
    }


    void Update()
    {
        transform.LookAt(target);
    }
}
