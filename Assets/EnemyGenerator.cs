using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵生成スクリプト
public class EnemyGenerator : MonoBehaviour
{
    //敵毎のPrefabと生成スパン[s]、現在の時間[s]
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

    //敵出現フラグ(パターン4で使う)
    bool fishFlag;
    bool octopusFlag;
    bool stoneFlag;
    bool squidFlag;

    //現在の時間[s](出現パターン管理用)
    float gameTime;

    //敵の出現パターン
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

        //開始から75sまでパターン0
        this.pattern = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //フレームごとに経過時間を加算
        this.gameTime += Time.deltaTime;

        //経過時間(gameTime)ごとに出現パターンを決定
        //パターンは0→1→2→3→4
        if (this.gameTime >= 350.0f)
        {
            //350sからパターン4
            //フラグを用いて50s毎に出現する敵を切り替える
            this.pattern = 4;
            
            if (this.gameTime >= 600.0f)
            {
                //一周したらパターン4の最初に移行
                this.gameTime -= 250.0f;
            }
            else if (this.gameTime >= 550.0f)
            {
                //550s～600sまで全ての敵が出現
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
                //500～550sまでピラニア以外が出現
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
                //450s～500sまで隕石以外が出現
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
                //400～450sまでタコ以外が出現
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
                //350～400sまでイカ以外が出現
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
            //250s～350sまでパターン3
            this.pattern = 3;
        }
        else if (this.gameTime >= 150.0f)
        {
            //150s～250sまでパターン2
            this.pattern = 2;
        }
        else if (this.gameTime >= 75.0f)
        {
            //75s～150sまでパターン1
            this.pattern = 1;
        }
        //※開始から75sまでパターン0

        //敵出現
        //敵毎の経過時間(delta)が出現スパン(span)に達すると出現
        //敵は敵毎に決められた位置からランダムに出現
        if (this.pattern == 4) //パターン4の場合
        {
            //フラグが立っている敵が出現
            this.fishDelta += Time.deltaTime;
            this.octopusDelta += Time.deltaTime;
            this.stoneDelta += Time.deltaTime;
            this.squidDelta += Time.deltaTime;

            //ピラニア出現
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

            //タコ出現
            if ((this.octopusDelta >= this.octopusSpan) && octopusFlag)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
            }

            //隕石出現
            if ((this.stoneDelta >= this.stoneSpan) && stoneFlag)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
            }

            //イカ出現
            if ((this.squidDelta >= this.squidSpan) && squidFlag)
            {
                this.squidDelta = 0;
                GameObject goSquid = Instantiate(squidPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goSquid.transform.position = new Vector3(3.0f, py, 0);
            }
        }
        else if (this.pattern == 3) //パターン3の場合
        {
            //全ての敵が出現
            this.fishSpan = 1.5f;
            this.fishDelta += Time.deltaTime;
            this.octopusSpan = 2.0f;
            this.octopusDelta += Time.deltaTime;
            this.stoneSpan = 3.0f;
            this.stoneDelta += Time.deltaTime;
            this.squidSpan = 3.5f;
            this.squidDelta += Time.deltaTime;

            //ピラニア出現
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

            //タコ出現
            if (this.octopusDelta >= this.octopusSpan)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
            }

            //隕石出現
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
            }

            //イカ出現
            if (this.squidDelta >= this.squidSpan)
            {
                this.squidDelta = 0;
                GameObject goSquid = Instantiate(squidPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goSquid.transform.position = new Vector3(3.0f, py, 0);
            }
        }
        else if(this.pattern == 2) //パターン2の場合
        {
            //イカ以外が出現
            this.fishSpan = 1.5f;
            this.fishDelta += Time.deltaTime;
            this.octopusSpan = 2.5f;
            this.octopusDelta += Time.deltaTime;
            this.stoneSpan = 2.0f;
            this.stoneDelta += Time.deltaTime;

            //ピラニア出現
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

            //タコ出現
            if (this.octopusDelta >= this.octopusSpan)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
            }

            //隕石出現
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
            }
        }
        else if(this.pattern == 1) //パターン1の場合
        {
            //ピラニアと隕石が出現
            this.fishSpan = 1.0f;
            this.fishDelta += Time.deltaTime;
            this.stoneSpan = 2.0f;
            this.stoneDelta += Time.deltaTime;

            //ピラニア出現
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

            //隕石出現
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
            }
        }
        else //パターン0の場合
        {
            //ピラニアのみ出現
            this.fishDelta += Time.deltaTime;

            //ピラニア出現
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
