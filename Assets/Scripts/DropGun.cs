using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DropGun : MonoBehaviourPun
{
    [Header("GameObjects")]
    public GameObject Kalash;               //������ ������ � �����
    public GameObject crowbar;                  //������ ������ � �����
    public GameObject M4;                       //������ �4 
    public GameObject Maincamera;                   //������ ������
    public GameObject Player;

    [Header("UI")]
    public GameObject CanvasText;               //������ � ������� "����� ��������� ������,������� ������"
    public GameObject ActiveKalashIcon;         //������ �������� ������ ������
    public GameObject NoActiveKalashIcon;       //������ ���������� ������ ������
    public GameObject ActiveM4Icon;             //������ �������� ������ �4
    public GameObject NoActiveM4Icon;           //������ ���������� ������ �4
    public GameObject ActiveCrowBarIcon;        //������ �������� ������ ������
    public GameObject NoActiveCrowBarIcon;      //������ ���������� ������ ������
    public GameObject PatronsTextKalash;        //������ ������ ���������� �������� ������
    public GameObject M4PatronsText;            //������ ������ ���������� �������� �4
    public GameObject NumberWeapon;             //������ ������ ������
    public GameObject TakeGunText;              //������ ������ "�������� ������"

    [Header("RigidBodys")]
    public Rigidbody rbKalash;                        //RigidBody ������
    public Rigidbody rbM4;                      //RigidBody �4 

    [Header("Variables")]
    public float dropPower = 100f;              //���� ������������ ������
    public bool haveKalash = false;                //���� ���� �� ������ ���������
    public bool haveM4 = true;                  //���� ���� �� �4 � ��������� 
    public bool M4Selected = true;              //����� �� �4  � ���� ��� ��� 
    public bool KalashSelected = false;         //���� �� ������ ���� ��� ��� 

    [Header("Layers")]
    public LayerMask M4Layer;                   //���� �4   
    public LayerMask KalashLayer;               //���� ������

    [Header("Animations")]
    public Animation M4Anim;                    //��������� �������� �4
    public Animation KalashAnim;                //��������� �������� ������             

    [Header("Scripts")]
    public PlayerShots PlayerShots;             //������ PlayerShots


    private PhotonView DphotonView;

    //�������, ������������� � ������ ����
    void Start()
    {
        DphotonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            M4Anim = M4.GetComponent<Animation>();
            KalashAnim = Kalash.GetComponent<Animation>();
        }
    }


    //������� ������� ������
    public void DropgunKalash()
    {
        Kalash.transform.parent = null;
        rbKalash.isKinematic = false;
        rbKalash.AddForce(Player.transform.forward * dropPower);
        rbKalash.AddForce(Player.transform.up * dropPower * 2);
        crowbar.SetActive(true);
        ActiveCrowBarIcon.SetActive(true);
        NoActiveCrowBarIcon.SetActive(false);
        ActiveKalashIcon.SetActive(false);
        NoActiveKalashIcon.SetActive(false);
        PatronsTextKalash.SetActive(false);
        haveKalash = false;
        PlayerShots.ChangeG(2);
        KalashSelected = false;
        NumberWeapon.SetActive(false);
    }


    //������� ������� �4
    public void DropM4(string a)
    {
        Debug.Log(a);
        Debug.Log(M4.name);
        M4.SetActive(true);
        M4.transform.parent = null;
        rbM4.isKinematic = false;
        rbM4.AddForce(Player.transform.forward * dropPower);
        rbM4.AddForce(Player.transform.up * dropPower);
        crowbar.SetActive(true);
        ActiveCrowBarIcon.SetActive(true);
        NoActiveCrowBarIcon.SetActive(false);
        ActiveM4Icon.SetActive(false);
        NoActiveM4Icon.SetActive(false);
        M4PatronsText.SetActive(false);
        haveM4 = false;
        PlayerShots.ChangeG(2);
        M4Selected = false;
        NumberWeapon.SetActive(false);
    }


    //������� ������ ������ � ����
    public void TakeGun()
    {
        if (!photonView.IsMine) return;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        Debug.DrawRay(ray.origin, ray.direction * 3);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3, M4Layer))
        {
            if (haveKalash)
            {
                DropgunKalash();
            }

            M4.transform.parent = Maincamera.transform;
            M4.transform.localPosition = new Vector3(0.136f, -0.162f, 0.274f);
            M4.transform.localRotation = Quaternion.Euler(0.986f, 0.986f, 0.15f);
            M4.transform.localScale = new Vector3(0.7f, 0.8749996f, 0.7f);
            rbM4.isKinematic = true;
            haveM4 = true;
            crowbar.SetActive(false);
            ActiveCrowBarIcon.SetActive(false);
            NoActiveCrowBarIcon.SetActive(true);
            ActiveKalashIcon.SetActive(false);
            NoActiveKalashIcon.SetActive(false);
            ActiveM4Icon.SetActive(true);
            NoActiveM4Icon.SetActive(false);
            PatronsTextKalash.SetActive(false);
            M4PatronsText.SetActive(true);
            M4Anim.Play("Take");
            PlayerShots.ChangeG(1);
            M4Selected = true;
            NumberWeapon.SetActive(true);
        }

        if (Physics.Raycast(ray, out hit, 3, KalashLayer))
        {
            if (haveM4)
            {
                DropM4("Ray");
            }

            Debug.Log("b");

            Kalash.transform.parent = GetComponent<Camera>().transform;
            Kalash.transform.localPosition = new Vector3(0.208035f, -0.1393906f, 0.3659038f);
            Kalash.transform.localRotation = Quaternion.Euler(-4.922f, 0.976f, 0.151f);
            Kalash.transform.localScale = new Vector3(1f, 1f, 1f);
            rbKalash.isKinematic = true;
            haveKalash = true;
            crowbar.SetActive(false);
            ActiveCrowBarIcon.SetActive(false);
            NoActiveCrowBarIcon.SetActive(true);
            ActiveKalashIcon.SetActive(true);
            NoActiveKalashIcon.SetActive(false);
            ActiveM4Icon.SetActive(false);
            NoActiveM4Icon.SetActive(false);
            PatronsTextKalash.SetActive(true);
            M4Anim.Play("Take");
            PlayerShots.ChangeG(0);
            KalashSelected = true;
            KalashAnim.Play("Take");
            NumberWeapon.SetActive(true);
        }
    }
}


