using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace WeaponSelector;

internal class ArrowButtons : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponMenu menuInstance;
    public Image img;
    public bool isLeft;

    public Change type;

    public Sprite Normal;

    public void OnPointerDown(PointerEventData eventData)
    {
        ArrowClick();
    }

    public void ArrowClick()
    {
        var data = SaveFile.SaveData;

        switch (type)
        {
            case Change.Weapon:
                {
                    int cur = (int)WeaponPatches.Weapon;
                    int max = WeaponPatches.Weapon.EnumLength();
                    int choice = ParseChoice(cur, max);
                    data.Item1 = (WeaponChoices)choice;
                }
                break;
            case Change.Trait:
                {
                    int cur = (int)WeaponPatches.Trait;
                    int max = WeaponPatches.Trait.EnumLength();
                    int choice = ParseChoice(cur, max);
                    data.Item2 = (WeaponTraits)choice;
                }
                break;
            case Change.Curse:
                {
                    int cur = (int)WeaponPatches.Curse;
                    int max = WeaponPatches.Curse.EnumLength();
                    int choice = ParseChoice(cur, max);
                    data.Item3 = (CurseChoices)choice;
                }
                break;
        }

        SaveFile.SaveData = data;
        menuInstance.UpdateText(type);
    }

    private int ParseChoice(int index, int max)
    {
        index = isLeft ? index -= 1 : index += 1;
        if (index >= max) index = 0;
        if (index < 0) index = max - 1; // Last item
        return index;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeColor(isRed: true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeColor(isRed: false);
    }

    void ChangeColor(bool isRed)
    {
        Color temp = isRed ? new Color(1f, 0, 0, 1f) : new Color(1f, 1f, 1f, 1f);
        img.color = temp;
    }
}
