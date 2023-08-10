using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����Ƃ���1�̔w�i�摜���g���Ĕw�i�𖳌��ɍ��X�N���[��������
//(�v���C���[���E�ɓ����Ă���悤�Ɍ�����)
public class BackController : MonoBehaviour
{
    float backSpeed; //�w�i�摜�̈ړ��X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //60�t���[���ɌŒ�
        this.backSpeed = -0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //�w�i�摜����ʂ��猩�؂ꂻ���ɂȂ�����E�ɉ摜���ړ�
        if (transform.position.x + this.backSpeed < -23.0f)
        {
            transform.Translate(82.0f, 0, 0);
        }
        else
        {
            transform.Translate(this.backSpeed, 0, 0);
        }
        
    }
}
