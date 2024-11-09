using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//プレイヤーをスワイプで動かすスクリプト
//スワイプするとその方向に動き続ける
//当たり判定とそれに伴う効果音もここで処理
public class BossyController : MonoBehaviour
{
    enum Direction //プレイヤーの移動方向を表す列挙型
    {
        Stop, //停止
        Up, //上
        Down, //下
        Left, //左(後退)
        Right //右(前進)
    }

    enum State //プレイヤーの状態
    {
        Normal, //通常
        DamageInvincible, //ダメージ時の無敵状態
        BarrierInvincible //アイテムによる無敵状態
    }

    float speed; //移動スピード
    Vector2 startPos; //指が触れた座標
    Vector2 endPos; //指が離れた座標
    Vector2 moveVector; //スワイプのベクトル
    Direction direcction; //現在のプレイヤーの移動方向(初期は停止)
    State state; //プレイヤの状態
    bool flag; //ポーズ状態の有無(再開時ポーズ時の方向を維持するため)
    Direction pauseDirection; //ポーズ時の移動方向を保存する変数
    [SerializeField] float flashInterval1; //点滅の間隔[s]
    [SerializeField] float flashInterval2; //点滅の間隔[s](解除が近い時)
    [SerializeField] int damageLoopCount; //ダメージ時の点滅の回数
    [SerializeField] int barrierLoopCount1; //アイテムによる無敵の点滅回数
    [SerializeField] int barrierLoopCount2; //アイテムによる無敵の点滅回数(解除が近いとき)
    SpriteRenderer sp; //点滅用のスプライトレンダラー

