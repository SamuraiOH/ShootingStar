using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G(�s���j�A)�̐���
//���̃X�s�[�h�ō��֓���
public class FishController : MonoBehaviour
{
    float fishSpeed; //�ړ��X�s�[�h
    float incFishSpeed; //�ړ��X�s�[�h�̏オ�蕝
    int level; //�Q�[���̃��x��(�X�s�[�h�ɉe��)

    // Start is called before the first frame update
    void Start()
    {
        this.fishSpeed = -0.04f;
        this.incFishSpeed = this.fishSpeed * 0.1f;
        this.fishSpeed += (float)this.level * this.incFishSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //���̃X�s�[�h�ňړ�
        transform.Translate(this.fishSpeed, 0, 0);
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
