using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WeaponSelector;

internal class BoxHelper : MonoBehaviour, IDragHandler
{
    public WeaponMenu menuInstance;
    public Image img;
    public RectTransform dragRectTransform;
    public Canvas canvas;

    void Start()
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
    public WeaponMenu menuInstance;
    public TextMeshProUGUI tmp;
}
