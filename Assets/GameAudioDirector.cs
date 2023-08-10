using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGMの管理
public class GameAudioDirector : MonoBehaviour
{
    //BGMの再生用コンポーネント(イントロ部とループ部)
    [SerializeField] AudioSource introAudioSource;
    [SerializeField] AudioSource loopAudioSource;
    
    bool isPausingIntro; //イントロ中にポーズしたかどうか

    //ポーズ時のBGMの一時停止
    public void pause()
    {
        //イントロ中に一時停止したかどうか
        isPausingIntro = introAudioSource.isPlaying;

        //BGMの一時停止
        loopAudioSource.Pause();
        introAudioSource.Pause();
    }

    //ポーズ復帰時のBGMの再開
    public void unpause()
    {
        //イントロの再開(終了していたらならない)
        introAudioSource.UnPause();

        if (isPausingIntro)
        {
            //イントロ中に一時停止した場合はループ部分の遅延再生を設定し直す
            loopAudioSource.Stop();
            loopAudioSource.PlayScheduled(AudioSettings.dspTime - introAudioSource.time + ((float)introAudioSource.clip.samples / (float)introAudioSource.clip.frequency));
        }
        else
        {
            //ループに突入していたらループ部分の再開
            loopAudioSource.UnPause();
        }
    }

    //ゲームオーバー時のBGM停止
    public void stop()
    {
        introAudioSource.Stop();
        loopAudioSource.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        //BGM再生(まずイントロを再生し、そのあとループ部分再生)
        introAudioSource.PlayScheduled(AudioSettings.dspTime);
        loopAudioSource.PlayScheduled(AudioSettings.dspTime + ((float)introAudioSource.clip.samples / (float)introAudioSource.clip.frequency));
    }
}
