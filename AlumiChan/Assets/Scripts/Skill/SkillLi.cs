public class SkillLi : SkillBase
{
    public override void Execute(PlayerController pc)
    {
        pc.SetJumpPower();
    }

    public override void ResetSkill(PlayerController pc)
    {
        pc.ResetPower();
    }
}