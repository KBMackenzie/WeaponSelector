using HarmonyLib;

namespace WeaponSelector;

[HarmonyPatch]
internal static class WeaponPatches
{
    public static WeaponChoices Weapon;
    public static WeaponTraits Trait;
    public static CurseChoices Curse;

    private static bool ShouldSkipWeapon()
        => Weapon == WeaponChoices.Random && Trait == WeaponTraits.Random;

    private static bool ShouldSkipCurses()
        => Curse == CurseChoices.Random;

    [HarmonyPatch(typeof(DataManager), nameof(DataManager.GetRandomWeaponInPool))]
    [HarmonyPostfix]
    private static void GetWeaponPostfix(DataManager __instance, ref EquipmentType __result)
    {
        if (ShouldSkipWeapon()) return;

        if (Weapon != WeaponChoices.Random) // Chosen weapon, chosen trait.
        {
            int trait = Trait != WeaponTraits.Random
                ? (int)Trait
                : UnityEngine.Random.Range(0, Trait.EnumLength() - 1);

            __result = ToEquipment.FromWeapon(Weapon)[(int)Trait];
            return;
        }

        if (Trait != WeaponTraits.Random)  // Random weapon, chosen trait
        {
            // The "-1" is so WeaponChoices.Random isn't rolled
            int random = UnityEngine.Random.Range(0, Weapon.EnumLength() - 1);
            __result = ToEquipment.FromWeapon((WeaponChoices)random)[(int)Trait];
        }
    }

    [HarmonyPatch(typeof(DataManager), nameof(DataManager.GetRandomCurseInPool))]
    [HarmonyPostfix]
    private static void GetCursePostFix(DataManager __instance, ref EquipmentType __result)
    {
        if (ShouldSkipCurses()) return;
        __result = ToEquipment.FromCurse(Curse);
    }
}
