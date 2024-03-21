using System.Collections.Generic;

namespace WeaponSelector;

internal class Library
{
    // Names:
    public Dictionary<WeaponChoices, string> WeaponNames => weaponNames;

    Dictionary<WeaponChoices, string> weaponNames = new Dictionary<WeaponChoices, string>()
    {
        { WeaponChoices.Sword,      "Sword"     },
        { WeaponChoices.Axe,        "Axe"       },
        { WeaponChoices.Hammer,     "Hammer"    },
        { WeaponChoices.Dagger,     "Dagger"    },
        { WeaponChoices.Gauntlet,   "Gauntlet"  },
        { WeaponChoices.Random,     "Random"    },
    };

    public Dictionary<WeaponTraits, string> TraitNames => traitNames;

    Dictionary<WeaponTraits, string> traitNames = new Dictionary<WeaponTraits, string>()
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

    public Dictionary<CurseChoices, string> CurseNames => curseNames;

    Dictionary<CurseChoices, string> curseNames = new Dictionary<CurseChoices, string>()
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


    // EquipmentType:
    public Dictionary<WeaponChoices, List<EquipmentType>> Weapons => weapons;

    Dictionary<WeaponChoices, List<EquipmentType>> weapons = new Dictionary<WeaponChoices, List<EquipmentType>>()
    {
        { WeaponChoices.Sword,      GetWeaponVariants(EquipmentType.Sword)      },
        { WeaponChoices.Axe,        GetWeaponVariants(EquipmentType.Axe)        },
        { WeaponChoices.Hammer,     GetWeaponVariants(EquipmentType.Hammer)     },
        { WeaponChoices.Dagger,     GetWeaponVariants(EquipmentType.Dagger)     },
        { WeaponChoices.Gauntlet,   GetWeaponVariants(EquipmentType.Gauntlet)   },
    };

    static List<EquipmentType> GetWeaponVariants(EquipmentType type)
    {
        int start = (int)type;
        int max = start + 7;

        List<EquipmentType> list = new List<EquipmentType>();

        for (int i = start; i < max; i++)
        {
            list.Add((EquipmentType)i);
        }

        return list;
    }

    /*This is true for all weapons:
     * Axe == 100
     * Hammer == 200
     * Dagger == 300 
     * Gauntlet == 400
     * And all of them have +6 variants on top of the normal one.
     * Sword ranges from 0 to 6, for example. Axe ranges from 100 to 106.*/

    public Dictionary<CurseChoices, EquipmentType> Curses => curses;

    Dictionary<CurseChoices, EquipmentType> curses = new Dictionary<CurseChoices, EquipmentType>()
    {
        { CurseChoices.TouchofTurua,        EquipmentType.Tentacles             },
        { CurseChoices.Maelstrom,           EquipmentType.Tentacles_Circular    },
        { CurseChoices.TouchofIthaqua,      EquipmentType.Tentacles_Ice         },
        { CurseChoices.TouchoftheRevenant,  EquipmentType.Tentacles_Necromancy  },
        { CurseChoices.DivineBlast,         EquipmentType.EnemyBlast            },
        { CurseChoices.DivineGuardian,      EquipmentType.EnemyBlast_DeflectsProjectiles},
        { CurseChoices.DivineBlizzard,      EquipmentType.EnemyBlast_Ice        },
        { CurseChoices.DivineBlight,        EquipmentType.EnemyBlast_Poison     },
        { CurseChoices.IchorThrown,         EquipmentType.ProjectileAOE         },
        { CurseChoices.PointofCorruption,   EquipmentType.ProjectileAOE_ExplosiveImpact},
        { CurseChoices.PathoftheRighteous,  EquipmentType.ProjectileAOE_GoopTrail},
        { CurseChoices.CalloftheCrown,      EquipmentType.ProjectileAOE_Charm   },
        { CurseChoices.FlamingShot,         EquipmentType.Fireball              },
        { CurseChoices.HoundsofFate,        EquipmentType.Fireball_Swarm        },
        { CurseChoices.CleansingFire,       EquipmentType.Fireball_Triple       },
        { CurseChoices.StrikeoftheCrown,    EquipmentType.Fireball_Charm        },
        { CurseChoices.DeathsSweep,         EquipmentType.MegaSlash             },
        { CurseChoices.DeathsAttendant,     EquipmentType.MegaSlash_Necromancy  },
        { CurseChoices.DeathsSquall,        EquipmentType.MegaSlash_Ice         },
        { CurseChoices.OathoftheCrown,      EquipmentType.MegaSlash_Charm       },
    };



    /*Dictionary<WeaponChoices, EquipmentType> weapons = new Dictionary<WeaponChoices, EquipmentType>()
      {
      { WeaponChoices.Axe,        EquipmentType.Axe },
      { WeaponChoices.Hammer,     EquipmentType.Hammer },
      { WeaponChoices.Sword,      EquipmentType.Sword },
      { WeaponChoices.Dagger,     EquipmentType.Dagger },
      { WeaponChoices.Gauntlet,   EquipmentType.Gauntlet },
      };*/
}
