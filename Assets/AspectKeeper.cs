using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A�X�y�N�g�䒲���X�N���v�g
[ExecuteAlways]
public class AspectKeeper : MonoBehaviour
{
    //�Q�[����ʕ`��J����(���C���J����)
    [SerializeField] private Camera targetCamera;
    //�ړI(�Q�[����ʂ��`�悳��镔��)�̃A�X�y�N�g��x�N�g��(x,y)
    [SerializeField] Vector2 aspectVec;
    //���ۂ̉�ʂ̃A�X�y�N�g��(x/y)
    float screenAspect;
    //�ړI�̃A�X�y�N�g��(x:y)
    float targetAspect;
    //�ړI�̃A�X�y�N�g��ɂ���{��(�ړI/����)
    float magRate;
    //���ۂ̉�ʂɑ΂���Q�[����ʂ̕`��͈�
    Rect viewportRect;

    // Update is called once per frame
    void Update()
    {
        //���ۂ̉�ʂ̃A�X�y�N�g��(x/y)
        this.screenAspect = Screen.width / (float)Screen.height;
        //�ړI�̃A�X�y�N�g��(x/y)
        this.targetAspect = aspectVec.x / aspectVec.y;
        //���ۂ̃A�X�y�N�g��ɂ���ړI�̃A�X�y�N�g��̔{��(�ړI/����)
        this.magRate = targetAspect / screenAspect;
        //���ۂ̉�ʂɑ΂���Q�[����ʂ̕`��͈�
        this.viewportRect = new Rect(0, 0, 1, 1);

        if (this.magRate < 1)
        {
            //���ۂ̉�ʂ��ړI��艡���̏ꍇ
            viewportRect.width = magRate;
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;
        }
        else
        {
            //���ۂ̉�ʂ��ړI���c���̏ꍇ
            viewportRect.height = 1 / magRate;
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;
        }

        targetCamera.rect = viewportRect;
    }
}
