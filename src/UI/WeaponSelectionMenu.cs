using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using WeaponSelector.Choices;
using WeaponSelector.Utils;

namespace WeaponSelector.UI;

internal class WeaponSelectionMenu : MonoBehaviour
{
    public GameObject? Parent;
    public Canvas? Canvas;
    public TextMeshProUGUI? TextMesh;

    private readonly LayerMask Layer = LayerMask.NameToLayer("UI");

    private readonly static Dictionary<Direction, Sprite> ArrowSprites = new()
    {
        { Direction.Left,  TextureLoader.MakeSprite(Properties.Resources.ArrowL) },
        { Direction.Right, TextureLoader.MakeSprite(Properties.Resources.ArrowR) }
    };
    private readonly static Sprite BoxSprite = TextureLoader.MakeSprite(Properties.Resources.MenuBox);

    private class Arrows
    {
        public ArrowButton Left { get; }
        public ArrowButton Right { get; }
        public Arrows(ArrowButton left, ArrowButton right)
        {
            Left  = left;
            Right = right;
        }
    }

    private readonly Dictionary<ChangeType, Arrows> ArrowButtons = new();
    private readonly Dictionary<ChangeType, MenuText> TextObjects = new();

    private void Start() // Initialize
    {
        CreateMenu();
    }

    private void CreateMenu()
    {
        GameObject HeartBox = CreateBox();
        Transform BoxParent = HeartBox.transform;

        // Header -- Weapons
        CreateHeader(BoxParent, ChangeType.Weapon);

        // Weapons
        CreateText(BoxParent, ChangeType.Weapon);
        CreateArrows(BoxParent, ChangeType.Weapon);

        // Traits
        CreateText(BoxParent, ChangeType.Trait);
        CreateArrows(BoxParent, ChangeType.Trait);


        // Header -- Curses
        CreateHeader(BoxParent, ChangeType.Curse);

        // Curses
        CreateText(BoxParent, ChangeType.Curse);
        CreateArrows(BoxParent, ChangeType.Curse);

    }

    private GameObject CreateBox()
    {
        GameObject box = new GameObject();
        box.name = "MenuBox";
        box.layer = Layer;
        box.transform.SetParent(Parent?.transform);
        box.transform.localPosition = new Vector3(0f, -100f, 0);

        Image image = box.AddComponent<Image>();
        image.sprite = BoxSprite;
        image.SetNativeSize();
        image.preserveAspect = true;
        TextureLoader.ChangeOpacity(image, 0.8f);

        Draggable draggable = box.AddComponent<Draggable>();
        draggable.Initialize(Canvas);

        box.transform.localScale = new Vector3(0.5f, 0.5f, 0);

        return box;
    }

    private Vector3 UIImageSize(Image img)
    {
        float w = img.sprite.rect.width;
        float h = img.sprite.rect.height;
        return new Vector3(w, h, 0);
    }

    private void CreateHeader(Transform parent, ChangeType type)
    {
        Dictionary<ChangeType, float> GetY = new Dictionary<ChangeType, float>()
        {
            { ChangeType.Weapon, 343f },
            { ChangeType.Curse, -163f },
        };

        float y = GetY[type];

        GameObject textBox = new GameObject();
        textBox.name = $"UIText_Header_{type}";
        textBox.layer = Layer;
        textBox.transform.SetParent(parent);

        TextMeshProUGUI textMesh = textBox.AddComponent<TextMeshProUGUI>();
        textMesh.font = TextMesh?.font;
        textMesh.fontSize = (TextMesh?.fontSize ?? 12) * 0.7f;
        textMesh.text = $"Choose a {type}:";

        textBox.transform.localPosition = new Vector3(0, y, 0);
        textMesh.alignment = TextAlignmentOptions.Center;
    }

