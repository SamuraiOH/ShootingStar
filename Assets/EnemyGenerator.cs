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

    //ゲームのレベル(敵のスピードや出現頻度、得点に影響)
    int level;
    //ゲームのレベル上限
    int maxLevel;
    //レベルアップフラグ
    bool levelUpFlag;

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

        //敵のスピードレベル関連
        this.level = 0;
        this.maxLevel = 5;
        this.levelUpFlag = true;
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
                //敵スピードアップフラグ起動
                this.levelUpFlag = true;
            }
            else if (this.gameTime >= 550.0f)
            {
                //550s～600sまで全ての敵が出現
                this.fishFlag = true;
                this.octopusFlag = true;
                this.stoneFlag = true;
                this.squidFlag = true;

                this.fishSpan = 1.25f * (1.0f - 0.05f * (float)this.level);
                this.octopusSpan = 1.75f * (1.0f - 0.05f * (float)this.level);
                this.stoneSpan = 2.5f * (1.0f - 0.05f * (float)this.level);
                this.squidSpan = 2.5f * (1.0f - 0.05f * (float)this.level);
            }
            else if (this.gameTime >= 500.0f)
            {
                //500～550sまでピラニア以外が出現
                this.fishFlag = false;
                this.octopusFlag = true;
                this.stoneFlag = true;
                this.squidFlag = true;

                this.octopusSpan = 1.6f * (1.0f - 0.05f * (float)this.level);
                this.stoneSpan = 2.25f * (1.0f - 0.05f * (float)this.level);
                this.squidSpan = 2.4f * (1.0f - 0.05f * (float)this.level);
            }
            else if (this.gameTime >= 450.0f)
            {
                //450s～500sまで隕石以外が出現
                this.fishFlag = true;
                this.octopusFlag = true;
                this.stoneFlag = false;
                this.squidFlag = true;

                this.fishSpan = 1.15f * (1.0f - 0.05f * (float)this.level);
                this.octopusSpan = 1.7f * (1.0f - 0.05f * (float)this.level);
                this.squidSpan = 2.25f * (1.0f - 0.05f * (float)this.level);
            }
            else if (this.gameTime >= 400.0f)
            {
                //400～450sまでタコ以外が出現
                this.fishFlag = true;
                this.octopusFlag = false;
                this.stoneFlag = true;
                this.squidFlag = true;

                this.fishSpan = 1.15f * (1.0f - 0.05f * (float)this.level);
                this.stoneSpan = 2.3f * (1.0f - 0.05f * (float)this.level);
                this.squidSpan = 2.35f * (1.0f - 0.05f * (float)this.level);
            }
            else if (this.gameTime >= 350.0f)
            {
                //敵のスピードレベルアップ
                if (this.levelUpFlag && (this.level < this.maxLevel))
                {
                    this.level++;
                    this.levelUpFlag = false;
                    GameObject director = GameObject.Find("GameDirector");
                    director.GetComponent<GameDirector>().LevelUp();
                }
                //350～400sまでイカ以外が出現
                this.fishFlag = true;
                this.octopusFlag = true;
                this.stoneFlag = true;
                this.squidFlag = false;

                this.fishSpan = 1.15f * (1.0f - 0.05f * (float)this.level);
                this.octopusSpan = 1.6f * (1.0f - 0.05f * (float)this.level);
                this.stoneSpan = 2.3f * (1.0f - 0.05f * (float)this.level);
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
            if ((this.fishDelta >= this.fishSpan) && this.fishFlag)
            {
                this.fishDelta = 0;
                GameObject goFish = Instantiate(this.fishPrefab);
                int x = Random.Range(0, 2);
                float px = 4.0f + x;
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
                goFish.GetComponent<FishController>().SpeedController(this.level);
            }

            //タコ出現
            if ((this.octopusDelta >= this.octopusSpan) && this.octopusFlag)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(this.octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
                goOctopus.GetComponent<OctopusController>().SpeedController(this.level);
            }

            //隕石出現
            if ((this.stoneDelta >= this.stoneSpan) && this.stoneFlag)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(this.stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
                goStone.GetComponent<StoneController>().SpeedController(this.level);
            }

            //イカ出現
            if ((this.squidDelta >= this.squidSpan) && this.squidFlag)
            {
                this.squidDelta = 0;
                GameObject goSquid = Instantiate(this.squidPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goSquid.transform.position = new Vector3(3.0f, py, 0);
                goSquid.GetComponent<SquidController>().SpeedController(this.level);
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
                GameObject goFish = Instantiate(this.fishPrefab);
                int x = Random.Range(0, 2);
                float px = 4.0f + x;
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
                goFish.GetComponent<FishController>().SpeedController(this.level);
            }

            //タコ出現
            if (this.octopusDelta >= this.octopusSpan)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(this.octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
                goOctopus.GetComponent<OctopusController>().SpeedController(this.level);
            }

            //隕石出現
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(this.stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
                goStone.GetComponent<StoneController>().SpeedController(this.level);
            }

            //イカ出現
            if (this.squidDelta >= this.squidSpan)
            {
                this.squidDelta = 0;
                GameObject goSquid = Instantiate(this.squidPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goSquid.transform.position = new Vector3(3.0f, py, 0);
                goSquid.GetComponent<SquidController>().SpeedController(this.level);
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
                GameObject goFish = Instantiate(this.fishPrefab);
                int x = Random.Range(0, 2);
                float px = 4.0f + x;
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
                goFish.GetComponent<FishController>().SpeedController(this.level);
            }

            //タコ出現
            if (this.octopusDelta >= this.octopusSpan)
            {
                this.octopusDelta = 0;
                GameObject goOctopus = Instantiate(this.octopusPrefab);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goOctopus.transform.position = new Vector3(3.0f, py, 0);
                goOctopus.GetComponent<OctopusController>().SpeedController(this.level);
            }

            //隕石出現
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(this.stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
                goStone.GetComponent<StoneController>().SpeedController(this.level);
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
                GameObject goFish = Instantiate(this.fishPrefab);
                int x = Random.Range(0, 2);
                float px = 4.0f + x;
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
                goFish.GetComponent<FishController>().SpeedController(this.level);
            }

            //隕石出現
            if (this.stoneDelta >= this.stoneSpan)
            {
                this.stoneDelta = 0;
                GameObject goStone = Instantiate(this.stonePrefab);
                int x = Random.Range(0, 10);
                float px = x;
                goStone.transform.position = new Vector3(px, 6.0f, 0);
                goStone.GetComponent<StoneController>().SpeedController(this.level);
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
                GameObject goFish = Instantiate(this.fishPrefab);
                int x = Random.Range(-1, 2);
                float px = 3.5f + (x / 2);
                int y = Random.Range(-15, 16);
                float py = y / 3;
                goFish.transform.position = new Vector3(px, py, 0);
                goFish.GetComponent<FishController>().SpeedController(this.level);
            }
        }
    }
}
