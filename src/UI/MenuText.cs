using UnityEngine;
using TMPro;

namespace WeaponSelector.UI;

internal class MenuText : MonoBehaviour
{
    public TextMeshProUGUI? TextMesh;

    public void Initialize(TextMeshProUGUI? textMesh)
    {
        TextMesh = textMesh;
    }
}
