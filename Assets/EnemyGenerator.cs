using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G�����X�N���v�g
public class EnemyGenerator : MonoBehaviour
{
    //�G����Prefab�Ɛ����X�p��[s]�A���݂̎���[s]
    public GameObject fishPrefab;
    float fishSpan;
    float fishDelta;
    public GameObject octopusPrefab;
    float octopusSpan;
    float octopusDelta;
    public GameObject stonePrefab;
    float stoneSpan;
    float stoneDelta;
    public GameObject squidPrefab;
    float squidSpan;
    float squidDelta;

    //�G�o���t���O(�p�^�[��4�Ŏg��)
    bool fishFlag;
    bool octopusFlag;
    bool stoneFlag;
    bool squidFlag;

    //���݂̎���[s](�o���p�^�[���Ǘ��p)
    float gameTime;

    //�G�̏o���p�^�[��
    int pattern;

    // Start is called before the first frame update
    void Start()
    {
        this.fishSpan = 0.8f;
        this.fishDelta = 0;
        this.octopusDelta = 0;
        this.stoneDelta = 0;
        this.squidDelta = 0;
        this.gameTime = 0;
        this.fishFlag = true;
        this.octopusFlag = true;
        this.stoneFlag = true;
        this.squidFlag = true;

        //�J�n����75s�܂Ńp�^�[��0
        this.pattern = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//�|�[�Y���͏�����~
        {
            return;
        }

        //�t���[�����ƂɌo�ߎ��Ԃ����Z
        this.gameTime += Time.deltaTime;

        //�o�ߎ���(gameTime)���Ƃɏo���p�^�[��������
        //�p�^�[����0��1��2��3��4
        if (this.gameTime >= 350.0f)
        {
            //350s����p�^�[��4
            //�t���O��p����50s���ɏo������G��؂�ւ���
            this.pattern = 4;
            
            if (this.gameTime >= 600.0f)
            {
                //���������p�^�[��4�̍ŏ��Ɉڍs
                this.gameTime -= 250.0f;
            }
            else if (this.gameTime >= 550.0f)
            {
                //550s�`600s�܂őS�Ă̓G���o��
                this.fishFlag = true;
                this.octopusFlag = true;
                this.stoneFlag = true;
                this.squidFlag = true;

                this.fishSpan = 1.25f;
                this.octopusSpan = 1.75f;
                this.stoneSpan = 2.5f;
                this.squidSpan = 2.5f;
            }
            else if (this.gameTime >= 500.0f)
            {
                //500�`550s�܂Ńs���j�A�ȊO���o��
                this.fishFlag = false;
                this.octopusFlag = true;
                this.stoneFlag = true;
                this.squidFlag = true;

                this.octopusSpan = 1.6f;
                this.stoneSpan = 2.25f;
                this.squidSpan = 2.4f;
            }
            else if (this.gameTime >= 450.0f)
            {
                //450s�`500s�܂�覐ΈȊO���o��
                this.fishFlag = true;
                this.octopusFlag = true;
                this.stoneFlag = false;
                this.squidFlag = true;

                this.fishSpan = 1.15f;
                this.octopusSpan = 1.7f;
                this.squidSpan = 2.25f;
            }
            else if (this.gameTime >= 400.0f)
            {
                //400�`450s�܂Ń^�R�ȊO���o��
                this.fishFlag = true;
                this.octopusFlag = false;
                this.stoneFlag = true;
                this.squidFlag = true;

                this.fishSpan = 1.15f;
                this.stoneSpan = 2.3f;
                this.squidSpan = 2.35f;
            }
            else if (this.gameTime >= 350.0f)
            {
                //350�`400s�܂ŃC�J�ȊO���o��
                this.fishFlag = true;
                this.octopusFlag = true;
                this.stoneFlag = true;
                this.squidFlag = false;

                this.fishSpan = 1.15f;
                this.octopusSpan = 1.6f;
                this.stoneSpan = 2.3f;
            }
        }
        else if (this.gameTime >= 250.0f)
        {
            //250s�`350s�܂Ńp�^�[��3
            this.pattern = 3;
        }
        else if (this.gameTime >= 150.0f)
        {
            //150s�`250s�܂Ńp�^�[��2
            this.pattern = 2;
        }
        else if (this.gameTime >= 75.0f)
        {
            //75s�`150s�܂Ńp�^�[��1
            this.pattern = 1;
        }
        //���J�n����75s�܂Ńp�^�[��0

        //�G�o��
        //�G���̌o�ߎ���(delta)���o���X�p��(span)�ɒB����Əo��
        //�G�͓G���Ɍ��߂�ꂽ�ʒu���烉���_���ɏo��
        if (this.pattern == 4) //�p�^�[��4�̏ꍇ
        {
            //�t���O�������Ă���G���o��
            this.fishDelta += Time.deltaTime;
            this.octopusDelta += Time.deltaTime;
            this.stoneDelta += Time.deltaTime;
            this.squidDelta += Time.deltaTime;

            //�s���j�A�o��
            if ((this.fishDelta >= this.fishSpan) && fishFlag)
            {
                this.fishDelta = 0;
                GameObject goFish = Instantiate(fishPrefab);
                int x = Random.Range(0, 2);
                float px = 4.0f + x;
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
            }

            //�^�R�o��
            if ((this.octopusDelta >= this.octopusSpan) && octopusFlag)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
            }

            //覐Ώo��
            if ((this.stoneDelta >= this.stoneSpan) && stoneFlag)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
            }

