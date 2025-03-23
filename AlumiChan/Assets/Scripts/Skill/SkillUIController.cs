using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Sequence = DG.Tweening.Sequence;

public class SkillUIController : MonoBehaviour
{
    public Transform player;
    
    [Header("Effect")]
    private PlayerAppearanceChanger appearanceChanger;
    [SerializeField] private SkillData leftSkill;
    [SerializeField] private SkillData rightSkill;
    [SerializeField] private SkillData upSkill;
    [SerializeField] private SkillData defaultSkill;

    
    public Transform uiRoot; // SkillUIの親
    public Transform skillLeft, skillRight, skillUp;

    private Vector3 leftOffset = new Vector3(-1.5f, 0, 0);
    private Vector3 rightOffset = new Vector3(1.5f, 0, 0);
    private Vector3 upOffset = new Vector3(0, 1.5f, 0);

    private bool isShown = false;

    public SkillBubble skillLeftBubble;
    public SkillBubble skillUpBubble;
    public SkillBubble skillRightBubble;

    [SerializeField]
    private bool haveAu;
    [SerializeField]
    private bool haveFe;
    [SerializeField]
    private bool haveLi;

    //スキル発動している
    private bool inAu;
    private bool inFe;
    private bool inLi;

    private bool inAuChanging;
    private bool inFeChanging;
    private bool inLiChanging;

    public SpriteRenderer auSprite;
    public SpriteRenderer feSprite;
    public SpriteRenderer liSprite;
    
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
            if (Input.GetKeyDown(KeyCode.A) && !inFeChanging && !inLiChanging &&  !inAuChanging)
            {
                if (!haveAu)
                {
                    FlashRed(skillLeftBubble.GetComponent<SpriteRenderer>());
                    return;
                }
                inAuChanging = true;
                skillLeftBubble.ResetVisual();
                skillUpBubble.ResetVisual(); // 毎回リセットしておく
                skillRightBubble.ResetVisual();
                skillLeftBubble.PlaySelectAnimation(() =>
                {
                    // アニメ後にスキル発動
                    ActivateLeftSkill();
                    HideUI();
                    inAuChanging = false;
                });
            }
        
            if (Input.GetKeyDown(KeyCode.W) &&  !inFeChanging && !inLiChanging &&  !inAuChanging)
            {
                if (!haveFe)
                {
                    FlashRed(skillUpBubble.GetComponent<SpriteRenderer>());
                    return;
                }
                inFeChanging = true;
                skillLeftBubble.ResetVisual();
                skillUpBubble.ResetVisual(); // 毎回リセットしておく
                skillRightBubble.ResetVisual();
                skillUpBubble.PlaySelectAnimation(() =>
                {
                    // アニメ後にスキル発動
                    ActivateUpSkill();
                    HideUI();
                    inFeChanging = false;
                });
            }
        
            if (Input.GetKeyDown(KeyCode.D) && !inFeChanging && !inLiChanging &&  !inAuChanging)
            {
                if (!haveLi)
                {
                    FlashRed(skillRightBubble.GetComponent<SpriteRenderer>());
                    return;
                }
                inLiChanging = true;
                skillLeftBubble.ResetVisual();
                skillUpBubble.ResetVisual(); // 毎回リセットしておく
                skillRightBubble.ResetVisual();
                skillRightBubble.PlaySelectAnimation(() =>
                {
                    // アニメ後にスキル発動
                    ActivateRightSkill();
                    HideUI();
                    inLiChanging = false;
                });
            }
        }
    }
    
    public void FlashRed(SpriteRenderer sprite)
    {
        Color originalColor = sprite.color;

        Sequence flashSeq = DOTween.Sequence();
        flashSeq.Append(sprite.DOColor(Color.red, 0.1f));
        flashSeq.Append(sprite.DOColor(originalColor, 0.1f));
        flashSeq.SetLoops(2); // 2回点滅
    }
    
    void LateUpdate()
    {
        // プレイヤーの位置に追従（高さ調整したいならY軸を+0.5fとか）
        uiRoot.position = player.position;
    }

    void ShowUI()
    {
        isShown = true;
        
        skillLeft.localPosition = Vector3.zero;
        skillLeft.localScale = Vector3.zero;
        skillLeft.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        skillLeft.DOLocalMove(leftOffset, 0.3f).SetEase(Ease.OutBack);
        skillLeft.gameObject.SetActive(true);
        
        skillUp.localPosition = Vector3.zero;
        skillUp.localScale = Vector3.zero;
        skillUp.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        skillUp.DOLocalMove(upOffset, 0.3f).SetEase(Ease.OutBack);
        skillUp.gameObject.SetActive(true);

        skillRight.localPosition = Vector3.zero;
        skillRight.localScale = Vector3.zero;
        skillRight.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        skillRight.DOLocalMove(rightOffset, 0.3f).SetEase(Ease.OutBack);
        skillRight.gameObject.SetActive(true);
        
        if (!haveAu || inAu)
        {
            auSprite.enabled = false;
        }
        else
        {
            auSprite.enabled = true;   
        }

        if (!haveFe || inFe)
        {
            feSprite.enabled = false;
        }
        else
        {
            feSprite.enabled = true;
        }

        if (!haveLi || inLi)
        {
            liSprite.enabled = false;
        }
        else
        {
            liSprite.enabled = true;
        }
        
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
        var pc = player.GetComponent<PlayerController>();
        if (inAu)
        {
            appearanceChanger.ChangeAppearance(defaultSkill);
            pc.ResetSkill();
            inAu = false;
        }
        else
        {
            inFe = false;
            inLi = false;
            inAu = true;
            appearanceChanger.ChangeAppearance(leftSkill);
            pc.SetCurrentSkill(new SkillAu(pc));
        }
        
        // 実際のスキル処理を書く
    }
    
    void ActivateUpSkill()
    {
        Debug.Log("上のスキルが発動されました！");
        var pc = player.GetComponent<PlayerController>();
        if (inFe)
        {
            appearanceChanger.ChangeAppearance(defaultSkill);
            pc.ResetSkill();
            inFe = false;
        }
        else
        {
            inLi = false;
            inAu = false;
            inFe = true;
            appearanceChanger.ChangeAppearance(upSkill);
            pc.SetCurrentSkill(new SkillFe(pc));
        }
    }
    
    void ActivateRightSkill()
    {
        Debug.Log("右のスキルが発動されました！");
        var pc = player.GetComponent<PlayerController>();
        if (inLi)
        {
            appearanceChanger.ChangeAppearance(defaultSkill);
            pc.ResetSkill();
            inLi = false;  
        }
        else
        {
            inFe = false;
            inAu = false;
            inLi = true;
            appearanceChanger.ChangeAppearance(rightSkill);
            pc.SetCurrentSkill(new SkillLi());
        }
    }

    public void SetSkillHave(string skillName)
    {
        switch (skillName)
        {
            case "Au":
                haveAu = true;
                auSprite.enabled = true;
                break;
            case "Fe":
                haveFe = true;  
                feSprite.enabled = true;
                break;
            case "Li":  
                haveLi = true;
                liSprite.enabled = true;
                break;
            default:
                Debug.LogWarning("識別できないスキル");
                break;
        }
    }
}
