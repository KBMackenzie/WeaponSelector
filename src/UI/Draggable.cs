using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WeaponSelector.UI;

internal class Draggable : MonoBehaviour, IDragHandler
{
    public Canvas? Canvas;
    private RectTransform? DragRect;

    public void Initialize(Canvas? canvas)
    {
        Canvas = canvas;
    }

    private void Start()
    {
        DragRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DragRect == null || Canvas == null) return;
        DragRect.anchoredPosition += eventData.delta / Canvas.scaleFactor;
    }
}
