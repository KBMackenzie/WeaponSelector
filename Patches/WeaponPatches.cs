using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using Lamb;
using System.Collections;
using UnityEngine;

namespace WeaponSelector
{
    [HarmonyPatch]
    internal static class WeaponPatches
    {
        public static WeaponChoices Weapon;
        public static WeaponTraits Trait;
        public static CurseChoices Curse;

        static bool skipWeapon => Weapon == WeaponChoices.Random && Trait == WeaponTraits.Random;
        static bool skipCurses => Curse == CurseChoices.Random;


        [HarmonyPatch(typeof(DataManager), nameof(DataManager.GetRandomWeaponInPool))]
        [HarmonyPostfix]
        static void GetWeaponPostfix(DataManager __instance, ref EquipmentType __result)
        {
            if (skipWeapon) return;

            // Library!
            Library library = new Library();

            if (Weapon != WeaponChoices.Random) // Not random weapon
            {
                if (Trait != WeaponTraits.Random) // Chosen trait
                {
                    __result = library.Weapons[Weapon][(int)Trait];
                }
                else // Random trait
                {
                    // The "-1" is so WeaponTraits.Random isn't rolled
                    int random = UnityEngine.Random.Range(0, Trait.EnumLength() - 1);
                    __result = library.Weapons[Weapon][random];
                }
            }
            else if (Trait != WeaponTraits.Random) // Random weapon, chosen trait
            {
                // The "-1" is so WeaponChoices.Random isn't rolled
                int random = UnityEngine.Random.Range(0, Weapon.EnumLength() - 1);
                __result = library.Weapons[(WeaponChoices)random][(int)Trait];
            }
        }

        [HarmonyPatch(typeof(DataManager), nameof(DataManager.GetRandomCurseInPool))]
        [HarmonyPostfix]
        static void GetCursePostFix(DataManager __instance, ref EquipmentType __result)
        {
            if (skipCurses) return;

            // Library!
            Library library = new Library();

            FileLog.Log("Curse: " + library.CurseNames[Curse].ToString());

            __instance.AddCurse(library.Curses[Curse]);

            __result = library.Curses[Curse];
        }
    }
}
