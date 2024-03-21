using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace WeaponSelector.UI;

internal class ArrowButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Direction direction;
    private MenuOption option;
    private Image? portrait;
    private Sprite? normalSprite;

    public void Initialize(Direction direction, MenuOption option, Image? portrait, Sprite? normalSprite)
    {
        this.direction = direction;
        this.option = option;
        this.portrait = portrait;
        this.normalSprite = normalSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ArrowClick();
    }

    public bool IsLeftArrow()
        => direction == Direction.Left;

    public bool IsRightArrow()
        => direction == Direction.Right;

    public void ArrowClick()
    {
        SetOption();
        WeaponSelectionMenu.Instance?.UpdateText(option);
    }

    private void SetOption()
    {
        int modifier = IsLeftArrow() ? -1 : 1;
        WeaponSelectionMenu.Instance?.State.SetOption(option, modifier);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeColor(hovered: true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeColor(hovered: false);
    }

    void ChangeColor(bool hovered)
    {
        if (portrait == null) return;
        Color color = hovered ? new Color(1f, 0, 0, 1f) : new Color(1f, 1f, 1f, 1f);
        portrait.color = color;
    }
}
