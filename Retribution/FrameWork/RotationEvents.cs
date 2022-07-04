using RobotManager.Helpful;
using System;
using System.Collections.Generic;
using Wmanager.Helpers;
using Wmanager.ObjectManager;

namespace Retribution.FrameWork
{
    internal class RotationEvents
    {
        public static void Start()
        {
            EventLua.LuaEventWithArgs += CombatEvent;
        }

        public static void Stop()
        {
            EventLua.LuaEventWithArgs -= CombatEvent;
        }

        private static void CombatEvent(String EventName, List<String> Args)
        {
            var PlayerGuid = ToWoWGuid();
            if (EventName.ToString() == "COMBAT_LOG_EVENT_UNFILTERED")
            {
                if (Args[2] == PlayerGuid)
                {
                    for (int i = 0; i < Args.Count; i++)
                    {
                        Logging.Write($"Args[{i}] = {Args[i]}");
                    }
                    switch (Args[1])
                    {
                        case "SPELL_CAST_START":
                            //_unlockTime = DateTime.Now.AddMilliseconds(50);
                            break;
                        case "SPELL_CAST_SUCCESS":
                        case "SPELL_HEAL":
                        case "SPELL_DAMAGE":
                            _unlockTime = DateTime.Now.AddMilliseconds(Delay);
                            break;
                    }
                }
            }
        }

        private static string ToWoWGuid()
        {
            var wowGuid = ObjectManager.Me.GUID.ToString("x").ToUpper();
            var c = 16 - wowGuid.Length;
            for (var i = 0; i < c; i++)
            {
                wowGuid = "0" + wowGuid;
            }
            return "0x" + wowGuid;
        }

        private static int Delay = 50;
        private static DateTime _unlockTime = DateTime.Now;
        public static bool GcdActive => DateTime.Now < _unlockTime;
    }
}
