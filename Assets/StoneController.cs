using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵(隕石)の制御
//一定のスピードで左下へ動く
public class StoneController : MonoBehaviour
{
    float stoneXSpeed; //横方向の移動スピード
    float stoneYSpeed; //縦方向の移動スピード

    // Start is called before the first frame update
    void Start()
    {
        this.stoneXSpeed = -0.04f;
        this.stoneYSpeed = -0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //一定のスピード（背景と同じ）で移動
        transform.Translate(this.stoneXSpeed, this.stoneYSpeed, 0);
        //画面を通過したら消滅
        if ((transform.position.x < -3.0f) || (transform.position.y < -6.0f))
        {
            Destroy(gameObject);
        }
    }
}