    private void CreateText(Transform parent, ChangeType type)
    {
        // DECIDE THINGS.
        Dictionary<ChangeType, float> GetY = new Dictionary<ChangeType, float>()
        {
            { ChangeType.Weapon,   189f },
            { ChangeType.Trait,    20f  },
            { ChangeType.Curse,   -314f }
        };

        float y = GetY[type];

        GameObject textBox = new GameObject();
        textBox.name = $"UIText_{type}";
        textBox.layer = Layer;
        textBox.transform.SetParent(parent);

        TextMeshProUGUI textMesh = textBox.AddComponent<TextMeshProUGUI>();
        textMesh.font = TextMesh?.font;
        textMesh.fontSize = TextMesh?.fontSize ?? 12;
        textMesh.text = GetText(type);

        textBox.transform.localPosition = new Vector3(0, y, 0);
        textMesh.alignment = TextAlignmentOptions.Center;

        MenuText textScript = textBox.AddComponent<MenuText>();
        textScript.MenuInstance = this;
        textScript.TextMesh = textMesh;

        // Add with a reference to the Change
        TextObjects.Add(type, textScript);
    }

    private void CreateArrows(Transform parent, ChangeType type)
    {
        // DECIDE THINGS.
        Dictionary<ChangeType, float> GetY = new Dictionary<ChangeType, float>()
        {
            { ChangeType.Weapon,  193f  },
            { ChangeType.Trait,   30f   },
            { ChangeType.Curse,  -304f  }
        };

        float y = GetY[type];

        // Left arrow
        GameObject leftArrow = new GameObject();
        leftArrow.name = $"LeftArrow_{type}";
        leftArrow.layer = Layer;
        leftArrow.transform.SetParent(parent);
        leftArrow.transform.localPosition = new Vector3(-360f, y, 0);
        leftArrow.transform.localScale = new Vector3(0.24f, 0.24f, 0);

        Image img = leftArrow.AddComponent<Image>();
        img.sprite = ArrowSprites[Direction.Left];
        img.SetNativeSize();

        ArrowButton arrL = leftArrow.AddComponent<ArrowButton>();
        arrL.MenuInstance = this;
        arrL.Portrait = img;
        arrL.Direction = Direction.Left;
        arrL.Change = type;
        arrL.Normal = ArrowSprites[Direction.Left];


        // Right arrow
        GameObject rightArrow = new GameObject();
        rightArrow.name = $"RightArrow_{type}";
        rightArrow.layer = Layer;
        rightArrow.transform.SetParent(parent);
        rightArrow.transform.localPosition = new Vector3(360, y, 0);
        rightArrow.transform.localScale = new Vector3(0.24f, 0.24f, 0);

        Image img2 = rightArrow.AddComponent<Image>();
        img2.sprite = ArrowSprites[Direction.Right];
        img2.SetNativeSize();

        ArrowButton arrR = rightArrow.AddComponent<ArrowButton>();
        arrR.MenuInstance = this;
        arrR.Portrait = img2;
        arrL.Direction = Direction.Right;
        arrR.Change = type;
        arrR.Normal = ArrowSprites[Direction.Left];

        ArrowButtons.Add(type, new Arrows(arrL, arrR));
    }

    private string GetText(ChangeType type)
    {
        // todo: MAKE THIS NOT RELY ON PATCHES
        /*
        switch (type)
        {
            case ChangeType.Weapon:
                return NameManager.GetWeaponName(WeaponPatches.Weapon);
            case ChangeType.Trait:
                return NameManager.GetTraitName(WeaponPatches.Trait);
            case ChangeType.Curse:
                return NameManager.GetCurseName(WeaponPatches.Curse);
        }
        */
        return "??";
    }

    public void UpdateText(ChangeType type)
    {
        // TODO: Make this dynamically display something else and not just weapon names
        // todo: make this NOT RELY ON PATCHES!
        /*
        switch (type)
        {
            case ChangeType.Weapon:
                TextObjects[type].tmp.text = NameManager.GetWeaponName(WeaponPatches.Weapon);
                break;
            case ChangeType.Trait:
                TextObjects[type].tmp.text = NameManager.GetTraitName(WeaponPatches.Trait);
                break;
            case ChangeType.Curse:
                TextObjects[type].tmp.text = NameManager.GetCurseName(WeaponPatches.Curse);
                break;
        }
        */
    }

    // Update() holds key presses and all that jazz. c:
    private void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            ArrowButtons[ChangeType.Weapon].Right.ArrowClick();
        }

        if (Input.GetKeyDown("k"))
        {
            ArrowButtons[ChangeType.Trait].Right.ArrowClick();
        }

        if (Input.GetKeyDown("l"))
        {
            ArrowButtons[ChangeType.Curse].Right.ArrowClick();
        }
    }
}