            //�C�J�o��
            if ((this.squidDelta >= this.squidSpan) && squidFlag)
            {
                this.squidDelta = 0;
                GameObject goSquid = Instantiate(squidPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goSquid.transform.position = new Vector3(3.0f, py, 0);
            }
        }
        else if (this.pattern == 3) //�p�^�[��3�̏ꍇ
        {
            //�S�Ă̓G���o��
            this.fishSpan = 1.5f;
            this.fishDelta += Time.deltaTime;
            this.octopusSpan = 2.0f;
            this.octopusDelta += Time.deltaTime;
            this.stoneSpan = 3.0f;
            this.stoneDelta += Time.deltaTime;
            this.squidSpan = 3.5f;
            this.squidDelta += Time.deltaTime;

            //�s���j�A�o��
            if (this.fishDelta >= this.fishSpan)
            {
                this.fishDelta = 0;
                GameObject goFish = Instantiate(fishPrefab);
                int x = Random.Range(0, 2);
                float px = 4.0f + x;
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
            }

            //�^�R�o��
            if (this.octopusDelta >= this.octopusSpan)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
            }

            //覐Ώo��
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
            }

            //�C�J�o��
            if (this.squidDelta >= this.squidSpan)
            {
                this.squidDelta = 0;
                GameObject goSquid = Instantiate(squidPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goSquid.transform.position = new Vector3(3.0f, py, 0);
            }
        }
        else if(this.pattern == 2) //�p�^�[��2�̏ꍇ
        {
            //�C�J�ȊO���o��
            this.fishSpan = 1.5f;
            this.fishDelta += Time.deltaTime;
            this.octopusSpan = 2.5f;
            this.octopusDelta += Time.deltaTime;
            this.stoneSpan = 2.0f;
            this.stoneDelta += Time.deltaTime;

            //�s���j�A�o��
            if (this.fishDelta >= this.fishSpan)
            {
                this.fishDelta = 0;
                GameObject goFish = Instantiate(fishPrefab);
                int x = Random.Range(0, 2);
                float px = 4.0f + x;
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
            }

            //�^�R�o��
            if (this.octopusDelta >= this.octopusSpan)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
            }

            //覐Ώo��
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
            }
        }
        else if(this.pattern == 1) //�p�^�[��1�̏ꍇ
        {
            //�s���j�A��覐΂��o��
            this.fishSpan = 1.0f;
            this.fishDelta += Time.deltaTime;
            this.stoneSpan = 2.0f;
            this.stoneDelta += Time.deltaTime;

            //�s���j�A�o��
            if (this.fishDelta >= this.fishSpan)
            {
                this.fishDelta = 0;
                GameObject goFish = Instantiate(fishPrefab);
                int x = Random.Range(0, 2);
                float px = 4.0f + x;
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
            }

            //覐Ώo��
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
            }
        }
        else //�p�^�[��0�̏ꍇ
        {
            //�s���j�A�̂ݏo��
            this.fishDelta += Time.deltaTime;

            //�s���j�A�o��
            if (this.fishDelta >= this.fishSpan)
            {
                this.fishDelta = 0;
                GameObject goFish = Instantiate(fishPrefab);
                int x = Random.Range(-1, 2);
                float px = 3.5f + (x / 2);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
            }
        }
    }
}
