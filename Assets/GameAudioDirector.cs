using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGM�̊Ǘ�
public class GameAudioDirector : MonoBehaviour
{
    //BGM�̍Đ��p�R���|�[�l���g(�C���g�����ƃ��[�v��)
    [SerializeField] AudioSource introAudioSource;
    [SerializeField] AudioSource loopAudioSource;
    
    bool isPausingIntro; //�C���g�����Ƀ|�[�Y�������ǂ���

    //�|�[�Y����BGM�̈ꎞ��~
    public void pause()
    {
        //�C���g�����Ɉꎞ��~�������ǂ���
        isPausingIntro = introAudioSource.isPlaying;

        //BGM�̈ꎞ��~
        loopAudioSource.Pause();
        introAudioSource.Pause();
    }

    //�|�[�Y���A����BGM�̍ĊJ
    public void unpause()
    {
        //�C���g���̍ĊJ(�I�����Ă�����Ȃ�Ȃ�)
        introAudioSource.UnPause();

        if (isPausingIntro)
        {
            //�C���g�����Ɉꎞ��~�����ꍇ�̓��[�v�����̒x���Đ���ݒ肵����
            loopAudioSource.Stop();
            loopAudioSource.PlayScheduled(AudioSettings.dspTime - introAudioSource.time + ((float)introAudioSource.clip.samples / (float)introAudioSource.clip.frequency));
        }
        else
        {
            //���[�v�ɓ˓����Ă����烋�[�v�����̍ĊJ
            loopAudioSource.UnPause();
        }
    }

    //�Q�[���I�[�o�[����BGM��~
    public void stop()
    {
        introAudioSource.Stop();
        loopAudioSource.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        //BGM�Đ�(�܂��C���g�����Đ����A���̂��ƃ��[�v�����Đ�)
        introAudioSource.PlayScheduled(AudioSettings.dspTime);
        loopAudioSource.PlayScheduled(AudioSettings.dspTime + ((float)introAudioSource.clip.samples / (float)introAudioSource.clip.frequency));
    }
}