    //効果音用の変数
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] public AudioClip damageSE;
    [SerializeField] public AudioClip healSE;
    [SerializeField] public AudioClip barrierSE;
    [SerializeField] public AudioClip barrierSE2;
    [SerializeField] public AudioClip life1SE;
    [SerializeField] public AudioClip barrierHitSE;

    //初期設定
    // Start is called before the first frame update
    void Start()
    {
        //変数の初期化
        this.sp = GetComponent<SpriteRenderer>();
        this.speed = 0.06f;
        this.direcction = Direction.Stop;
        this.pauseDirection = Direction.Stop;
        this.flag = false;
        this.state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            this.pauseDirection = this.direcction;
            this.flag = true;
            return;
        }

        //スワイプの開始
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        //スワイプの終了
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            moveVector = endPos - startPos;
            //スワイプの方向に応じて移動方向を決定
            //スワイプベクトルの各成分の正負と絶対値の大きさから上下左右のいずれかに決定
            if (Math.Abs(moveVector.x) > Math.Abs(moveVector.y))
            {
                if (moveVector.x > 0)
                {
                    direcction = Direction.Right;
                }else if (moveVector.x < 0)
                {
                    direcction = Direction.Left;
                }
            }else if (Math.Abs(moveVector.x) < Math.Abs(moveVector.y))
            {
                if (moveVector.y > 0)
                {
                    direcction = Direction.Up;
                }
                else if (moveVector.y < 0)
                {
                    direcction = Direction.Down;
                }
            }
        }

        if (this.flag)
        {
            //ポーズが解除されたら移動方向をポーズ時のものに
            this.direcction = this.pauseDirection;
            this.flag = false;
        }

        //現在の移動方向に従い画面内の範囲でプレイヤを移動させる(上下の限界に達したら逆方向へ)
        if ((direcction == Direction.Up) && (transform.position.y < 4.5f))
        {
            transform.Translate(0, this.speed, 0);
        }
        else if ((direcction == Direction.Up) && (transform.position.y >= 4.5f))
        {
            direcction = Direction.Down;
        }
        else if ((direcction == Direction.Down) && (transform.position.y > -4.5f))
        {
            transform.Translate(0, -1 * this.speed, 0);
        }
        else if ((direcction == Direction.Down) && (transform.position.y <= -4.5f))
        {
            direcction = Direction.Up;
        }
        else if ((direcction == Direction.Left) && (transform.position.x > -2.5f))
        {
            transform.Translate(-1 * this.speed, 0, 0);
        }
        else if ((direcction == Direction.Right) && (transform.position.x < 2.5f))
        {
            transform.Translate(this.speed, 0, 0);
        }
    }

    //オブジェクトとの接触時の処理
    void OnTriggerEnter2D(Collider2D other)
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //敵との接触
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            if (this.state == State.Normal)
            {
                //通常状態の場合
                GameObject director = GameObject.Find("GameDirector");
                director.GetComponent<GameDirector>().Damage();
                if (GameDirector.life == 1)
                {
                    seAudioSource.PlayOneShot(life1SE);
                }
                else if(GameDirector.life > 1)
                {
                    seAudioSource.PlayOneShot(damageSE);
                }
                this.state = State.DamageInvincible; //無敵状態へ移行（3秒）
                StartCoroutine(damageFlash()); //点滅
            }else if (this.state == State.BarrierInvincible)
            {
                //アイテムによる無敵状態の場合
                if (GameDirector.score + 50 < 9999999)
                {
                    GameDirector.score += 50;
                }else if (GameDirector.score < 9999999)
                {
                    GameDirector.score = 9999999;
                }
                seAudioSource.PlayOneShot(barrierHitSE);
            }

        }

        //アイテムとの接触
        if (other.gameObject.CompareTag("Barrier"))
        {
            //バリアスター(無敵アイテム)の場合
            Destroy(other.gameObject);
            if (this.state == State.Normal)
            {
                this.state = State.BarrierInvincible;
                //ダメージ時からの無敵状態への移行との兼ね合いで一旦ダメージ点滅へ
                StartCoroutine(damageFlash());
            }else
            {
                this.state = State.BarrierInvincible;
            }
            seAudioSource.PlayOneShot(barrierSE);
        }

        if (other.gameObject.CompareTag("Heart"))
        {
            //ハート(回復アイテム)の場合
            Destroy(other.gameObject);
            seAudioSource.PlayOneShot(healSE);
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().Heal();
        }
    }

    //ダメージ時の点滅(その間無敵)
    IEnumerator damageFlash()
    {
        //無敵時間はflashInterval1[s] * damageLoopCount1
        for (int i = 0;(i < this.damageLoopCount) && (this.state == State.DamageInvincible);i++)
        {
            //flashInterval1[s]待ってプレイヤを非表示
            yield return new WaitForSeconds(flashInterval1);
            sp.enabled = false;
            //flashInterval1[s]待ってプレイヤを表示
            yield return new WaitForSeconds(flashInterval1);
            sp.enabled = true;
            //ポーズ中は無敵時間が減らない
            if (PauseDirector.getPaused())
            {
                i--;
            }
        }

        if (this.state == State.DamageInvincible)
        {
            this.state = State.Normal; //無敵状態解除
        }else if (this.state == State.BarrierInvincible)
        {
            StartCoroutine(barrierFlash()); //無敵状態の点滅へ移行
        }
    }

    //無敵時の点滅
    IEnumerator barrierFlash()
    {
        //無敵時間は2 * (flashInterval1[s] * barrierLoopCount1 + flashInterval2[s] * barrierLoopCount2)
        //アイテムゲット直後
        for (int i = 0;i < this.barrierLoopCount1;i++)
        {
            //flashInterval1[s]待ってプレイヤを非表示
            yield return new WaitForSeconds(flashInterval1);
            sp.enabled = false;
            //flashInterval1[s]待ってプレイヤを表示
            yield return new WaitForSeconds(flashInterval1);
            sp.enabled = true;
            //ポーズ中は無敵時間が減らない
            if (PauseDirector.getPaused())
            {
                i--;
            }
        }
        seAudioSource.PlayOneShot(barrierSE2);
        //解除間近は点滅が遅くなる
        for (int i = 0; i < this.barrierLoopCount2; i++)
        {
            //flashInterval2[s]待ってプレイヤを非表示
            yield return new WaitForSeconds(flashInterval2);
            sp.enabled = false;
            //flashInterval2[s]待ってプレイヤを表示
            yield return new WaitForSeconds(flashInterval2);
            sp.enabled = true;
            //ポーズ中は無敵時間が減らない
            if (PauseDirector.getPaused())
            {
                i--;
            }
        }

        this.state = State.Normal; //無敵状態解除
    }
}
