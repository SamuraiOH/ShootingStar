using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵(隕石)の制御
//一定のスピードで左下へ動く
public class StoneController : MonoBehaviour
{
    float stoneXSpeed; //横方向の移動スピード
    float incStoneXSpeed; //横方向の移動スピードの上がり幅
    float stoneYSpeed; //縦方向の移動スピード
    int level; //ゲームのレベル(スピードに影響)

    // Start is called before the first frame update
    void Start()
    {
        this.stoneXSpeed = -0.04f;
        this.incStoneXSpeed = this.stoneXSpeed * 0.1f;
        this.stoneXSpeed += (float)this.level * this.incStoneXSpeed;

        this.stoneYSpeed = -0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //一定のスピードで移動
        transform.Translate(this.stoneXSpeed, this.stoneYSpeed, 0);
        //画面を通過したら消滅
        if ((transform.position.x < -3.0f) || (transform.position.y < -6.0f))
        {
            Destroy(gameObject);
        }
    }

    //経過時間に応じて移動スピードアップ
    public void SpeedController(int level)
    {
        this.level = level;
    }
}
