using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�^�R�G�̒e�̐���X�N���v�g
public class EBallController : MonoBehaviour
{
    float eBallSpeed; //�ړ��X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        eBallSpeed = -0.06f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //���̃X�s�[�h�i�w�i�Ɠ����j�ňړ�
        transform.Translate(this.eBallSpeed, 0, 0);
        //��ʂ�ʉ߂��������
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }
    }
}
