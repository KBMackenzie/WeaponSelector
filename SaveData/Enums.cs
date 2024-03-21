using System;

namespace WeaponSelector;

public enum WeaponChoice
{
    Sword,
    Axe,
    Hammer,
    Dagger,
    Gauntlet,
    Random,     // Random is always last.
}

public enum WeaponTrait
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

public enum CurseChoice
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
    public static (WeaponChoice, WeaponTrait, CurseChoice) GetDefault()
    {
        return (WeaponChoice.Random, WeaponTrait.Random, CurseChoice.Random);
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
    public static int EnumLength(this WeaponChoice a)
    {
        return Enum.GetNames(typeof(WeaponChoice)).Length;
    }
    public static int EnumLength(this WeaponTrait a)
    {
        return Enum.GetNames(typeof(WeaponTrait)).Length;
    }
    public static int EnumLength(this CurseChoice a)
    {
        return Enum.GetNames(typeof(CurseChoice)).Length;
    }
}
