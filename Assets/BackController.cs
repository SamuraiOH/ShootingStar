using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//これともう1つの背景画像を使って背景を無限に左スクロールさせる
//(プレイヤーが右に動いているように見える)
public class BackController : MonoBehaviour
{
    float backSpeed; //背景画像の移動スピード

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //60フレームに固定
        this.backSpeed = -0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //背景画像が画面から見切れそうになったら右に画像を移動
        if (transform.position.x + this.backSpeed < -23.0f)
        {
            transform.Translate(82.0f, 0, 0);
        }
        else
        {
            transform.Translate(this.backSpeed, 0, 0);
        }
        
    }
}
