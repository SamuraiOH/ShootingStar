using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ポーズ時の「ゲームを再開する」ボタン
//押されるとゲームを再開する
public class continueDirector : MonoBehaviour
{
    GameObject pausePanel; //ポーズ時に表示されるパネル(ウインドウ)

    // Start is called before the first frame update
    void Start()
    {
        pausePanel = transform.parent.gameObject;
    }

    public void OnClick()
    {
        //BGMの再開
        GameObject audioDirector = GameObject.Find("AudioDirector");
        audioDirector.GetComponent<GameAudioDirector>().unpause();
        //ポーズの解除
        PauseDirector.paused = false;
        //ポーズパネルの非表示
        pausePanel.SetActive(false);
    }
}
