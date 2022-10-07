﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WeaponSelector
{
    // Random is always the last one. Keep that in mind!
    internal enum WeaponChoices
    {
        Sword,
        Axe,
        Hammer,
        Dagger,
		Gauntlet,
        Random,
    }

	internal enum WeaponTraits
	{
		Normal,
		Poison,
		Critical,
		Healing,
		Fervour,
		Godly,
		Necromancy, // I refuse to spell it like "Nercomancy" like the game does LOL
        Random
	}

	internal enum CurseChoices
	{
        TouchofTurua,
        SailorsGrave,
        PassageoftheMariner,
        Maelstrom,
        TouchofIthaqua,
        TouchoftheRevenant,
        DivineBlast,
        DivineGuardian,
        DivineBlizzard,
        DivineBlight,
        IchorThrown,
        PointofCorruption,
        IchorConsuming,
        PathoftheRighteous,
        CalloftheCrown,
        FlamingShot,
        HoundsofFate,
        TrailBlazed,
        CleansingFire,
        StrikeoftheCrown,
        DeathsSweep,
        DeathsDisregard,
        DeathsAttendant,
        DeathsSquall,
        OathoftheCrown,
        Random,
    }

    internal enum Change
    {
        Weapon,
        Trait,
        Curse,
    }

    internal static class EnumHelpers
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
}