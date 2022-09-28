using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{

    [Header("Variables")]
    public bool Active = false;         //Есть ли соприкосновение обьекта тела калаша с обьектом тега "Ground"
    public float dist;                  //Расстояние между обьектом калаша и персонажем

    [Header("GameObjects")]
    public GameObject UI;              //Обьект с надписью "Нажмите клавишу для подбора оружия"
    public GameObject PlayerPos;       //Обьект-пустышка, имеющий позицию игрока для определения расстояния между игроков и калашем


    //Функция, запускающаяся в каждом кадре
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

    //Функция, отслежвающая коллизию калаша с обьектом тега "Ground"
    public void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Ground")
        {
            Active = true;
        }
    }

    //Функция, отслеживающая выход из коллизии с обьектами, тега "Ground"
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Active = false;
        }
    }
}
