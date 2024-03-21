using HarmonyLib;
using WeaponSelector.Choices;
using WeaponSelector.Equipment;

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
            WeaponTrait trait = Trait != WeaponTrait.Random
                ? Trait
                : ChoiceManager.GetRandomTrait();

            __result = ToEquipment.FromWeapon(Weapon)[(int)trait];
            return;
        }

        if (Trait != WeaponTrait.Random)  // Random weapon, chosen trait
        {
            WeaponChoice weapon = ChoiceManager.GetRandomWeapon();
            __result = ToEquipment.FromWeapon(weapon)[(int)Trait];
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
