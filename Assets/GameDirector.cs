using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//得点とライフの管理
public class GameDirector : MonoBehaviour
{
    public static int score; //得点
    public static int life; //ライフ
    float time = 0; //得点加算用の時間（0.1秒につき1点)
    GameObject scoreText; //得点表示UIオブジェクト
    GameObject lifeUI;//体力表示UIオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        this.scoreText = GameObject.Find("Score"); //得点表示UIオブジェクトの取得
        this.lifeUI = GameObject.Find("LifeUI"); //ライフ表示UIオブジェクトの取得
        //変数の初期化
        score = 0;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        this.time += Time.deltaTime; //時間計測

        //時間が0.1秒になったら時間をリセットして得点加算
        if ((this.time >= 0.1f) && (score < 9999999))
        {
            score++;
            this.time = 0;
        }

        //得点UIの操作
        this.scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString("D");
    }

    //ダメージ処理
    public void Damage()
    {
        life--;

        //ライフUIの操作
        this.lifeUI.GetComponent<Image>().fillAmount = (float)(life) / 5;

        //ライフが0になったらゲームオーバー
        if (life <= 0)
        {
            //BGM停止
            GameObject audioDirector = GameObject.Find("AudioDirector");
            audioDirector.GetComponent<GameAudioDirector>().stop();

            //ゲームオーバー画面へ
            SceneManager.LoadScene("GameOverScene");
        }
    }

    //回復処理
    public void Heal()
    {
        //ライフの上限は5
        if (life < 5)
        {
            life++;
            //ライフUIの操作
            this.lifeUI.GetComponent<Image>().fillAmount = (float)(life) / 5;
        }
    }
}
