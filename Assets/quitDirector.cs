using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�|�[�Y���́u�Q�[������߂�v�{�^��
//�������ƃ^�C�g���֖߂�
public class quitDirector : MonoBehaviour
{
    public void OnClick()
    {
        //BGM��~
        GameObject audioDirector = GameObject.Find("AudioDirector");
        audioDirector.GetComponent<GameAudioDirector>().stop();
        //�^�C�g����ʂ�
        SceneManager.LoadScene("TitleScene");
    }
}
