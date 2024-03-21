using HarmonyLib;
using WeaponSelector.Enums;
using WeaponSelector.Choices;

namespace WeaponSelector.Patches;

[HarmonyPatch]
internal static class WeaponPatches
{
    public static WeaponChoice Weapon;
    public static WeaponTrait Trait;
    public static CurseChoice Curse;

    private static bool ShouldSkipWeapon()
        => Weapon == WeaponChoice.Random && Trait == WeaponTrait.Random;

    private static bool ShouldSkipCurses()
        => Curse == CurseChoice.Random;

    [HarmonyPatch(typeof(DataManager), nameof(DataManager.GetRandomWeaponInPool))]
    [HarmonyPostfix]
    private static void GetWeaponPostfix(DataManager __instance, ref EquipmentType __result)
    {
        if (ShouldSkipWeapon()) return;

        if (Weapon != WeaponChoice.Random) // Chosen weapon, chosen trait.
        {
            int trait = Trait != WeaponTrait.Random
                ? (int)Trait
                : UnityEngine.Random.Range(0, Trait.EnumLength() - 1);

            __result = ToEquipment.FromWeapon(Weapon)[(int)Trait];
            return;
        }

        if (Trait != WeaponTrait.Random)  // Random weapon, chosen trait
        {
            // The "-1" is so WeaponChoices.Random isn't rolled
            int random = UnityEngine.Random.Range(0, Weapon.EnumLength() - 1);
            __result = ToEquipment.FromWeapon((WeaponChoice)random)[(int)Trait];
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
