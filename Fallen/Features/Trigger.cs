﻿#region

using Fallen.API;
using hazedumper;
using System.Threading;

#endregion

namespace Fallen.Features
{
    internal class Trigger
    {
        internal void Run()
        {
            while (true)
            {
                if (Settings.Trigger.Enabled)
                {
                    var entityInCrossId = Memory.ProcessMemory.ReadMemory<int>(LocalPlayer.Base + netvars.m_iCrosshairId);
                    if (entityInCrossId != 0)
                    {
                        var entityBase = Memory.ProcessMemory.ReadMemory<int>(MainClass.ClientPointer + signatures.dwEntityList + (entityInCrossId - 1) * 0x10);
                        var entityTeam = Memory.ProcessMemory.ReadMemory<int>(entityBase + netvars.m_iTeamNum);
                        if (!Settings.Trigger.Key)
                            Thread.Sleep(1);

                        if (Settings.Trigger.Key && entityTeam != LocalPlayer.Team)
                        {
                            Memory.ProcessMemory.WriteMemory<int>(MainClass.ClientPointer + signatures.dwForceAttack, 5);
                            Thread.Sleep(2);
                            Memory.ProcessMemory.WriteMemory<int>(MainClass.ClientPointer + signatures.dwForceAttack, 4);
                            Thread.Sleep(4);
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}