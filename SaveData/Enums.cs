using System;

namespace WeaponSelector;

public enum WeaponChoices
{
    Sword,
    Axe,
    Hammer,
    Dagger,
    Gauntlet,
    Random,     // Random is always last.
}

public enum WeaponTraits
{
    Normal,
    Poison,
    Critical,
    Healing,
    Fervour,
    Godly,
    Necromancy, // I refuse to spell it "Nercomancy" like the game does LOL
    Random
}

public enum CurseChoices
{
    TouchofTurua,
    Maelstrom,
    TouchofIthaqua,
    TouchoftheRevenant,
    DivineBlast,
    DivineGuardian,
    DivineBlizzard,
    DivineBlight,
    IchorThrown,
    PointofCorruption,
    PathoftheRighteous,
    CalloftheCrown,
    FlamingShot,
    HoundsofFate,
    CleansingFire,
    StrikeoftheCrown,
    DeathsSweep,
    DeathsAttendant,
    DeathsSquall,
    OathoftheCrown,
    Random,
}

public enum Change
{
    Weapon,
    Trait,
    Curse,
}

public static class EnumHelpers
{
    public static (WeaponChoices, WeaponTraits, CurseChoices) GetDefault()
    {
        return (WeaponChoices.Random, WeaponTraits.Random, CurseChoices.Random);
    }

    public static int DefaultFromInt(Change type)
    {
        var data = GetDefault();

        switch (type)
        {
            case Change.Weapon:
                return (int)data.Item1;
            case Change.Trait:
                return (int)data.Item2;
            case Change.Curse:
                return (int)data.Item3;
        }

        return 0;
    }

    // Extension methods
    public static int EnumLength(this WeaponChoices a)
    {
        return Enum.GetNames(typeof(WeaponChoices)).Length;
    }
    public static int EnumLength(this WeaponTraits a)
    {
        return Enum.GetNames(typeof(WeaponTraits)).Length;
    }
    public static int EnumLength(this CurseChoices a)
    {
        return Enum.GetNames(typeof(CurseChoices)).Length;
    }
}
