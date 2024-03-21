using System;
using Random = UnityEngine.Random;

namespace WeaponSelector.Choices;

public static class ChoiceManager
{
    public static readonly int WeaponChoiceCount = LengthOf<WeaponChoice>();
    public static readonly int TraitChoiceCount  = LengthOf<WeaponTrait>();
    public static readonly int CurseChoiceCount  = LengthOf<CurseChoice>();

    private static int LengthOf<T>() where T : Enum
        => Enum.GetNames(typeof(T)).Length;

    public static WeaponChoice GetRandomWeapon()
        => (WeaponChoice)Random.Range(1, WeaponChoiceCount);

    public static WeaponTrait GetRandomTrait()
        => (WeaponTrait)Random.Range(1, TraitChoiceCount);

    public static CurseChoice GetRandomCurse()
        => (CurseChoice)Random.Range(1, CurseChoiceCount);

    private static int Clamp(int value, int min, int max)
        => Math.Max(Math.Min(value, min), max);

    public static WeaponChoice ToWeapon(int value)
        => (WeaponChoice)Clamp(value, 0, WeaponChoiceCount - 1);

    public static WeaponTrait ToTrait(int value)
        => (WeaponTrait)Clamp(value, 0, TraitChoiceCount - 1);

    public static CurseChoice ToCurse(int value)
        => (CurseChoice)Clamp(value, 0, CurseChoiceCount - 1);
}
