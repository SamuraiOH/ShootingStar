using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�v���C���[���X���C�v�œ������X�N���v�g
//�X���C�v����Ƃ��̕����ɓ���������
//�����蔻��Ƃ���ɔ������ʉ��������ŏ���
public class BossyController : MonoBehaviour
{
    enum Direction //�v���C���[�̈ړ�������\���񋓌^
    {
        Stop, //��~
        Up, //��
        Down, //��
        Left, //��(���)
        Right //�E(�O�i)
    }

    enum State //�v���C���[�̏��
    {
        Normal, //�ʏ�
        DamageInvincible, //�_���[�W���̖��G���
        BarrierInvincible //�A�C�e���ɂ�閳�G���
    }

    float speed; //�ړ��X�s�[�h
    Vector2 startPos; //�w���G�ꂽ���W
    Vector2 endPos; //�w�����ꂽ���W
    Vector2 moveVector; //�X���C�v�̃x�N�g��
    Direction direcction; //���݂̃v���C���[�̈ړ�����(�����͒�~)
    State state; //�v���C���̏��
    bool flag; //�|�[�Y��Ԃ̗L��(�ĊJ���|�[�Y���̕������ێ����邽��)
    Direction pauseDirection; //�|�[�Y���̈ړ�������ۑ�����ϐ�
    [SerializeField] float flashInterval1; //�_�ł̊Ԋu[s]
    [SerializeField] float flashInterval2; //�_�ł̊Ԋu[s](�������߂���)
    [SerializeField] int damageLoopCount; //�_���[�W���̓_�ł̉�
    [SerializeField] int barrierLoopCount1; //�A�C�e���ɂ�閳�G�̓_�ŉ�
    [SerializeField] int barrierLoopCount2; //�A�C�e���ɂ�閳�G�̓_�ŉ�(�������߂��Ƃ�)
    SpriteRenderer sp; //�_�ŗp�̃X�v���C�g�����_���[

    //���ʉ��p�̕ϐ�
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] public AudioClip damageSE;
    [SerializeField] public AudioClip healSE;
    [SerializeField] public AudioClip barrierSE;
    [SerializeField] public AudioClip barrierSE2;
    [SerializeField] public AudioClip life1SE;
    [SerializeField] public AudioClip barrierHitSE;

    //�����ݒ�
    // Start is called before the first frame update
    void Start()
    {
        //�ϐ��̏�����
        this.sp = GetComponent<SpriteRenderer>();
        this.speed = 0.06f;
        this.direcction = Direction.Stop;
        this.pauseDirection = Direction.Stop;
        this.flag = false;
        this.state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            this.pauseDirection = this.direcction;
            this.flag = true;
            return;
        }

        //�X���C�v�̊J�n
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        //�X���C�v�̏I��
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            moveVector = endPos - startPos;
            //�X���C�v�̕����ɉ����Ĉړ�����������
            //�X���C�v�x�N�g���̊e�����̐����Ɛ�Βl�̑傫������㉺���E�̂����ꂩ�Ɍ���
            if (Math.Abs(moveVector.x) > Math.Abs(moveVector.y))
            {
                if (moveVector.x > 0)
                {
                    direcction = Direction.Right;
                }else if (moveVector.x < 0)
                {
                    direcction = Direction.Left;
                }
            }else if (Math.Abs(moveVector.x) < Math.Abs(moveVector.y))
            {
                if (moveVector.y > 0)
                {
                    direcction = Direction.Up;
                }
                else if (moveVector.y < 0)
                {
                    direcction = Direction.Down;
                }
            }
        }

        if (this.flag)
        {
            //�|�[�Y���������ꂽ��ړ��������|�[�Y���̂��̂�
            this.direcction = this.pauseDirection;
            this.flag = false;
        }

        //���݂̈ړ������ɏ]����ʓ��͈̔͂Ńv���C�����ړ�������(�㉺�̌��E�ɒB������t������)
        if ((direcction == Direction.Up) && (transform.position.y < 4.5f))
        {
            transform.Translate(0, this.speed, 0);
        }
        else if ((direcction == Direction.Up) && (transform.position.y >= 4.5f))
        {
            direcction = Direction.Down;
        }
        else if ((direcction == Direction.Down) && (transform.position.y > -4.5f))
        {
            transform.Translate(0, -1 * this.speed, 0);
        }
        else if ((direcction == Direction.Down) && (transform.position.y <= -4.5f))
        {
            direcction = Direction.Up;
        }
        else if ((direcction == Direction.Left) && (transform.position.x > -2.5f))
        {
            transform.Translate(-1 * this.speed, 0, 0);
        }
        else if ((direcction == Direction.Right) && (transform.position.x < 2.5f))
        {
            transform.Translate(this.speed, 0, 0);
        }
    }

    //�I�u�W�F�N�g�Ƃ̐ڐG���̏���
    void OnTriggerEnter2D(Collider2D other)
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //�G�Ƃ̐ڐG
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            if (this.state == State.Normal)
            {
                //�ʏ��Ԃ̏ꍇ
                GameObject director = GameObject.Find("GameDirector");
                director.GetComponent<GameDirector>().Damage();
                if (GameDirector.life == 1)
                {
                    seAudioSource.PlayOneShot(life1SE);
                }
                else if(GameDirector.life > 1)
                {
                    seAudioSource.PlayOneShot(damageSE);
                }
                this.state = State.DamageInvincible; //���G��Ԃֈڍs�i3�b�j
                StartCoroutine(damageFlash()); //�_��
            }else if (this.state == State.BarrierInvincible)
            {
                //�A�C�e���ɂ�閳�G��Ԃ̏ꍇ
                if (GameDirector.score + 50 < 9999999)
                {
                    GameDirector.score += 50;
                }else if (GameDirector.score < 9999999)
                {
                    GameDirector.score = 9999999;
                }
                seAudioSource.PlayOneShot(barrierHitSE);
            }

        }

        //�A�C�e���Ƃ̐ڐG
        if (other.gameObject.CompareTag("Barrier"))
        {
            //�o���A�X�^�[(���G�A�C�e��)�̏ꍇ
            Destroy(other.gameObject);
            if (this.state == State.Normal)
            {
                this.state = State.BarrierInvincible;
                //�_���[�W������̖��G��Ԃւ̈ڍs�Ƃ̌��ˍ����ň�U�_���[�W�_�ł�
                StartCoroutine(damageFlash());
            }else
            {
                this.state = State.BarrierInvincible;
            }
            seAudioSource.PlayOneShot(barrierSE);
        }

        if (other.gameObject.CompareTag("Heart"))
        {
            //�n�[�g(�񕜃A�C�e��)�̏ꍇ
            Destroy(other.gameObject);
            seAudioSource.PlayOneShot(healSE);
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().Heal();
        }
    }

    //�_���[�W���̓_��(���̊Ԗ��G)
    IEnumerator damageFlash()
    {
        //���G���Ԃ�flashInterval1[s] * damageLoopCount1
        for (int i = 0;(i < this.damageLoopCount) && (this.state == State.DamageInvincible);i++)
        {
            //flashInterval1[s]�҂��ăv���C�����\��
            yield return new WaitForSeconds(flashInterval1);
            sp.enabled = false;
            //flashInterval1[s]�҂��ăv���C����\��
            yield return new WaitForSeconds(flashInterval1);
            sp.enabled = true;
            //�|�[�Y���͖��G���Ԃ�����Ȃ�
            if (PauseDirector.getPaused())
            {
                i--;
            }
        }

        if (this.state == State.DamageInvincible)
        {
            this.state = State.Normal; //���G��ԉ���
        }else if (this.state == State.BarrierInvincible)
        {
            StartCoroutine(barrierFlash()); //���G��Ԃ̓_�łֈڍs
        }
    }

    //���G���̓_��
    IEnumerator barrierFlash()
    {
        //���G���Ԃ�2 * (flashInterval1[s] * barrierLoopCount1 + flashInterval2[s] * barrierLoopCount2)
        //�A�C�e���Q�b�g����
        for (int i = 0;i < this.barrierLoopCount1;i++)
        {
            //flashInterval1[s]�҂��ăv���C�����\��
            yield return new WaitForSeconds(flashInterval1);
            sp.enabled = false;
            //flashInterval1[s]�҂��ăv���C����\��
            yield return new WaitForSeconds(flashInterval1);
            sp.enabled = true;
            //�|�[�Y���͖��G���Ԃ�����Ȃ�
            if (PauseDirector.getPaused())
            {
                i--;
            }
        }
        seAudioSource.PlayOneShot(barrierSE2);
        //�����ԋ߂͓_�ł��x���Ȃ�
        for (int i = 0; i < this.barrierLoopCount2; i++)
        {
            //flashInterval2[s]�҂��ăv���C�����\��
            yield return new WaitForSeconds(flashInterval2);
            sp.enabled = false;
            //flashInterval2[s]�҂��ăv���C����\��
            yield return new WaitForSeconds(flashInterval2);
            sp.enabled = true;
            //�|�[�Y���͖��G���Ԃ�����Ȃ�
            if (PauseDirector.getPaused())
            {
                i--;
            }
        }

        this.state = State.Normal; //���G��ԉ���
    }
}
