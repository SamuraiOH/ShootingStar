using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//���_�ƃ��C�t�̊Ǘ�
public class GameDirector : MonoBehaviour
{
    public static int score; //���_
    public static int life; //���C�t
    float time = 0; //���_���Z�p�̎��ԁi0.1�b�ɂ�1�_)
    GameObject scoreText; //���_�\��UI�I�u�W�F�N�g
    GameObject lifeUI;//�̗͕\��UI�I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        this.scoreText = GameObject.Find("Score"); //���_�\��UI�I�u�W�F�N�g�̎擾
        this.lifeUI = GameObject.Find("LifeUI"); //���C�t�\��UI�I�u�W�F�N�g�̎擾
        //�ϐ��̏�����
        score = 0;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        this.time += Time.deltaTime; //���Ԍv��

        //���Ԃ�0.1�b�ɂȂ����玞�Ԃ����Z�b�g���ē��_���Z
        if ((this.time >= 0.1f) && (score < 9999999))
        {
            score++;
            this.time = 0;
        }

        //���_UI�̑���
        this.scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString("D");
    }

    //�_���[�W����
    public void Damage()
    {
        life--;

        //���C�tUI�̑���
        this.lifeUI.GetComponent<Image>().fillAmount = (float)(life) / 5;

        //���C�t��0�ɂȂ�����Q�[���I�[�o�[
        if (life <= 0)
        {
            //BGM��~
            GameObject audioDirector = GameObject.Find("AudioDirector");
            audioDirector.GetComponent<GameAudioDirector>().stop();

            //�Q�[���I�[�o�[��ʂ�
            SceneManager.LoadScene("GameOverScene");
        }
    }

    //�񕜏���
    public void Heal()
    {
        //���C�t�̏����5
        if (life < 5)
        {
            life++;
            //���C�tUI�̑���
            this.lifeUI.GetComponent<Image>().fillAmount = (float)(life) / 5;
        }
    }
}
