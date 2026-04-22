using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Attack2 : MonoBehaviour
{
    int sd = 1;//shortdamage
    int ld = 5;//longdamage
    GameObject player;
    GameObject enemy;

    private bool DownButton => Input.GetKeyDown(KeyCode.K);
    private bool UpButton => Input.GetKeyUp(KeyCode.K);
    private float lpt= 1.0f;//LongPressThreshold
    private float t = 0;
    private bool isPressing;


    void Start()
    {
        player = GameObject.FindWithTag("player");
        enemy = GameObject.FindWithTag("Enemy");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)){
            isPressing = true;
            t = 0;
        }

        if (isPressing)
        {
            t += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            isPressing = false;

            if (t >= lpt)
            {
                OnLongPress();
            }
            else
            {
                OnShortPress();
            }
        }
    }
    private bool IsHit()
    {
        Vector3 me = player.transform.position;
        Vector3 you = enemy.transform.position;
        float distance = Vector3.Distance(me, you);
        Vector3 dir = (you - me).normalized;
        Vector3 forward = player.transform.forward;
        float angle = Vector3.Angle(forward, dir);
        return distance < 1f && angle < 30f;
    }

    private void OnShortPress()
    {
        if (IsHit())
        {
            Debug.Log(sd + "ダメージ！");
        }
        else
        {
            Debug.Log("ミス！");
        }
    }

    private void OnLongPress()
    {
        if (IsHit())
        {
            Debug.Log(ld + "ダメージ！");
        }
        else
        {
            Debug.Log("ミス！");
        }
    }
}