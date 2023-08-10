using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムを一定の周期で生成する
public class ItemGenerator : MonoBehaviour
{
    public GameObject barrierPrefab; //バリアスターのPrefab
    float barrierSpan; //バリアスターの発生周期[s]
    float barrierDelta; //バリアスター用の現在時間[s]
    public GameObject heartPrefab; //ハートのPrefab
    float heartSpan; //ハートの発生周期[s]
    float heartDelta; //ハート用の現在時間[s]

    // Start is called before the first frame update
    void Start()
    {
        //変数の初期化
        this.barrierSpan = 50.0f;
        this.barrierDelta = 0;
        this.heartSpan = 25.0f;
        this.heartDelta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseDirector.getPaused())//ポーズ時は処理停止
        {
            return;
        }

        //フレームごとに経過時間を加算
        this.barrierDelta += Time.deltaTime;
        this.heartDelta += Time.deltaTime;

        //アイテム出現
        //アイテム毎の経過時間(delta)が出現スパン(span)に達すると出現
        //アイテムは決められた位置からランダムに出現
        if (this.barrierDelta >= this.barrierSpan)
        {
            //バリアスター出現
            this.barrierDelta = 0;
            GameObject goBarrier = Instantiate(barrierPrefab);
            int y = Random.Range(-18, 19);
            float py = y / 4;
            goBarrier.transform.position = new Vector3(3.0f, py, 0);
        }

        if (this.heartDelta >= this.heartSpan)
        {
            //ハート出現
            this.heartDelta = 0;
            GameObject goHeart = Instantiate(heartPrefab);
            int y = Random.Range(-18, 19);
            float py = y / 4;
            goHeart.transform.position = new Vector3(3.0f, py, 0);
            this.heartSpan = 50.0f;
        }
    }
}
