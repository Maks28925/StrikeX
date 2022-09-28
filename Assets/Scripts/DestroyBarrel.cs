using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrel : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject Barrel1;
    public GameObject Barrel2;

    [Header("Particles")]
    public ParticleSystem Explosion;

    class Barrels
    {
        public int number;
        public int hp = 100;
    }

    Barrels first = new Barrels();
    Barrels second = new Barrels();


    void Start()
    {
        first.number = 1;
        second.number = 2;
    }

    public void DamageBarreles(string name)
    {
        Debug.Log(name);
        if (name == "1")
        {
            first.hp -= 38;
            Debug.Log(first.hp);
            if (first.hp < 0)
            {
                DestroyBarreles(name);
            }
        }

        else if(name == "2")
        {
            second.hp -= 38;
            Debug.Log(second.hp);
            if (second.hp < 0)
            {
                DestroyBarreles(name);
            }
        }

    }

    public void DestroyBarreles(string name)
    {
        if (name == "1")
        {
            Explosion.transform.position = Barrel1.transform.position;
            Barrel1.SetActive(false);
            Explosion.Play();
        }
        else if (name == "2")
        {
            Explosion.transform.position = Barrel2.transform.position;
            Barrel2.SetActive(false);
            Explosion.Play();
        }
    }
}
