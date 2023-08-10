using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�^�C�g����ʂ̃Q�[�������\���{�^��
//�������ƃQ�[��������\���A���̂��Ɖ�ʂ��^�b�v�Ŗ߂�
public class InfoDirector : MonoBehaviour, IPointerClickHandler
{
    //��ʕ\������Ă���UI��{�^���̕\����\����؂�ւ��邽�߂̕ϐ�
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject title2;
    [SerializeField] private GameObject highScoreTitle;
    [SerializeField] private GameObject highScore1;
    [SerializeField] private GameObject highScore2;
    [SerializeField] private GameObject highScore3;
    [SerializeField] private GameObject barrierImage;
    [SerializeField] private GameObject heartImage;
    SpriteRenderer sp;
    SpriteRenderer sp2;
    bool tapped; //�w����ʂɐG��Ă��邩�ǂ���
    bool infoON; //������ʂ��ǂ���

    // Start is called before the first frame update
    void Start()
    {
        //�ϐ��̏�����
        this.sp = GetComponent<SpriteRenderer>();
        this.sp2 = startButton.GetComponent<SpriteRenderer>();
        infoPanel.SetActive(false);
        infoON = false;
        tapped = false;
        barrierImage.SetActive(false);
        heartImage.SetActive(false);
    }

    //������ʂŃ^�b�v�����w����ʂ��痣�ꂽ�Ƃ��Ƀ^�C�g����
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && (infoON))
        {
            tapped = true;
        }
        else if ((tapped == true) && (Input.GetMouseButtonUp(0)))
        {
            infoPanel.SetActive(false);
            infoON = false;
            tapped = false;
            sp.enabled = true;
            sp2.enabled = true;
            title.SetActive(true);
            title2.SetActive(true);
            highScoreTitle.SetActive(true);
            highScore1.SetActive(true);
            highScore2.SetActive(true);
            highScore3.SetActive(true);
            barrierImage.SetActive(false);
            heartImage.SetActive(false);
        }
    }

    //������ʂɈڍs
    public void OnPointerClick(PointerEventData eventData)
    {
        infoPanel.SetActive(true);
        infoON = true;
        sp.enabled = false;
        sp2.enabled = false;
        title.SetActive(false);
        title2.SetActive(false);
        highScoreTitle.SetActive(false);
        highScore1.SetActive(false);
        highScore2.SetActive(false);
        highScore3.SetActive(false);
        barrierImage.SetActive(true);
        heartImage.SetActive(true);
    }
}
