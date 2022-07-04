using Retribution.FrameWork;
using RobotManager.CustomClass;
using RobotManager.Helpful;
using System.Collections.Generic;
using Wmanager.Helpers;
using Wmanager.ObjectManager;

public class Main : ICustomClass
{
    public float Range => 0;


    public void Initialize()
    {
        Wmanager.RavenSettings.CurrentSettings.EventFreshRefreshMs = 50;
        RotationEvents.Start();
        Rotation();
    }

    public void Dispose()
    {
        RotationEvents.Stop();
        IsStarrted = false;
    }


    void Rotation()
    {
        this.IsStarrted = true;
        while(this.IsStarrted)
        {
            if (ObjectManager.Me.InCombat)
            {
                if(ObjectManager.Target.IsVaild)
                {
                    this.Excute();
                }
            }
        }
    }

    public void Settings()
    {
        Logging.Write("No Settings");
    }

    void Excute()
    {
        foreach (var spell in SpellRotation)
        {
            if(spell.Excute())
            {
                break;
            }
        }
    }

    private bool IsStarrted = false;
    private List<SpellRotation> SpellRotation = new List<SpellRotation>()
    {
        new SpellRotation(new Spell("Flash of Light"), ()=> ObjectManager.Me.HaveBuff("The Art of War") && ObjectManager.Me.HealthPercentage < 20),
        new SpellRotation(new Spell("Judgement of Wisdom"), () => SpellManager.IsUseable("Judgement of Wisdom")),
        new SpellRotation(new Spell("Divine Storm"), ()=> SpellManager.IsUseable("Divine Storm")),
        new SpellRotation(new Spell("Crusader Strike"), ()=> SpellManager.IsUseable("Crusader Strike")),
        new SpellRotation(new Spell("Hammer of Wrath"), ()=> ObjectManager.Target.HealthPercentage < 20),
        new SpellRotation(new Spell("Consecration"), ()=> ObjectManager.Me.MaximumHealth >= 20),
        new SpellRotation(new Spell("Exorcism"), ()=> ObjectManager.Me.HaveBuff("The Art of War")),
    };
}
