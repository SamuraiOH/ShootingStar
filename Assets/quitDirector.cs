using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ポーズ時の「ゲームをやめる」ボタン
//押されるとタイトルへ戻る
public class quitDirector : MonoBehaviour
{
    public void OnClick()
    {
        //BGM停止
        GameObject audioDirector = GameObject.Find("AudioDirector");
        audioDirector.GetComponent<GameAudioDirector>().stop();
        //タイトル画面へ
        SceneManager.LoadScene("TitleScene");
    }
}
