using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float Cannon_Ball_Speed;
    [SerializeField] private float fire_Time;
    [SerializeField] private Transform initialPoint;
    [SerializeField] private GameObject Cannon_Ball_Prefab;
    [SerializeField] Animator anim;
    private float fire_Timer;

    private void Start()
    {
        fire_Timer = fire_Time;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        fire_Timer -= Time.deltaTime;

        if(fire_Timer < 0)
        {
            anim.SetTrigger("Fire");
            fire_Timer = fire_Time;
        }
    }
    public void Fire()
    {
        GameObject cannonBall =  Instantiate(Cannon_Ball_Prefab, initialPoint.transform.position, initialPoint.transform.rotation);
        cannonBall.GetComponent<Cannon_Ball>().SetSpeed(Cannon_Ball_Speed);
    }
}
