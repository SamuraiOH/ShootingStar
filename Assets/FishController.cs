using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵(ピラニア)の制御
//一定のスピードで左へ動く
public class FishController : MonoBehaviour
{
    float fishSpeed; //移動スピード

    // Start is called before the first frame update
    void Start()
    {
        this.fishSpeed = -0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //一定のスピード（背景と同じ）で移動
        transform.Translate(this.fishSpeed, 0, 0);
        //画面を通過したら消滅
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }
    }
}
