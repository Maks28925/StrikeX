using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRay : MonoBehaviourPun
{
    [Header("Layers")]
    public LayerMask LayerMask;

    [Header("Variables")]
    public float Distance;
    public float speed = 7f;
    public float dist;
    public bool OnCheat;

    [Header("Scripts")]
    public PlayerController PlayerController;

    [Header("GameObjects")]
    public GameObject MainCamera;

    private PhotonView PphotonView;


    //Функция, запускающаяся каждый кадр
    void Update()
    {
        if (!photonView.IsMine) return;
        GetInput();
    }

    private void Start()
    {
        PphotonView = GetComponent<PhotonView>();
    }

    //Функция приёма нажатия клавиш
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            OnCheat = PlayerController.cheatOn;

            if (Input.GetKey(KeyCode.W))
            {
                TeleportForward(OnCheat);
            }

            if (Input.GetKey(KeyCode.A))
            {
                TeleportLeft(/*OnCheat*/);
            }

            if (Input.GetKey(KeyCode.S))
            {
                TeleportBack(/*OnCheat*/);
            }

            if (Input.GetKey(KeyCode.D))
            {
                TeleportRight(/*OnCheat*/);
            }
            if(!Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.W))
            {
                TeleportForward(OnCheat);
            }
        }
    }


    //Функция телепорта вперед
    void TeleportForward(bool OnCheat)
    {
        Ray ray;
        if (OnCheat)
        {
            ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * Distance);
        }
        else
        {
            ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * Distance);
        }

        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, Distance, LayerMask))
        {
            if (OnCheat)
            {
                dist = Vector3.Distance(hit.point, transform.position);
                transform.localPosition += MainCamera.transform.forward * (dist - 1f);
            }
            else
            {
                dist = Vector3.Distance(hit.point, transform.position);
                transform.localPosition += transform.forward * (dist-1f);
            }
        }
        else
        {
            if (OnCheat)
            {
                transform.localPosition += MainCamera.transform.forward * Distance;
            }
            else
            {
                transform.localPosition += transform.forward * Distance;
            }
        }
    }


    //Функция телепорта влево
    void TeleportLeft()
    {
        Ray ray = new Ray(transform.position, -transform.right);
        Debug.DrawRay(ray.origin, ray.direction * Distance);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Distance, LayerMask))
        {
            dist = Vector3.Distance(hit.point, transform.position);
            transform.localPosition += -transform.right * (dist -1f);
        }
        else
        {
            transform.localPosition += -transform.right * Distance;
        }
    }


    //Функция телепорта назад
    void TeleportBack()
    {
        Ray ray = new Ray(transform.position, -transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * Distance);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Distance, LayerMask))
        {
            dist = Vector3.Distance(hit.point, transform.position);
            transform.localPosition += -transform.forward * (dist -1f);
        }
        else
        {
            transform.localPosition += -transform.forward * Distance;
        }
    }

    
    //Функция телепорта вправо
    void TeleportRight()
    {
        Ray ray = new Ray(transform.position, transform.right);
        Debug.DrawRay(ray.origin, ray.direction * Distance);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Distance, LayerMask))
        {
            dist = Vector3.Distance(hit.point, transform.position);
            transform.localPosition += transform.right * (dist - 1f);
            Debug.Log("TP");
        }
        else
        {
            transform.localPosition += transform.right * speed;
        }
    }
}
