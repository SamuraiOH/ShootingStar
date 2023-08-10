using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�|�[�Y�{�^���������ƃ|�[�Y���Ăяo��
public class PauseDirector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject pausePanel; //�|�[�Y���ɌĂяo���p�l��
    public static bool paused; //�|�[�Y���Ă��邩�ǂ���

    //���̃N���X����̎Q�Ɨp
    public static bool getPaused()
    {
        return paused;
    }

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��̓|�[�Y���Ă��Ȃ��̂Ńp�l���͔�\��
        pausePanel.SetActive(false);
        paused = false;
    }

    //�|�[�Y�{�^���^�b�v�Ń|�[�Y��ʕ\��
    public void OnPointerClick(PointerEventData eventData)
    {
        if (paused == false)
        {
            //�|�[�Y
            pausePanel.SetActive(true);
            paused = true;
            //BGM�ꎞ��~
            GameObject audioDirector = GameObject.Find("AudioDirector");
            audioDirector.GetComponent<GameAudioDirector>().pause();
        }
    }
}
