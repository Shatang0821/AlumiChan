public class SkillAu : SkillBase
{
    public SkillAu(PlayerController pc)
    {
        pc.InitElec();
    }

    public override void Execute(PlayerController pc)
    {
        pc.electricityball();
    }

    public override void ResetSkill(PlayerController pc)
    {
        pc.RestElec();
    }
}