using System.Collections.Generic;

namespace WeaponSelector;

public static class NameManager
{
    public static string GetWeaponName(WeaponChoices weapon)
        => weaponNames[weapon];

    public static string GetTraitName(WeaponTraits trait)
        => traitNames[trait];

    public static string GetCurseName(CurseChoices curse)
        => curseNames[curse];

    private static Dictionary<WeaponChoices, string> weaponNames = new()
    {
        { WeaponChoices.Sword,      "Sword"     },
        { WeaponChoices.Axe,        "Axe"       },
        { WeaponChoices.Hammer,     "Hammer"    },
        { WeaponChoices.Dagger,     "Dagger"    },
        { WeaponChoices.Gauntlet,   "Gauntlet"  },
        { WeaponChoices.Random,     "Random"    },
    };

    private static Dictionary<WeaponTraits, string> traitNames = new()
    {
        { WeaponTraits.Normal,      "Normal"        },
        { WeaponTraits.Poison,      "Bane"          },
        { WeaponTraits.Critical,    "Merciless"     },
        { WeaponTraits.Healing,     "Vampiric"      },
        { WeaponTraits.Fervour,     "Zealous"       },
        { WeaponTraits.Godly,       "Godly"         },
        { WeaponTraits.Necromancy,  "Necromantic"   },
        { WeaponTraits.Random,      "Random"        },
    };

    private static Dictionary<CurseChoices, string> curseNames = new()
    {
        { CurseChoices.TouchofTurua,        "Touch of Torua"            },
        { CurseChoices.Maelstrom,           "Maelstrom"                 },
        { CurseChoices.TouchofIthaqua,      "Touch of Ithaqua"          },
        { CurseChoices.TouchoftheRevenant,  "Touch of the Revenant"     },
        { CurseChoices.DivineBlast,         "Divine Blast"              },
        { CurseChoices.DivineGuardian,      "Divine Guardian"           },
        { CurseChoices.DivineBlizzard,      "Divine Blizzard"           },
        { CurseChoices.DivineBlight,        "Divine Blight"             },
        { CurseChoices.IchorThrown,         "Ichor Thrown"              },
        { CurseChoices.PointofCorruption,   "Point of Corruption"       },
        { CurseChoices.PathoftheRighteous,  "Path of the Righteous"     },
        { CurseChoices.CalloftheCrown,      "Call of the Crown"         },
        { CurseChoices.FlamingShot,         "Flaming Shot"              },
        { CurseChoices.HoundsofFate,        "Hounds of Fate"            },
        { CurseChoices.CleansingFire,       "Cleansing Fire"            },
        { CurseChoices.StrikeoftheCrown,    "Strike of the Crown"       },
        { CurseChoices.DeathsSweep,         "Death's Sweep"             },
        { CurseChoices.DeathsAttendant,     "Death's Attendant"         },
        { CurseChoices.DeathsSquall,        "Death's Squall"            },
        { CurseChoices.OathoftheCrown,      "Oath of the Crown"         },
        { CurseChoices.Random,              "Random"                    },
    };
}
