using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�G(�C�J)�̐���
//���̃X�s�[�h�ŏ㉺���Ȃ��獶�֓���
public class SquidController : MonoBehaviour
{
    float squidSpeed; //���ւ̈ړ��X�s�[�h
    float incSquidSpeed; //���ւ̈ړ��X�s�[�h�̏オ�蕝
    int angle; //�T�C���J�[�u�Ɋ�Â��㉺�^���̂��߂̊p�x
    int level; //�Q�[���̃��x��(�X�s�[�h�ɉe��)

    // Start is called before the first frame update
    void Start()
    {
        this.squidSpeed = -0.04f;
        this.incSquidSpeed = this.squidSpeed * 0.1f;
        this.squidSpeed += (float)this.level * this.incSquidSpeed;
        this.angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //x�����͈��̃X�s�[�h�ňړ�
        //y�����ɂ͏㉺�ɐU������悤�Ɉړ��i�T�C���J�[�u��`���j
        transform.Translate(this.squidSpeed, (float)(1.5f * (Math.Sin((angle + 2) * Math.PI / 180) - Math.Sin(angle * Math.PI / 180))), 0);
        //��ʂ�ʉ߂��������
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }

        if (angle >= 360)
        {
            angle = 0;
        }
        angle += 2;
    }

    //�o�ߎ��Ԃɉ����Ĉړ��X�s�[�h�A�b�v
    public void SpeedController(int level)
    {
        this.level = level;
    }
}
