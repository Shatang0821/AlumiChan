using UnityEngine;
using DG.Tweening;

public class SkillBubble : MonoBehaviour
{
    public SpriteRenderer contentRenderer;    // 中の金属など

    public float animTime = 0.5f;

    public void PlaySelectAnimation(System.Action onComplete)
    {
        // 中身を拡大しながら透明に
        contentRenderer.transform.DOScale(1.5f, animTime).SetEase(Ease.OutBack);
        contentRenderer.DOFade(0f, animTime).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // アニメーション終了後にスキル発動処理をコールバックで呼ぶ
                onComplete?.Invoke();
            });
    }

    public void ResetVisual()
    {
        contentRenderer.color = new Color(1f, 1f, 1f, 1f);
        contentRenderer.transform.localScale = Vector3.one;
    }
}