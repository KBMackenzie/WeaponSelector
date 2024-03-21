using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace WeaponSelector;

internal class ArrowButtons : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponMenu menuInstance;
    public Image img;
    public bool isLeft;

    public ChangeType type;

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
            case ChangeType.Weapon:
                {
                    int cur = (int)WeaponPatches.Weapon;
                    int max = WeaponPatches.Weapon.EnumLength();
                    int choice = ParseChoice(cur, max);
                    data.Item1 = (WeaponChoice)choice;
                }
                break;
            case ChangeType.Trait:
                {
                    int cur = (int)WeaponPatches.Trait;
                    int max = WeaponPatches.Trait.EnumLength();
                    int choice = ParseChoice(cur, max);
                    data.Item2 = (WeaponTrait)choice;
                }
                break;
            case ChangeType.Curse:
                {
                    int cur = (int)WeaponPatches.Curse;
                    int max = WeaponPatches.Curse.EnumLength();
                    int choice = ParseChoice(cur, max);
                    data.Item3 = (CurseChoice)choice;
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
