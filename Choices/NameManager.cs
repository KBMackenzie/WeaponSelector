using System.Collections.Generic;
using WeaponSelector.Enums;

namespace WeaponSelector.Choices;

public static class NameManager
{
    public static string GetWeaponName(WeaponChoice weapon)
        => weaponNames[weapon];

    public static string GetTraitName(WeaponTrait trait)
        => traitNames[trait];

    public static string GetCurseName(CurseChoice curse)
        => curseNames[curse];

    private static Dictionary<WeaponChoice, string> weaponNames = new()
    {
        { WeaponChoice.Sword,      "Sword"     },
        { WeaponChoice.Axe,        "Axe"       },
        { WeaponChoice.Hammer,     "Hammer"    },
        { WeaponChoice.Dagger,     "Dagger"    },
        { WeaponChoice.Gauntlet,   "Gauntlet"  },
        { WeaponChoice.Random,     "Random"    },
    };

    private static Dictionary<WeaponTrait, string> traitNames = new()
    {
        { WeaponTrait.Normal,      "Normal"        },
        { WeaponTrait.Poison,      "Bane"          },
        { WeaponTrait.Critical,    "Merciless"     },
        { WeaponTrait.Healing,     "Vampiric"      },
        { WeaponTrait.Fervour,     "Zealous"       },
        { WeaponTrait.Godly,       "Godly"         },
        { WeaponTrait.Necromancy,  "Necromantic"   },
        { WeaponTrait.Random,      "Random"        },
    };

    private static Dictionary<CurseChoice, string> curseNames = new()
    {
        { CurseChoice.TouchofTurua,        "Touch of Torua"            },
        { CurseChoice.Maelstrom,           "Maelstrom"                 },
        { CurseChoice.TouchofIthaqua,      "Touch of Ithaqua"          },
        { CurseChoice.TouchoftheRevenant,  "Touch of the Revenant"     },
        { CurseChoice.DivineBlast,         "Divine Blast"              },
        { CurseChoice.DivineGuardian,      "Divine Guardian"           },
        { CurseChoice.DivineBlizzard,      "Divine Blizzard"           },
        { CurseChoice.DivineBlight,        "Divine Blight"             },
        { CurseChoice.IchorThrown,         "Ichor Thrown"              },
        { CurseChoice.PointofCorruption,   "Point of Corruption"       },
        { CurseChoice.PathoftheRighteous,  "Path of the Righteous"     },
        { CurseChoice.CalloftheCrown,      "Call of the Crown"         },
        { CurseChoice.FlamingShot,         "Flaming Shot"              },
        { CurseChoice.HoundsofFate,        "Hounds of Fate"            },
        { CurseChoice.CleansingFire,       "Cleansing Fire"            },
        { CurseChoice.StrikeoftheCrown,    "Strike of the Crown"       },
        { CurseChoice.DeathsSweep,         "Death's Sweep"             },
        { CurseChoice.DeathsAttendant,     "Death's Attendant"         },
        { CurseChoice.DeathsSquall,        "Death's Squall"            },
        { CurseChoice.OathoftheCrown,      "Oath of the Crown"         },
        { CurseChoice.Random,              "Random"                    },
    };
}
