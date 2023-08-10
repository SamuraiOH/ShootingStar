using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タコ敵の弾の制御スクリプト
public class EBallController : MonoBehaviour
{
    float eBallSpeed; //移動スピード

    // Start is called before the first frame update
    void Start()
    {
        eBallSpeed = -0.06f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //一定のスピード（背景と同じ）で移動
        transform.Translate(this.eBallSpeed, 0, 0);
        //画面を通過したら消滅
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }
    }
}
