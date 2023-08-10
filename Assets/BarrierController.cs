using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バリアスター(無敵アイテム)を制御するスクリプト
public class BarrierController : MonoBehaviour
{
    float pointSpeed; //移動スピード

    // Start is called before the first frame update
    void Start()
    {
        this.pointSpeed = -0.045f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //一定のスピードで移動
        transform.Translate(this.pointSpeed, 0, 0);
        //画面を通過したら消滅
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }
    }
}
