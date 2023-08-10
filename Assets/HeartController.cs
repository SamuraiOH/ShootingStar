using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�n�[�g(�񕜃A�C�e��)�̐���X�N���v�g
public class HeartController : MonoBehaviour
{
    float pointSpeed; //�ړ��X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        this.pointSpeed = -0.045f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //���̃X�s�[�h�ňړ�
        transform.Translate(this.pointSpeed, 0, 0);
        //��ʂ�ʉ߂��������
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }
    }
}
