using UnityEngine;

public class SkillFe : SkillBase
{
    public SkillFe(PlayerController pc)
    {
        pc.InitFe();
    }
    public override void Execute(PlayerController pc)
    {
        
    }

    public override void ResetSkill(PlayerController pc)
    {
        pc.ResetFe();
    }
}