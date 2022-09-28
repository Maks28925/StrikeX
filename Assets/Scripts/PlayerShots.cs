using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerShots : MonoBehaviourPun
{


    [Header("Variables")]
    public float fireRate = 15f;          //���������� ����� ����� ���������� ������ 
    public float attackRate = 70f;        //���������� ������� ����� ������� ������
    private float nextTimeToFire = 0f;    //������������ ��� ��������� ���������� ������� ����� ����������/�������
    public float DistanceShot = 100f;     //��������� ���� ��� ��������� 
    public int KalashPatrons = 30;        //���������� �������� � ������ ������
    public int KalashallPatrons = 90;     //���������� ���� �������� ������
    public int M4Patrons = 35;            //���������� �������� � ������ �4 
    public int M4allPatrons = 100;        //���������� ���� �������� �4 
    public float myTime = 0;              //���������� ��� �������� ������� ��� ���������� ���������� ������ �� ���������� ������� �����������
    public bool OnPricel = false;         //� ������ ������������ �� ������ �������
    public bool animIsPlaying = false;    //�������� �� ������ ��������
    public bool isReloaded;               //���� �������������� �� ������ �����
    private int a;                        //���������� ����������� �������� �� ������ ������
    private bool Fire = false;      //���� �������� �� � ���� ����� ������
    private int g=1;                      //���� ������ 
                                          // 0 - Kalash
                                          // 1 - M4 
                                          // 2 - crowbar
    [Header("Layers")]
    public LayerMask LayerMask;           //���� � �������� ������������ ��� ���������
    public LayerMask LayerM4;             //���� �4
    public LayerMask LayerKalash;         //���� ������
    public LayerMask LayerBarrels;        //���� ����� 

    [Header("Vectors")]
    public Vector3 size;                  //�������� ���������� ������ ������
    public Vector3 center;                //����� ���������� ������ ������

    [Header("GameObjects")]
    public GameObject KalashLight;        //���� ��� ������� �������
    public GameObject GameIsPaused;       //���� ������ ����
    public GameObject SettingsPaused;     //���� �������� 
    public GameObject cam;                //������ ������
    public GameObject gun;                //������ ������  
    public GameObject gunInFloor;         //������ ������ �� ����
    public GameObject gunInHands;         //������ ������ � �����
    public GameObject crowbar;            //������ ������ � �����
    public GameObject M4;                 //������ �4 � �����
    public GameObject M4Light;            //������ ������ ������� �� ��������
    public Transform BulletSpawn;         //�������� �� ������� �������� ������� ������� ������
    public Transform M4BulletSpawn;       //�������� �� ������� �������� ������ �������� �4 

    [Header("Audios")]
    public AudioClip AudioShot;           //���� �������� ������
    public AudioClip AudioAttack;         //���� ����� �������
    public AudioClip M4Shot;              //���� �������� � �4 
    public AudioSource AudioShots;        //��������� ����� �������� ������ 
    public AudioSource M4AudioShots;      //��������� ����� �������� �4 

    [Header("Scripts")]
    public DropGun Dropgun;               //������ ������� ���������
    public DestroyBarrel DestroyBarrel;   //������ ������ ������������ �����

    [Header("Particles")]
    public ParticleSystem KalashMuzzleFlash;    //������� �������� ������
    public ParticleSystem M4MuzzleFlash;        //������ �������� �������� �4

    [Header("Animations")]
    private Animation anim;               //��������� �������� � ������� ������
    private Animation animAttack;         //��������� �������� �������� ������
    private Animation animM4;             //��������� �������� �4 

    [Header("Texts")]
    public Text text;                     //������ ������ � Canvas �������� � ������ ������
    public Text text2;                    //������ ������ � Canvas �������� ����� ������
    public Text text3;                    //������ ������ � Canvas �������� � ������ �4
    public Text text4;                    //������ ������ � Canvas �������� ����� �4

    [Header("Colors")]
    public Color color;                   //����� ���� ��� ������ ��������
    public Color color2;                  //������� ���� ��� ������ ��������, ������� ������ 5

    [Header("RigidBodys")]
    public Rigidbody rbKalash;            //��������� RigidBody ������
    public Rigidbody rbM4;                //��������� RigidBody �4

    private PhotonView PphotonView;

    //������� ������������� � ������ ����� 
    void Start()
    {
        PphotonView = GetComponent<PhotonView>();
        if (!photonView.IsMine) return;
        Debug.Log("Start PlayeSHots");
        anim = gun.GetComponent<Animation>();
        animM4 = M4.GetComponent<Animation>();
        animAttack = crowbar.GetComponent<Animation>();
        AudioShots = GetComponent<AudioSource>();
        M4AudioShots = M4.GetComponent<AudioSource>();
    }

    //�������, �������������, � ������ �����
    void Update()
    {
        if (!photonView.IsMine) return;
        Debug.Log("GetInput PlayerShots");
        if (Input.GetMouseButtonDown(1) && !animIsPlaying)
        {
            if (OnPricel)
            {
                if (Dropgun.KalashSelected)
                {
                    g = 0;
                }

                else if (Dropgun.M4Selected)
                {
                    g = 1;
                }

                PricelExit(g);
            }

            else
            {
                if (Dropgun.KalashSelected)
                {
                    g = 0;
                }

                else if (Dropgun.M4Selected)
                {
                    g = 1;
                }
                Pricel(g);
            }
        }

        else if (Input.GetMouseButton(1) && crowbar.gameObject.activeInHierarchy && !animIsPlaying) 
        {
            g = 2;
            Pricel(g);
        }

        if (Fire)
        {
            KalashLight.SetActive(false);
            M4Light.SetActive(false);
        }

        if (anim.IsPlaying("Kalash") || anim.IsPlaying("Take") || anim.IsPlaying("ReloadInPricel") || anim.IsPlaying("Pricel") || anim.IsPlaying("PricelExit") || animM4.IsPlaying("M4Pricel") || animM4.IsPlaying("M4Reload") || animM4.IsPlaying("M4ReloadInPricel") || animM4.IsPlaying("M4PricelExit") || animM4.IsPlaying("Take") || animAttack.IsPlaying("Attack") || animAttack.IsPlaying("RAttack") || animAttack.IsPlaying("Take"))
        {
            animIsPlaying = true;
        }
        else
        {
            animIsPlaying = false;
        }


        if (isReloaded)
        {
            UpdateTime();
        }

        if (myTime > 2.6)
        {
            isReloaded = false;
            myTime = 0;
            Reload(g);
        }


        if (Input.GetMouseButton(0))
        {
            if (g==0 && KalashPatrons > 0 && !isReloaded && !GameIsPaused.gameObject.activeInHierarchy && !SettingsPaused.gameObject.activeInHierarchy && Time.time > nextTimeToFire && !animIsPlaying)
            {
                g = 0;
                nextTimeToFire = Time.time + 1f / fireRate;
                ChangePatron(g);
                Shots(g);
            }

            else if (g==1 && M4Patrons > 0 && !isReloaded && !GameIsPaused.gameObject.activeInHierarchy && !SettingsPaused.gameObject.activeInHierarchy && Time.time > nextTimeToFire && !animIsPlaying)
            {
                g = 1;
                nextTimeToFire = Time.time + 1f / fireRate;
                ChangePatron(g);
                Shots(g);
            }

            else if (crowbar.gameObject.activeInHierarchy && Time.time > nextTimeToFire && !animIsPlaying)
            {
                Attack();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && ((Dropgun.KalashSelected && KalashPatrons < 30 && KalashallPatrons>0) || (Dropgun.M4Selected && M4Patrons < 35 && M4allPatrons>0)) && !animIsPlaying)
        {
            isReloaded = true;
            if (Dropgun.KalashSelected)
            {
                g = 0;
            }

            else if (Dropgun.M4Selected)
            {
                g = 1;
            }

            if (!OnPricel)
            {
                if (g==0) { 
                    anim.Play("Kalash");
                }
                else
                {
                    animM4.Play("M4Reload");
                }
            }

            else
            {
                if (g == 0)
                {
                    anim.Play("ReloadInPricel");
                }

                else
                {
                    animM4.Play("M4ReloadInPricel");
                }
            }
        }
    }

    //������� ��� ����� ������ � ����� �������
    public void Pricel(int g)
    {
        if (g == 0)
        {
            anim.Play("Pricel");
            OnPricel = true;
        }
        else if (g == 1)
        {
            animM4.Play("M4Pricel");
            OnPricel = true;
        }
        else if (g==2 && !animIsPlaying)
        {
            animAttack.Play("RAttack");
            crowbar.GetComponent<AudioSource>().PlayOneShot(AudioAttack);
        }
    }

    //������� ��� ������ ������ �� �������
    public void PricelExit(int g)
    {
        if (g == 0)
        {
            anim.Play("PricelExit");
        }
        else if (g == 1)
        {
            animM4.Play("M4PricelExit");
        }
            OnPricel = false;
    }

    //������� ��������� ���������� �������� ������
    void ChangePatron(int g)
    {
        if (g==0)
        {
            KalashPatrons--;
            text.text = KalashPatrons.ToString();
            text2.text = KalashallPatrons.ToString();
        }

        else if (g==1)
        {
            M4Patrons--;
            text3.text = M4Patrons.ToString();
            text4.text = M4allPatrons.ToString();
        }
    }

    //������� ����������� ���������� �������� ������
    public void Reload(int g)
    {
        if (g==0)
        {
            a = 30 - KalashPatrons;

            if (a < KalashallPatrons)
            {
                KalashallPatrons -= a;
                KalashPatrons += a;
            }

            else if (a == KalashallPatrons)
            {
                KalashPatrons += KalashallPatrons;
                KalashallPatrons = 0;
            }

            else
            {
                KalashPatrons += KalashallPatrons;
                KalashallPatrons = 0;
            }
        }

        else if (g==1)
        {
            a = 30 - M4Patrons;

            if (a < M4allPatrons)
            {
                M4allPatrons -= a;
                M4Patrons += a;
            }

            else if (a == M4allPatrons)
            {
                M4Patrons += M4allPatrons;
                M4allPatrons = 0;
            }

            else
            {
                M4Patrons += M4allPatrons;
                M4allPatrons = 0;
            }
        }

        text.text = KalashPatrons.ToString();
        text2.text = KalashallPatrons.ToString();

        text3.text = M4Patrons.ToString();
        text4.text = M4allPatrons.ToString();

        if (KalashallPatrons < 6)
        {
            text2.GetComponent<Text>().color = color;
        }

        else
        {
            text2.GetComponent<Text>().color = color2;
        }

        if (M4allPatrons < 6)
        {
            text4.GetComponent<Text>().color = color;
        }

        else
        {
            text4.GetComponent<Text>().color = color2;
        }

        if (KalashPatrons > 5)
        {
            text.color = color2;
        }

        else
        {
            text.color = color;
        }

        if (M4Patrons > 5)
        {
            text3.color = color2;
        }

        else
        {
            text3.color = color;
        }
    }

    //������� ����� �������
    void Attack()
    {
        nextTimeToFire = Time.time + 60f / attackRate;
        Debug.Log("bah");
        animAttack.Play("Attack");
        crowbar.GetComponent<AudioSource>().PlayOneShot(AudioAttack);
    }

    //������� �������� �� ������
    void Shots(int g)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f , 0.5f));
        Debug.DrawRay(ray.origin, ray.direction * DistanceShot);

        if (g == 0)
        {
            if (!OnPricel)
            {
                anim.Play("AKM anim");
            }
            else
            {
                anim.Play("ShotInPricel");
            }

            KalashMuzzleFlash.Play();
            KalashLight.SetActive(true);
            Fire = true;
            AudioShots.PlayOneShot(AudioShot);
        }

        else if (g == 1)
        {

            if (!OnPricel)
            {
                animM4.Play("M4Fire");
            }
            else
            {
                animM4.Play("M4ShotInPricel");
            }

            M4MuzzleFlash.Play();
            M4Light.SetActive(true);
            Fire = true;
            M4AudioShots.PlayOneShot(M4Shot);
        }

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, DistanceShot, LayerBarrels))
        {
            DestroyBarrel.DamageBarreles(hit.collider.gameObject.name);
        }

        if (Physics.Raycast(ray, out hit, DistanceShot, LayerMask))
        {
            hit.transform.position = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            Debug.Log("aa");
        }

        else if (Physics.Raycast(ray, out hit, DistanceShot, LayerM4))
        {
            rbM4.AddForce(gunInHands.transform.forward * 100f);
        }

        else if (Physics.Raycast(ray, out hit, DistanceShot, LayerKalash))
        {
            rbKalash.AddForce(M4.transform.forward * 100f);
        }

        if (KalashPatrons < 6)
        {
            text.GetComponent<Text>().color = color;
        }
        else 
        {
            text.GetComponent<Text>().color = color2;
        }
        if (M4Patrons < 6)
        {
            text3.GetComponent<Text>().color = color;
        }
        else
        {
            text3.GetComponent<Text>().color = color2;
        }
    }

    //������� ���������� ������� ��� �� �����������
    public void UpdateTime()
    {
        myTime += Time.deltaTime;
    }

    //������� ��������� �������� ���������� g 
    public void ChangeG(int a)
    {
        g = a;
    }
}
