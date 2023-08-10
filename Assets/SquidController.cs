using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//敵(イカ)の制御
//一定のスピードで上下しながら左へ動く
public class SquidController : MonoBehaviour
{
    float squidSpeed; //左への移動スピード
    int angle; //サインカーブに基づく上下運動のための角度

    // Start is called before the first frame update
    void Start()
    {
        this.squidSpeed = -0.04f;
        this.angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //x方向は一定のスピード（背景と同じ）で移動
        //y方向には上下に振動するように移動（サインカーブを描く）
        transform.Translate(this.squidSpeed, (float)(1.5f * (Math.Sin((angle + 2) * Math.PI / 180) - Math.Sin(angle * Math.PI / 180))), 0);
        //画面を通過したら消滅
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }

        if (angle >= 360)
        {
            angle = 0;
        }
        angle += 2;
    }
}
