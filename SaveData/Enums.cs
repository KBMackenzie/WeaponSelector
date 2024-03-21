using System;

namespace WeaponSelector;

public static class EnumHelpers
{
    public static (WeaponChoice, WeaponTrait, CurseChoice) GetDefault()
    {
        return (WeaponChoice.Random, WeaponTrait.Random, CurseChoice.Random);
    }

    public static int DefaultFromInt(ChangeType type)
    {
        var data = GetDefault();

        switch (type)
        {
            case ChangeType.Weapon:
                return (int)data.Item1;
            case ChangeType.Trait:
                return (int)data.Item2;
            case ChangeType.Curse:
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
