using RobotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmanager.Helpers;
using Wmanager.ObjectManager;

namespace Retribution.FrameWork
{
    public class SpellRotation
    {
        public SpellRotation(Spell spell, Func<Boolean> func)
        {
            this.SpellName = spell;
            this.Funct = func;
        }

        public Boolean Excute()
        {
            if (RotationEvents.GcdActive) return false;

            if(ObjectManager.Me.IsStunned)
                return false;


            if (Funct.Invoke())
            {
                if(SpellManager.GetSpellCooldown(this.SpellName.SpellName) <= 0)
                {
                    this.SpellName.Cast(true);
                    return true;
                }
            }
            return false;
        }

        public Func<Boolean> Funct { get; set; }
        public Spell SpellName { get; set; }
    }
}
