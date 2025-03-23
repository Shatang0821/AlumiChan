using System;
using DG.Tweening;
using UnityEngine;

public class SkillUIController : MonoBehaviour
{
    public Transform player;
    
    [Header("Effect")]
    private PlayerAppearanceChanger appearanceChanger;
    [SerializeField] private SkillData leftSkill;
    [SerializeField] private SkillData rightSkill;
    [SerializeField] private SkillData upSkill;

    
    public Transform uiRoot; // SkillUIの親
    public Transform skillLeft, skillRight, skillUp;

    private Vector3 leftOffset = new Vector3(-1.5f, 0, 0);
    private Vector3 rightOffset = new Vector3(1.5f, 0, 0);
    private Vector3 upOffset = new Vector3(0, 1.5f, 0);

    private bool isShown = false;

    public SkillBubble skillLeftBubble;
    public SkillBubble skillUpBubble;
    public SkillBubble skillRightBubble;

    private void Start()
    {
        appearanceChanger = player.GetComponent<PlayerAppearanceChanger>();
        
        skillLeftBubble.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isShown) ShowUI();
            else HideUI();
        }

        if (isShown)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                skillLeftBubble.ResetVisual();
                skillUpBubble.ResetVisual(); // 毎回リセットしておく
                skillRightBubble.ResetVisual();
                skillLeftBubble.PlaySelectAnimation(() =>
                {
                    // アニメ後にスキル発動
                    ActivateLeftSkill();
                    HideUI();
                });
            }
        
            if (Input.GetKeyDown(KeyCode.W))
            {
                skillLeftBubble.ResetVisual();
                skillUpBubble.ResetVisual(); // 毎回リセットしておく
                skillRightBubble.ResetVisual();
                skillUpBubble.PlaySelectAnimation(() =>
                {
                    // アニメ後にスキル発動
                    ActivateUpSkill();
                    HideUI();
                });
            }
        
            if (Input.GetKeyDown(KeyCode.D))
            {
                skillLeftBubble.ResetVisual();
                skillUpBubble.ResetVisual(); // 毎回リセットしておく
                skillRightBubble.ResetVisual();
                skillRightBubble.PlaySelectAnimation(() =>
                {
                    // アニメ後にスキル発動
                    ActivateRightSkill();
                    HideUI();
                });
            }
        }
    }
    
    void LateUpdate()
    {
        // プレイヤーの位置に追従（高さ調整したいならY軸を+0.5fとか）
        uiRoot.position = player.position;
    }

    void ShowUI()
    {
        isShown = true;

        // 全部最初は体内にいる
        skillLeft.localPosition = Vector3.zero;
        skillRight.localPosition = Vector3.zero;
        skillUp.localPosition = Vector3.zero;

        skillLeft.localScale = Vector3.zero;
        skillRight.localScale = Vector3.zero;
        skillUp.localScale = Vector3.zero;
        
        skillLeft.gameObject.SetActive(true);
        skillRight.gameObject.SetActive(true);
        skillUp.gameObject.SetActive(true);

        // DoTweenアニメーション（泡のように飛び出す）
        skillLeft.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        skillLeft.DOLocalMove(leftOffset, 0.3f).SetEase(Ease.OutBack);

        skillRight.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        skillRight.DOLocalMove(rightOffset, 0.3f).SetEase(Ease.OutBack);

        skillUp.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        skillUp.DOLocalMove(upOffset, 0.3f).SetEase(Ease.OutBack);
    }

    void HideUI()
    {
        isShown = false;

        // 回転しながらスケールダウンして戻る
        skillLeft.DOLocalRotate(new Vector3(0, 0, 360), 0.3f, RotateMode.FastBeyond360);
        skillLeft.DOScale(0f, 0.3f);
        skillLeft.DOLocalMove(Vector3.zero, 0.3f).OnComplete(() =>
        {
            skillLeft.gameObject.SetActive(false);
        });

        skillRight.DOLocalRotate(new Vector3(0, 0, 360), 0.3f, RotateMode.FastBeyond360);
        skillRight.DOScale(0f, 0.3f);
        skillRight.DOLocalMove(Vector3.zero, 0.3f).OnComplete(() =>
        {
            skillRight.gameObject.SetActive(false);
        });

        skillUp.DOLocalRotate(new Vector3(0, 0, 360), 0.3f, RotateMode.FastBeyond360);
        skillUp.DOScale(0f, 0.3f);
        skillUp.DOLocalMove(Vector3.zero, 0.3f).OnComplete(() =>
        {
            skillUp.gameObject.SetActive(false);
        });
    }
    
    void ActivateLeftSkill()
    {
        Debug.Log("左のスキルが発動されました！");
        appearanceChanger.ChangeAppearance(leftSkill);
        var pc = player.GetComponent<PlayerController>();
        pc.SetCurrentSkill(new SkillAu(pc));
        // 実際のスキル処理を書く
    }
    
    void ActivateUpSkill()
    {

        Debug.Log("上のスキルが発動されました！");
        appearanceChanger.ChangeAppearance(upSkill);
        var pc = player.GetComponent<PlayerController>();
        pc.SetCurrentSkill(new SkillFe(pc));
        // 実際のスキル処理を書く
    }
    
    void ActivateRightSkill()
    {
        Debug.Log("右のスキルが発動されました！");
        appearanceChanger.ChangeAppearance(rightSkill);
        var pc = player.GetComponent<PlayerController>();
        pc.SetCurrentSkill(new SkillLi());
        // 実際のスキル処理を書く
    }
}
