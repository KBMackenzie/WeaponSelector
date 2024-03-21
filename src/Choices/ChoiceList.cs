namespace WeaponSelector.Choices;

public class ChoiceList
{
    public WeaponChoice Weapon { get; set; }
    public WeaponTrait Trait { get; set; }
    public CurseChoice Curse { get; set; }

    public ChoiceList(WeaponChoice weapon, WeaponTrait trait, CurseChoice curse) {
        Weapon = weapon;
        Trait  = trait;
        Curse  = curse;
    }
}
