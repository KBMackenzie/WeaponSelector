using System;

namespace WeaponSelector.Choices;

public static class ChoiceManager
{
    private static int weaponCount = Enum.GetNames(typeof(WeaponChoice)).Length;
    private static int traitCount  = Enum.GetNames(typeof(WeaponChoice)).Length;
    private static int curseCount  = Enum.GetNames(typeof(CurseChoice)).Length;

    public static WeaponChoice GetRandomWeapon()
        => (WeaponChoice)UnityEngine.Random.Range(1, weaponCount);

    public static WeaponTrait GetRandomTrait()
        => (WeaponTrait)UnityEngine.Random.Range(1, traitCount);

    public static CurseChoice GetRandomCurse()
        => (CurseChoice)UnityEngine.Random.Range(1, curseCount);
}
