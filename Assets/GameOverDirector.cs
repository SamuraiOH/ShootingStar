using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//ゲームオーバー時のタイトルへ戻るボタン
//効果音、スコア表示（画面管理）もここで
public class GameOverDirector : MonoBehaviour, IPointerClickHandler
{
    GameObject scoreText; //獲得したスコアを表示するテキストUI

    //ハイスコア
    int highScore1; 
    int highScore2;
    int highScore3;

    //ゲームオーバー効果音用の変数
    [SerializeField] AudioSource seAudioSource; //オーディオソースコンポーネント
    [SerializeField] public AudioClip gameoverSE; //ゲームオーバーSE

    // Start is called before the first frame update
    void Start()
    {
        //獲得スコア表示
        this.scoreText = GameObject.Find("Score");
        this.scoreText.GetComponent<TextMeshProUGUI>().text = "スコア: " + GameDirector.score.ToString("D");

        //ハイスコア(上位3つ)表示
        this.highScore1 = PlayerPrefs.GetInt("HIGHSCORE1");
        this.highScore2 = PlayerPrefs.GetInt("HIGHSCORE2");
        this.highScore3 = PlayerPrefs.GetInt("HIGHSCORE3");

        //ハイスコア更新
        if (GameDirector.score > this.highScore1)
        {
            this.highScore3 = this.highScore2;
            this.highScore2 = this.highScore1;
            this.highScore1 = GameDirector.score;

            PlayerPrefs.SetInt("HIGHSCORE1", this.highScore1);
            PlayerPrefs.SetInt("HIGHSCORE2", this.highScore2);
            PlayerPrefs.SetInt("HIGHSCORE3", this.highScore3);
            PlayerPrefs.Save();
        }
        else if (GameDirector.score > this.highScore2)
        {
            this.highScore3 = this.highScore2;
            this.highScore2 = GameDirector.score;

            PlayerPrefs.SetInt("HIGHSCORE2", this.highScore2);
            PlayerPrefs.SetInt("HIGHSCORE3", this.highScore3);
            PlayerPrefs.Save();
        }
        else if (GameDirector.score > this.highScore3)
        {
            this.highScore3 = GameDirector.score;
            PlayerPrefs.SetInt("HIGHSCORE3", this.highScore3);
            PlayerPrefs.Save();
        }

        //効果音
        seAudioSource.PlayOneShot(gameoverSE);
    }

    //ボタンタップでタイトルへ
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("TitleScene");
    }
}
