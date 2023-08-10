using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//�Q�[���I�[�o�[���̃^�C�g���֖߂�{�^��
//���ʉ��A�X�R�A�\���i��ʊǗ��j��������
public class GameOverDirector : MonoBehaviour, IPointerClickHandler
{
    GameObject scoreText; //�l�������X�R�A��\������e�L�X�gUI

    //�n�C�X�R�A
    int highScore1; 
    int highScore2;
    int highScore3;

    //�Q�[���I�[�o�[���ʉ��p�̕ϐ�
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] public AudioClip gameoverSE;

    // Start is called before the first frame update
    void Start()
    {
        //�l���X�R�A�\��
        this.scoreText = GameObject.Find("Score");
        this.scoreText.GetComponent<TextMeshProUGUI>().text = "�X�R�A: " + GameDirector.score.ToString("D");

        //�n�C�X�R�A(���3��)�\��
        this.highScore1 = PlayerPrefs.GetInt("HIGHSCORE1");
        this.highScore2 = PlayerPrefs.GetInt("HIGHSCORE2");
        this.highScore3 = PlayerPrefs.GetInt("HIGHSCORE3");

        //�n�C�X�R�A�X�V
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

        //���ʉ�
        seAudioSource.PlayOneShot(gameoverSE);
    }

    //�{�^���^�b�v�Ń^�C�g����
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("TitleScene");
    }
}
