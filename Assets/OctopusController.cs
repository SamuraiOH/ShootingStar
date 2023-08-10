using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G(�^�R)�̐���
//���̃X�s�[�h�ō��֓�����1��e�𔭎�
public class OctopusController : MonoBehaviour
{
    float octopusSpeed; //�ړ��X�s�[�h
    public GameObject enemyballPrefab; //�e�𐶐�����Prefab
    bool attackFlag; //�U���t���O(�U���𔭎˂������ǂ����̔��f)

    //�e���˂̌��ʉ�
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] public AudioClip attackSE;

    // Start is called before the first frame update
    void Start()
    {
        this.octopusSpeed = -0.04f;
        this.attackFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //���̃X�s�[�h�i�w�i�Ɠ����j�ňړ�
        transform.Translate(this.octopusSpeed, 0, 0);
        //��ʂ�ʉ߂��������
        if (transform.position.x < -3.0f)
        {
            Destroy(gameObject);
        }
        //x���W��2��荶�ɂȂ��1�x�����e�𔭎�
        if ((transform.position.x <= 2.0f) && (attackFlag == true))
        {
            GameObject go = Instantiate(enemyballPrefab);
            float octopusYPosition = transform.position.y;
            go.transform.position = new Vector3(1.5f, octopusYPosition, 0);
            attackFlag = false;
            //���ʉ�
            seAudioSource.PlayOneShot(attackSE);
        }
    }
}