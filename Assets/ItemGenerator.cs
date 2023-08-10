using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A�C�e�������̎����Ő�������
public class ItemGenerator : MonoBehaviour
{
    public GameObject barrierPrefab; //�o���A�X�^�[��Prefab
    float barrierSpan; //�o���A�X�^�[�̔�������[s]
    float barrierDelta; //�o���A�X�^�[�p�̌��ݎ���[s]
    public GameObject heartPrefab; //�n�[�g��Prefab
    float heartSpan; //�n�[�g�̔�������[s]
    float heartDelta; //�n�[�g�p�̌��ݎ���[s]

    // Start is called before the first frame update
    void Start()
    {
        //�ϐ��̏�����
        this.barrierSpan = 50.0f;
        this.barrierDelta = 0;
        this.heartSpan = 25.0f;
        this.heartDelta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //�t���[�����ƂɌo�ߎ��Ԃ����Z
        this.barrierDelta += Time.deltaTime;
        this.heartDelta += Time.deltaTime;

        //�A�C�e���o��
        //�A�C�e�����̌o�ߎ���(delta)���o���X�p��(span)�ɒB����Əo��
        //�A�C�e���͌��߂�ꂽ�ʒu���烉���_���ɏo��
        if (this.barrierDelta >= this.barrierSpan)
        {
            //�o���A�X�^�[�o��
            this.barrierDelta = 0;
            GameObject goBarrier = Instantiate(barrierPrefab);
            int y = Random.Range(-18, 19);
            float py = y / 4;
            goBarrier.transform.position = new Vector3(3.0f, py, 0);
        }

        if (this.heartDelta >= this.heartSpan)
        {
            //�n�[�g�o��
            this.heartDelta = 0;
            GameObject goHeart = Instantiate(heartPrefab);
            int y = Random.Range(-18, 19);
            float py = y / 4;
            goHeart.transform.position = new Vector3(3.0f, py, 0);
            this.heartSpan = 50.0f;
        }
    }
}
