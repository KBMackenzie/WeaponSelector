using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;
using WeaponSelector.Choices;

namespace WeaponSelector.UI;

internal class ArrowButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponSelectionMenu MenuInstance;
    public Image Portrait;
    public Direction Direction;
    public ChangeType Change;
    public Sprite Normal;

    public void OnPointerDown(PointerEventData eventData)
    {
        ArrowClick();
    }

    public bool IsLeftArrow()
        => Direction == Direction.Left;

    public bool IsRightArrow()
        => Direction == Direction.Right;

    public void ArrowClick()
    {
        /*
        var data = SaveFile.SaveData;

        switch (type)
        {
            case ChangeType.Weapon:
                {
                    int cur = (int)WeaponPatches.Weapon;
                    int max = ChoiceManager.WeaponChoiceCount;
                    int choice = ParseChoice(cur, max);
                    data.Item1 = (WeaponChoice)choice;
                }
                break;
            case ChangeType.Trait:
                {
                    int cur = (int)WeaponPatches.Trait;
                    int max = ChoiceManager.TraitChoiceCount;
                    int choice = ParseChoice(cur, max);
                    data.Item2 = (WeaponTrait)choice;
                }
                break;
            case ChangeType.Curse:
                {
                    int cur = (int)WeaponPatches.Curse;
                    int max = ChoiceManager.CurseChoiceCount;
                    int choice = ParseChoice(cur, max);
                    data.Item3 = (CurseChoice)choice;
                }
                break;
        }

        SaveFile.SaveData = data;
        */

        // todo: redo all of this. instead of saving to file on every click,
        // please store data in memory and only save when user saves their game!

        MenuInstance.UpdateText(Change);
    }

    private int ParseChoice(int index, int max)
    {
        index = IsLeftArrow() ? index - 1 : index + 1;
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
        Portrait.color = temp;
    }
}
