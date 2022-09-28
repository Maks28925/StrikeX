using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Photon.Pun;

public class CameraController : MonoBehaviourPun
{
    [Header("Variables")]
    private float mouseX;
    private float mouseY;
    public float sensitivityMouse = 200f;
    float xRotation = 0f;
    public bool isMine = false;

    [Header("GameObjects")]
    public Transform weapon;
    public GameObject Player;

    
    private PhotonView CphotonView;


    private void Start()
    {
        CphotonView = Player.GetComponent<PhotonView>();
        if (photonView.IsMine) isMine = true;
    }

    //Функция, запускающаяся каждый кадр
    void Update()
    {
        if(!isMine) return;
        mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

        Player.transform.Rotate(mouseX * new Vector3(0, 1, 0));

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Player.transform.Rotate(Vector3.up * mouseX);

    }
}
