using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G(�s���j�A)�̐���
//���̃X�s�[�h�ō��֓���
public class FishController : MonoBehaviour
{
    float fishSpeed; //�ړ��X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        this.fishSpeed = -0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //���̃X�s�[�h�i�w�i�Ɠ����j�ňړ�
        transform.Translate(this.fishSpeed, 0, 0);
        //��ʂ�ʉ߂��������
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }
    }
}
