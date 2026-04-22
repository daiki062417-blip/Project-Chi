using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackNanachi : MonoBehaviour
{
    int shortdamage = 1;//短押しのダメージ
    int longdamage = 5;//長押しのダメージ
    GameObject player;
    GameObject enemy;

    // ボタンの入力状態
    private bool IsButton => Input.GetKey(KeyCode.K);

    // 長押しと判定されるまでの時間[秒]
    private float LongPressThreshold => 1.0f;

    void Start()
    {
        player = GameObject.FindWithTag("player");
        enemy = GameObject.FindWithTag("Enemy");
        StartCoroutine(CheckCoroutine());
    }


    private IEnumerator CheckCoroutine()
    {
        var isLongPressFired = false; // 長押し検知済みかどうか
        var pressSeconds = 0.0f; // ボタンが押されている時間
        var lastValue = IsButton; // 1F前の状態

        while (true)
        {
            if (IsButton)
            {
                // 一度長押しが検知されたらボタンが離されるまで何もしない
                if (!isLongPressFired)
                {
                    // ボタンが押されていないかつ、まだ発火していない場合
                    // 押された時間を記録し、しきい値を超えたら発火
                    pressSeconds += Time.deltaTime;
                    if (pressSeconds >= LongPressThreshold)
                    {
                        OnLongPress();
                        isLongPressFired = true;
                    }
                }
            }
            // IsButton=false && lastValue=true
            // すなわち「ボタンがこの瞬間に離された」を検知したとき
            else if (lastValue)
            {
                if (pressSeconds < LongPressThreshold)
                {
                    // LongPressThreshold未満で離された場合は短押し
                    OnShortPress();
                }

                isLongPressFired = false;
                pressSeconds = 0.0f;
            }

            lastValue = IsButton;
            yield return null;//1フレームごとに処理を中断 → 次のフレームで再開
        }
    }


    private bool IsHit()
    {
        Vector3 me = player.transform.position;
        Vector3 you = enemy.transform.position;
        float distance = Vector3.Distance(me, you);
        Vector3 dir = (you - me).normalized;//playerから敵への方向ベクトル
        Vector3 forward = player.transform.forward;//playerの前方ベクトル
        float angle = Vector3.Angle(forward, dir);//playerと敵の偏角を取得
        return distance < 1f && angle < 30f;//一メートル以内で視野６０度以内
    }

    private void OnShortPress()
    {
        if(IsHit())
        {
            Debug.Log(shortdamage + "ダメージ！");
        }
        else
        {
            Debug.Log("ミス！");
        }
    }
    private void OnLongPress()
    {
        if(IsHit())
        {
            Debug.Log(longdamage + "ダメージ！");
        }
        else
        {
            Debug.Log("ミス！");
        }
    }
}
