using System;
using WeaponSelector.Choices;
using WeaponSelector.SaveData;
using WeaponSelector.Utils;

namespace WeaponSelector.UI;

public class MenuState
{
    public WeaponChoice Weapon { get; private set; }
    public WeaponTrait Trait   { get; private set; }
    public CurseChoice Curse   { get; private set; }

    public ChoiceList GetState()
        => new ChoiceList(Weapon, Trait, Curse);

    public void Save()
    {
        Plugin.Instance?.Log("Saving state...");
        SaveFile.SaveData = GetState();
    }

    public void SetOption(MenuOption option, int modifier)
    {
        Action<int> set = option switch 
        {
            MenuOption.Weapon => SetWeapon,
            MenuOption.Trait  => SetTrait,
            MenuOption.Curse  => SetCurse,
            _                 => _ => {}
        };
        set(modifier);
    }

    public void SetWeapon(int modifier)
    {
        Weapon = (WeaponChoice)MathUtils.WrapAround(
            value: (int)Weapon + modifier,
            min: 0,
            max: ChoiceManager.WeaponChoiceCount - 1
        );

    }

    public void SetTrait(int modifier)
    {
        Trait = (WeaponTrait)MathUtils.WrapAround(
            value: (int)Trait + modifier,
            min: 0,
            max: ChoiceManager.TraitChoiceCount - 1
        );
    }

    public void SetCurse(int modifier)
    {
        Curse = (CurseChoice)MathUtils.WrapAround(
            value: (int)Curse + modifier,
            min: 0,
            max: ChoiceManager.CurseChoiceCount - 1
        );
    }
}
