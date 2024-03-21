using System.Collections.Generic;

namespace WeaponSelector;

public static class ToEquipment
{
    public static List<EquipmentType> FromWeapon(WeaponChoices weapon)
        => weaponMap[weapon];

    public static EquipmentType FromCurse(CurseChoices curse)
        => curseMap[curse];

    public static List<EquipmentType> GetWeaponVariants(EquipmentType type)
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
     * Blunderbuss == 450
     * And all of them have +6 variants on top of the normal one.
     * Sword ranges from 0 to 6, for example. Axe ranges from 100 to 106.*/

    private static Dictionary<WeaponChoices, List<EquipmentType>> weaponMap = new()
    {
        { WeaponChoices.Sword,      GetWeaponVariants(EquipmentType.Sword)      },
        { WeaponChoices.Axe,        GetWeaponVariants(EquipmentType.Axe)        },
        { WeaponChoices.Hammer,     GetWeaponVariants(EquipmentType.Hammer)     },
        { WeaponChoices.Dagger,     GetWeaponVariants(EquipmentType.Dagger)     },
        { WeaponChoices.Gauntlet,   GetWeaponVariants(EquipmentType.Gauntlet)   },
    };

    private static Dictionary<CurseChoices, EquipmentType> curseMap = new()
    {
        { CurseChoices.TouchofTurua,        EquipmentType.Tentacles                         },
        { CurseChoices.Maelstrom,           EquipmentType.Tentacles_Circular                },
        { CurseChoices.TouchofIthaqua,      EquipmentType.Tentacles_Ice                     },
        { CurseChoices.TouchoftheRevenant,  EquipmentType.Tentacles_Necromancy              },
        { CurseChoices.DivineBlast,         EquipmentType.EnemyBlast                        },
        { CurseChoices.DivineGuardian,      EquipmentType.EnemyBlast_DeflectsProjectiles    },
        { CurseChoices.DivineBlizzard,      EquipmentType.EnemyBlast_Ice                    },
        { CurseChoices.DivineBlight,        EquipmentType.EnemyBlast_Poison                 },
        { CurseChoices.IchorThrown,         EquipmentType.ProjectileAOE                     },
        { CurseChoices.PointofCorruption,   EquipmentType.ProjectileAOE_ExplosiveImpact     },
        { CurseChoices.PathoftheRighteous,  EquipmentType.ProjectileAOE_GoopTrail           },
        { CurseChoices.CalloftheCrown,      EquipmentType.ProjectileAOE_Charm               },
        { CurseChoices.FlamingShot,         EquipmentType.Fireball                          },
        { CurseChoices.HoundsofFate,        EquipmentType.Fireball_Swarm                    },
        { CurseChoices.CleansingFire,       EquipmentType.Fireball_Triple                   },
        { CurseChoices.StrikeoftheCrown,    EquipmentType.Fireball_Charm                    },
        { CurseChoices.DeathsSweep,         EquipmentType.MegaSlash                         },
        { CurseChoices.DeathsAttendant,     EquipmentType.MegaSlash_Necromancy              },
        { CurseChoices.DeathsSquall,        EquipmentType.MegaSlash_Ice                     },
        { CurseChoices.OathoftheCrown,      EquipmentType.MegaSlash_Charm                   },
    };
}
