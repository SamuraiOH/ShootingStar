using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�|�[�Y���́u�Q�[�����ĊJ����v�{�^��
//�������ƃQ�[�����ĊJ����
public class continueDirector : MonoBehaviour
{
    GameObject pausePanel; //�|�[�Y���ɕ\�������p�l��(�E�C���h�E)

    // Start is called before the first frame update
    void Start()
    {
        pausePanel = transform.parent.gameObject;
    }

    public void OnClick()
    {
        //BGM�̍ĊJ
        GameObject audioDirector = GameObject.Find("AudioDirector");
        audioDirector.GetComponent<GameAudioDirector>().unpause();
        //�|�[�Y�̉���
        PauseDirector.paused = false;
        //�|�[�Y�p�l���̔�\��
        pausePanel.SetActive(false);
    }
}
