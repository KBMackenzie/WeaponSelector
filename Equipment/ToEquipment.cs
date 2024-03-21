using System.Collections.Generic;
using WeaponSelector.Choices;

namespace WeaponSelector.Equipment;

public static class ToEquipment
{
    public static List<EquipmentType> FromWeapon(WeaponChoice weapon)
        => weaponMap[weapon];

    public static EquipmentType FromCurse(CurseChoice curse)
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

    private static Dictionary<WeaponChoice, List<EquipmentType>> weaponMap = new()
    {
        { WeaponChoice.Sword,      GetWeaponVariants(EquipmentType.Sword)      },
        { WeaponChoice.Axe,        GetWeaponVariants(EquipmentType.Axe)        },
        { WeaponChoice.Hammer,     GetWeaponVariants(EquipmentType.Hammer)     },
        { WeaponChoice.Dagger,     GetWeaponVariants(EquipmentType.Dagger)     },
        { WeaponChoice.Gauntlet,   GetWeaponVariants(EquipmentType.Gauntlet)   },
    };

    private static Dictionary<CurseChoice, EquipmentType> curseMap = new()
    {
        { CurseChoice.TouchofTurua,        EquipmentType.Tentacles                         },
        { CurseChoice.Maelstrom,           EquipmentType.Tentacles_Circular                },
        { CurseChoice.TouchofIthaqua,      EquipmentType.Tentacles_Ice                     },
        { CurseChoice.TouchoftheRevenant,  EquipmentType.Tentacles_Necromancy              },
        { CurseChoice.DivineBlast,         EquipmentType.EnemyBlast                        },
        { CurseChoice.DivineGuardian,      EquipmentType.EnemyBlast_DeflectsProjectiles    },
        { CurseChoice.DivineBlizzard,      EquipmentType.EnemyBlast_Ice                    },
        { CurseChoice.DivineBlight,        EquipmentType.EnemyBlast_Poison                 },
        { CurseChoice.IchorThrown,         EquipmentType.ProjectileAOE                     },
        { CurseChoice.PointofCorruption,   EquipmentType.ProjectileAOE_ExplosiveImpact     },
        { CurseChoice.PathoftheRighteous,  EquipmentType.ProjectileAOE_GoopTrail           },
        { CurseChoice.CalloftheCrown,      EquipmentType.ProjectileAOE_Charm               },
        { CurseChoice.FlamingShot,         EquipmentType.Fireball                          },
        { CurseChoice.HoundsofFate,        EquipmentType.Fireball_Swarm                    },
        { CurseChoice.CleansingFire,       EquipmentType.Fireball_Triple                   },
        { CurseChoice.StrikeoftheCrown,    EquipmentType.Fireball_Charm                    },
        { CurseChoice.DeathsSweep,         EquipmentType.MegaSlash                         },
        { CurseChoice.DeathsAttendant,     EquipmentType.MegaSlash_Necromancy              },
        { CurseChoice.DeathsSquall,        EquipmentType.MegaSlash_Ice                     },
        { CurseChoice.OathoftheCrown,      EquipmentType.MegaSlash_Charm                   },
    };
}
