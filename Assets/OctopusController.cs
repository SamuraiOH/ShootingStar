using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵(タコ)の制御
//一定のスピードで左へ動く＆1回弾を発射
public class OctopusController : MonoBehaviour
{
    float octopusSpeed; //移動スピード
    float incOctopusSpeed; //移動スピードの上がり幅
    public GameObject enemyballPrefab; //弾を生成するPrefab
    bool attackFlag; //攻撃フラグ(攻撃を発射したかどうかの判断)
    int level; //ゲームのレベル(スピードに影響)

    //弾発射の効果音
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] public AudioClip attackSE;

    // Start is called before the first frame update
    void Start()
    {
        this.octopusSpeed = -0.04f;
        this.incOctopusSpeed = this.octopusSpeed * 0.1f;
        this.octopusSpeed += (float)this.level * this.incOctopusSpeed;

        this.attackFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //一定のスピード（背景と同じ）で移動
        transform.Translate(this.octopusSpeed, 0, 0);
        //画面を通過したら消滅
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }
        //x座標が2より左になると1度だけ弾を発射
        if ((transform.position.x <= 2.0f) && (attackFlag == true))
        {
            GameObject go = Instantiate(enemyballPrefab);
            float octopusYPosition = transform.position.y;
            go.transform.position = new Vector3(1.5f, octopusYPosition, 0);
            go.GetComponent<EBallController>().SpeedController(this.level);
            attackFlag = false;
            //効果音
            seAudioSource.PlayOneShot(attackSE);
        }
    }

    //経過時間に応じて移動スピードアップ
    public void SpeedController(int level)
    {
        this.level = level;
    }
}
