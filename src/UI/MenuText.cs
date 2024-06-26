using UnityEngine;
using TMPro;

namespace WeaponSelector.UI;

internal class MenuText : MonoBehaviour
{
    private TextMeshProUGUI? textMesh;

    public void Initialize(TextMeshProUGUI? textMesh)
    {
        this.textMesh = textMesh;
    }

    public void SetText(string message)
    {
        if (textMesh == null) return;
        textMesh.text = message;
    }
}
