using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "MyGame/SkillData")]
public class SkillData : ScriptableObject
{
    public Sprite appearance;  // 見た目のSprite
    public AnimatorOverrideController overrideController; // 追加！
    // 他にも：エフェクト、スキル処理、説明文など追加可能
}
