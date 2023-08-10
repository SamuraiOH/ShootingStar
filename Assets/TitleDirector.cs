using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//スタートボタン(タップでスタート)
//タイトル画面の制御も
//(ハイスコア(上位3つ)の表示、BGM)
public class TitleDirector : MonoBehaviour, IPointerClickHandler
{
    //ハイスコア表示用のUIテキストと変数
    int highScore1;
    int highScore2;
    int highScore3;
    GameObject highScoreText1;
    GameObject highScoreText2;
    GameObject highScoreText3;

    [SerializeField] AudioSource titleAudioSource; //タイトルBGM用のオーディオソースコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        //ハイスコア記録が無ければ初期値を登録
        if (!PlayerPrefs.HasKey("HIGHSCORE1"))
        {
            PlayerPrefs.SetInt("HIGHSCORE1", 5000);
            PlayerPrefs.Save();
        }
        this.highScore1 = PlayerPrefs.GetInt("HIGHSCORE1");

        if (!PlayerPrefs.HasKey("HIGHSCORE2"))
        {
            PlayerPrefs.SetInt("HIGHSCORE2", 3000);
            PlayerPrefs.Save();
        }
        this.highScore2 = PlayerPrefs.GetInt("HIGHSCORE2");

        if (!PlayerPrefs.HasKey("HIGHSCORE3"))
        {
            PlayerPrefs.SetInt("HIGHSCORE3", 1500);
            PlayerPrefs.Save();
        }
        this.highScore3 = PlayerPrefs.GetInt("HIGHSCORE3");

        //ハイスコア表示
        this.highScoreText1 = GameObject.Find("HighScore1");
        this.highScoreText1.GetComponent<TextMeshProUGUI>().text = "1st: " + highScore1.ToString("D");

        this.highScoreText2 = GameObject.Find("HighScore2");
        this.highScoreText2.GetComponent<TextMeshProUGUI>().text = "2nd: " + highScore2.ToString("D");

        this.highScoreText3 = GameObject.Find("HighScore3");
        this.highScoreText3.GetComponent<TextMeshProUGUI>().text = "3rd: " + highScore3.ToString("D");

        //タイトルBGMスタート
        titleAudioSource.Play();
    }

    //ボタンタップでゲームスタート
    public void OnPointerClick(PointerEventData eventData)
    {
        //タイトルBGM停止
        titleAudioSource.Stop();
        //ゲーム画面へ
        SceneManager.LoadScene("GameScene");
    }
}
