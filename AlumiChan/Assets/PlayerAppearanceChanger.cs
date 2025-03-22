using UnityEngine;
using DG.Tweening;

public class PlayerAppearanceChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteCurrent;
    [SerializeField] private SpriteRenderer spriteNext;
    [SerializeField] private float duration = 1.0f;
    private Animator currentAnimator;
    private Animator nextAnimator;
    private Material currentMat;

    private void Awake()
    {
        currentMat = spriteCurrent.material;
        currentAnimator = spriteCurrent.GetComponent<Animator>();
        nextAnimator = spriteNext.GetComponent<Animator>();
        ResetCutoff(); // 念のため初期化
    }

    public void ChangeAppearance(SkillData newSkill)
    {
        // 1. Nextに新しいSprite設定して表示ON
        spriteNext.sprite = newSkill.appearance;
        spriteNext.gameObject.SetActive(true);
        nextAnimator.runtimeAnimatorController = newSkill.overrideController;
        // 2. Currentを上から消していく（_CutoffY: 1 → 0）
        currentMat.SetFloat("_CutoffY", 1f);
        DOTween.To(() => currentMat.GetFloat("_CutoffY"),
                x => currentMat.SetFloat("_CutoffY", x),
            .5f, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // 3. 完了後：NextをCurrentに移す
                spriteCurrent.sprite = spriteNext.sprite;

                // 4. Reset（CutoffY & Next非表示）
                ResetCutoff();
                spriteNext.gameObject.SetActive(false);
                
                currentAnimator.runtimeAnimatorController = newSkill.overrideController;
                
                Debug.Log("変身完了！");
            });
    }

    private void ResetCutoff()
    {
        currentMat.SetFloat("_CutoffY", 1f);
    }
}