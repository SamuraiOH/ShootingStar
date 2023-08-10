using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G(覐�)�̐���
//���̃X�s�[�h�ō����֓���
public class StoneController : MonoBehaviour
{
    float stoneXSpeed; //�������̈ړ��X�s�[�h
    float stoneYSpeed; //�c�����̈ړ��X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        this.stoneXSpeed = -0.04f;
        this.stoneYSpeed = -0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //���̃X�s�[�h�i�w�i�Ɠ����j�ňړ�
        transform.Translate(this.stoneXSpeed, this.stoneYSpeed, 0);
        //��ʂ�ʉ߂��������
        if ((transform.position.x < -3.0f) || (transform.position.y < -6.0f))
        {
            Destroy(gameObject);
        }
    }
}
