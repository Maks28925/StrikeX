using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{

    [Header("Variables")]
    public bool Active = false;         //���� �� ��������������� ������� ���� ������ � �������� ���� "Ground"
    public float dist;                  //���������� ����� �������� ������ � ����������

    [Header("GameObjects")]
    public GameObject UI;              //������ � �������� "������� ������� ��� ������� ������"
    public GameObject PlayerPos;       //������-��������, ������� ������� ������ ��� ����������� ���������� ����� ������� � �������


    //�������, ������������� � ������ �����
    void Update()
    {

        if (Active && dist < 3)
        {
            dist = Vector3.Distance(UI.transform.position, PlayerPos.transform.position);
            UI.SetActive(true);
        }

        else 
        {
            UI.SetActive(false);
        }
    }

    //�������, ������������ �������� ������ � �������� ���� "Ground"
    public void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Ground")
        {
            Active = true;
        }
    }

    //�������, ������������� ����� �� �������� � ���������, ���� "Ground"
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Active = false;
        }
    }
}
