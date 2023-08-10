using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//�X�^�[�g�{�^��(�^�b�v�ŃX�^�[�g)
//�^�C�g����ʂ̐����
//(�n�C�X�R�A(���3��)�̕\���ABGM)
public class TitleDirector : MonoBehaviour, IPointerClickHandler
{
    //�n�C�X�R�A�\���p��UI�e�L�X�g�ƕϐ�
    int highScore1;
    int highScore2;
    int highScore3;
    GameObject highScoreText1;
    GameObject highScoreText2;
    GameObject highScoreText3;

    [SerializeField] AudioSource titleAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //�n�C�X�R�A�L�^��������Ώ����l��o�^
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

        //�n�C�X�R�A�\��
        this.highScoreText1 = GameObject.Find("HighScore1");
        this.highScoreText1.GetComponent<TextMeshProUGUI>().text = "1st: " + highScore1.ToString("D");

        this.highScoreText2 = GameObject.Find("HighScore2");
        this.highScoreText2.GetComponent<TextMeshProUGUI>().text = "2nd: " + highScore2.ToString("D");

        this.highScoreText3 = GameObject.Find("HighScore3");
        this.highScoreText3.GetComponent<TextMeshProUGUI>().text = "3rd: " + highScore3.ToString("D");

        //�^�C�g��BGM�X�^�[�g
        titleAudioSource.Play();
    }

    //�{�^���^�b�v�ŃQ�[���X�^�[�g
    public void OnPointerClick(PointerEventData eventData)
    {
        //�^�C�g��BGM��~
        titleAudioSource.Stop();
        //�Q�[����ʂ�
        SceneManager.LoadScene("GameScene");
    }
}
