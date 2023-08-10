using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//ポーズボタンを押すとポーズを呼び出す
public class PauseDirector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject pausePanel; //ポーズ時に呼び出すパネル
    public static bool paused; //ポーズしているかどうか

    //他のクラスからの参照用
    public static bool getPaused()
    {
        return paused;
    }

    // Start is called before the first frame update
    void Start()
    {
        //最初はポーズしていないのでパネルは非表示
        pausePanel.SetActive(false);
        paused = false;
    }

    //ポーズボタンタップでポーズ画面表示
    public void OnPointerClick(PointerEventData eventData)
    {
        if (paused == false)
        {
            //ポーズ
            pausePanel.SetActive(true);
            paused = true;
            //BGM一時停止
            GameObject audioDirector = GameObject.Find("AudioDirector");
            audioDirector.GetComponent<GameAudioDirector>().pause();
        }
    }
}
