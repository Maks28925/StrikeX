using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class PlayerController : MonoBehaviourPunCallbacks
{
    [Header("Variables")]
    public float jumpPower = 200f;            //���� ������
    public float speed = 7f;                  //�������� ������������
    public bool ground = true;                //�������� ���������� ������������, ����� �� �������� �� �����
    public bool cheatOn = false;              //���� �������� �� ����
    public int SceneId = 0;                   //�� ������ ����������� �����
    public bool isMine = false;

    [Header("GameObjects")]
    public GameObject gunInHands;             //������ ������ � �����
    public GameObject KalashPatronsText;      //������ ������ ���������� �������� ������
    public GameObject crowbar;                //������ ������ � �����
    public GameObject M4;                     //������ �4 � �����
    public GameObject M4PatronsText;          //������ ������ ���������� �������� �4 
    public GameObject Player;                 //������ ������
    public GameObject MainCamera;             //������ ������ 

    [Header("Animations")]
    private Animation CrowbarAnim;            //�������� ������
    private Animation M4Anim;                 //�������� �4
    private Animation KalashAnim;             //�������� ������

    [Header("RigidBodys")]
    public Rigidbody rb;                      //��������� RigidBody ������, ������������ ��� ������

    [Header("Scripts")]
    public DropGun DropGun;                    //������ ����� DropGun
    public PlayerShots PlayerShots;            //������ ������ Player Shots

    private PhotonView PCphotonView;


    //�������, ������������ ������ ���� 
    void Update()
    {
        if (!isMine) return;

            GetInput();
    }


    //�������� RigidBody ������
    void Start()
    {
        PCphotonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            isMine = false;
            MainCamera.GetComponent<Camera>().enabled = false;
            M4.transform.localPosition += M4.transform.forward * 0.5f;
        }
        else
        {
            isMine = true;
            Debug.Log("Animation Of Controller");
            CrowbarAnim = crowbar.GetComponent<Animation>();
            M4Anim = M4.GetComponent<Animation>();
            KalashAnim = gunInHands.GetComponent<Animation>();
        }
        if (SceneManager.GetActiveScene().name == "Portals")
        {
            SceneId = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Play")
        {
            SceneId = 0;
        }
    }
         

    //������� ����� ������� �������
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = new Vector3(1f, 1.1f, -3f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (cheatOn)
            {
                transform.localPosition += MainCamera.transform.forward * speed * Time.deltaTime;
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (cheatOn)
            {
                transform.localPosition += -MainCamera.transform.forward * speed * Time.deltaTime;
            }
            else
            {
                transform.localPosition += -transform.forward * speed * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (cheatOn)
            {
                transform.localPosition += -MainCamera.transform.right * speed * Time.deltaTime;
            }
            else
            {
                transform.localPosition += -transform.right * speed * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (cheatOn)
            {
                transform.localPosition += MainCamera.transform.right * speed * Time.deltaTime;
            }
            else
            {
                transform.localPosition += transform.right * speed * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && ground && !cheatOn)
        {
            rb.AddForce(transform.up * jumpPower);
        }

        else if (Input.GetKey(KeyCode.Space) && cheatOn)
        {
            transform.localPosition += transform.up * speed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.LeftShift) && cheatOn)
        {
            transform.localPosition += -transform.up * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown("3") && SceneId==0 && !DropGun.crowbar.gameObject.activeInHierarchy && !Player.GetComponent<PlayerShots>().animIsPlaying && !PlayerShots.OnPricel)
        {
            if (DropGun.haveKalash)
            {
                gunInHands.SetActive(false);
                DropGun.NoActiveKalashIcon.SetActive(true);
                DropGun.ActiveKalashIcon.SetActive(false);
                KalashPatronsText.SetActive(false);
                DropGun.KalashSelected = false;
            }
            else if (DropGun.haveM4)
            {
                DropGun.ActiveM4Icon.SetActive(false);
                DropGun.NoActiveM4Icon.SetActive(true);
                M4.SetActive(false);
                M4PatronsText.SetActive(false);
                DropGun.M4Selected = false;
            }

            DropGun.crowbar.SetActive(true);
            DropGun.ActiveCrowBarIcon.SetActive(true);
            DropGun.NoActiveCrowBarIcon.SetActive(false);
            CrowbarAnim.Play("Take");
            PlayerShots.ChangeG(2);
        }

        if (Input.GetKeyDown("1") && SceneId == 0 && DropGun.haveKalash && !DropGun.KalashSelected && !PlayerShots.OnPricel)
        {
            gunInHands.SetActive(true);
            DropGun.crowbar.SetActive(false);
            DropGun.ActiveCrowBarIcon.SetActive(false);
            DropGun.NoActiveCrowBarIcon.SetActive(true);
            DropGun.ActiveKalashIcon.SetActive(true);
            DropGun.NoActiveKalashIcon.SetActive(false);
            DropGun.ActiveM4Icon.SetActive(false);
            DropGun.NoActiveM4Icon.SetActive(false);
            KalashPatronsText.SetActive(true);
            M4PatronsText.SetActive(false);
            DropGun.KalashSelected = true;
            PlayerShots.ChangeG(0);
            KalashAnim.Play("Take");
        }

        else if (Input.GetKeyDown("1") && SceneId == 0 && DropGun.haveM4 && !DropGun.M4Selected && !PlayerShots.OnPricel)
        {
            M4.SetActive(true);
            DropGun.crowbar.SetActive(false);
            DropGun.ActiveCrowBarIcon.SetActive(false);
            DropGun.NoActiveCrowBarIcon.SetActive(true);
            DropGun.ActiveM4Icon.SetActive(true);
            DropGun.NoActiveM4Icon.SetActive(false);
            DropGun.ActiveKalashIcon.SetActive(false);
            DropGun.NoActiveKalashIcon.SetActive(false);
            KalashPatronsText.SetActive(false);
            M4PatronsText.SetActive(true);
            DropGun.M4Selected = true;
            PlayerShots.ChangeG(1);
            M4Anim.Play("Take");
        }

        if (Input.GetKeyDown(KeyCode.F) && SceneId == 0 && !DropGun.haveKalash && !PlayerShots.OnPricel)
        {
            DropGun.TakeGun();
        }

        else if (Input.GetKeyDown(KeyCode.F) && SceneId == 0 && !DropGun.haveM4 && !PlayerShots.OnPricel) 
        {
            DropGun.TakeGun();
        }

        if(Input.GetKeyDown(KeyCode.G) && SceneId == 0 && DropGun.KalashSelected && !Player.GetComponent<PlayerShots>().animIsPlaying && !PlayerShots.OnPricel)
        {

            DropGun.DropgunKalash();
            CrowbarAnim.Play("Take");
        }

        else if (Input.GetKeyDown(KeyCode.G) && SceneId == 0 && DropGun.M4Selected && !Player.GetComponent<PlayerShots>().animIsPlaying && !PlayerShots.OnPricel)
        {
            Debug.Log("Pressed");
            DropGun.DropM4("Pressed");
            CrowbarAnim.Play("Take");
        }
    }
    

    //������� �������� ������ � ��������� ���� "Ground"
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }


    //������� ������ � ������� ������ � ��������� ���� "Ground"
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
        }
    }
}

