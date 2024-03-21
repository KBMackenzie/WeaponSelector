using UnityEngine;
using UnityEngine.EventSystems;

namespace WeaponSelector.UI;

internal class Draggable : MonoBehaviour, IDragHandler
{
    private Canvas? canvas;
    private RectTransform? dragRect;

    public void Initialize(Canvas? canvas)
    {
        this.canvas = canvas;
    }

    private void Start()
    {
        dragRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragRect == null || canvas == null) return;
        dragRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
