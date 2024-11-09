using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�^�R�G�̒e�̐���X�N���v�g
public class EBallController : MonoBehaviour
{
    float eBallSpeed; //�ړ��X�s�[�h
    float incEBallSpeed; //�ړ��X�s�[�h�̏オ�蕝
    int level; //�Q�[���̃��x��(�X�s�[�h�ɉe��)

    // Start is called before the first frame update
    void Start()
    {
        eBallSpeed = -0.06f;
        this.incEBallSpeed = this.eBallSpeed * 0.1f;
        this.eBallSpeed += (float)this.level * this.incEBallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //���̃X�s�[�h�ňړ�
        transform.Translate(this.eBallSpeed, 0, 0);
        //��ʂ�ʉ߂��������
        if (transform.position.x < -3.0f)
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
