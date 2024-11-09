using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G(覐�)�̐���
//���̃X�s�[�h�ō����֓���
public class StoneController : MonoBehaviour
{
    float stoneXSpeed; //�������̈ړ��X�s�[�h
    float incStoneXSpeed; //�������̈ړ��X�s�[�h�̏オ�蕝
    float stoneYSpeed; //�c�����̈ړ��X�s�[�h
    int level; //�Q�[���̃��x��(�X�s�[�h�ɉe��)

    // Start is called before the first frame update
    void Start()
    {
        this.stoneXSpeed = -0.04f;
        this.incStoneXSpeed = this.stoneXSpeed * 0.1f;
        this.stoneXSpeed += (float)this.level * this.incStoneXSpeed;

        this.stoneYSpeed = -0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //���̃X�s�[�h�ňړ�
        transform.Translate(this.stoneXSpeed, this.stoneYSpeed, 0);
        //��ʂ�ʉ߂��������
        if ((transform.position.x < -3.0f) || (transform.position.y < -6.0f))
        {
            Destroy(gameObject);
        }
    }

    //�o�ߎ��Ԃɉ����Ĉړ��X�s�[�h�A�b�v
    public void SpeedController(int level)
    {
        this.level = level;
    }
}
