using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WeaponSelector.UI;

internal class BoxHelper : MonoBehaviour, IDragHandler
{
    public WeaponSelectionMenu menuInstance;
    public Image img;
    public RectTransform dragRectTransform;
    public Canvas canvas;

    private void Start()
    {
        dragRectTransform = GetComponent<RectTransform>();
        // maybe pass heartmenu transform here instead?
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}

internal class WeaponText : MonoBehaviour
{
    public WeaponSelectionMenu menuInstance;
    public TextMeshProUGUI tmp;
}
