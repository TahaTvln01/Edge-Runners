using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw_Trap : Trap
{
    private Animator anim;
    [SerializeField] private bool isWorking = true;
    [SerializeField] private bool isMoving = true;
    [SerializeField] private bool volta = true;

    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float Speed = 7;
    [SerializeField] private float coolDown = 2;

    private int movePointIndex;
    private float coolDownTimer;
    private bool goBack;
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = movePoints[0].position;
    }

    void Update()
    {
        isMoving = coolDownTimer < 0;
        anim.SetBool("isWorking", isWorking);

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[movePointIndex].position, Speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, movePoints[movePointIndex].position) < .15f)
        {

            if (volta)
            {
                if (movePointIndex >= movePoints.Length - 1)
                {
                    goBack = true;
                    coolDownTimer = coolDown;
                }
                if (movePointIndex <= 0)
                {
                    goBack = false;
                    coolDownTimer = coolDown;
                }

                if (goBack)
                    movePointIndex--;
                else
                    movePointIndex++;
            }
            else
            {
                coolDownTimer = coolDown;
                movePointIndex++;
                if (movePointIndex >= movePoints.Length)
                {
                    movePointIndex = 0;
                }
            }
        }
        coolDownTimer -= Time.deltaTime;
    }
}
