using System;

namespace WeaponSelector.Choices;

public static class ChoiceManager
{
    public static readonly int WeaponChoiceCount = LengthOf<WeaponChoice>();
    public static readonly int TraitChoiceCount  = LengthOf<WeaponTrait>();
    public static readonly int CurseChoiceCount  = LengthOf<CurseChoice>();

    public static WeaponChoice GetRandomWeapon()
        => (WeaponChoice)UnityEngine.Random.Range(1, WeaponChoiceCount);

    public static WeaponTrait GetRandomTrait()
        => (WeaponTrait)UnityEngine.Random.Range(1, TraitChoiceCount);

    public static CurseChoice GetRandomCurse()
        => (CurseChoice)UnityEngine.Random.Range(1, CurseChoiceCount);

    private static int LengthOf<T>() where T : Enum
        => Enum.GetNames(typeof(T)).Length;
}
