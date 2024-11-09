using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タコ敵の弾の制御スクリプト
public class EBallController : MonoBehaviour
{
    float eBallSpeed; //移動スピード
    float incEBallSpeed; //移動スピードの上がり幅
    int level; //ゲームのレベル(スピードに影響)

    // Start is called before the first frame update
    void Start()
    {
        eBallSpeed = -0.06f;
        this.incEBallSpeed = this.eBallSpeed * 0.1f;
        this.eBallSpeed += (float)this.level * this.incEBallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //一定のスピードで移動
        transform.Translate(this.eBallSpeed, 0, 0);
        //画面を通過したら消滅
        if (transform.position.x < -3.0f)
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
