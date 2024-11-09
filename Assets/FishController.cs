using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵(ピラニア)の制御
//一定のスピードで左へ動く
public class FishController : MonoBehaviour
{
    float fishSpeed; //移動スピード
    float incFishSpeed; //移動スピードの上がり幅
    int level; //ゲームのレベル(スピードに影響)

    // Start is called before the first frame update
    void Start()
    {
        this.fishSpeed = -0.04f;
        this.incFishSpeed = this.fishSpeed * 0.1f;
        this.fishSpeed += (float)this.level * this.incFishSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //一定のスピードで移動
        transform.Translate(this.fishSpeed, 0, 0);
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
