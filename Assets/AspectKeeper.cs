using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アスペクト比調整スクリプト
[ExecuteAlways]
public class AspectKeeper : MonoBehaviour
{
    //ゲーム画面描画カメラ(メインカメラ)
    [SerializeField] private Camera targetCamera;
    //目的(ゲーム画面が描画される部分)のアスペクト比ベクトル(x,y)
    [SerializeField] Vector2 aspectVec;
    //実際の画面のアスペクト比(x/y)
    float screenAspect;
    //目的のアスペクト比(x:y)
    float targetAspect;
    //目的のアスペクト比にする倍率(目的/実際)
    float magRate;
    //実際の画面に対するゲーム画面の描画範囲
    Rect viewportRect;

    // Update is called once per frame
    void Update()
    {
        //実際の画面のアスペクト比(x/y)
        this.screenAspect = Screen.width / (float)Screen.height;
        //目的のアスペクト比(x/y)
        this.targetAspect = aspectVec.x / aspectVec.y;
        //実際のアスペクト比にする目的のアスペクト比の倍率(目的/実際)
        this.magRate = targetAspect / screenAspect;
        //実際の画面に対するゲーム画面の描画範囲
        this.viewportRect = new Rect(0, 0, 1, 1);

        if (this.magRate < 1)
        {
            //実際の画面が目的より横長の場合
            viewportRect.width = magRate;
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;
        }
        else
        {
            //実際の画面が目的より縦長の場合
            viewportRect.height = 1 / magRate;
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;
        }

        targetCamera.rect = viewportRect;
    }
}
