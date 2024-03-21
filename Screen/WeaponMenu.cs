using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace WeaponSelector;

internal class WeaponMenu : MonoBehaviour
{
    public GameObject Parent;
    private LayerMask Layer = LayerMask.NameToLayer("UI");
    private static LoadTexture TexLoader = new LoadTexture(FilterMode.Bilinear);

    public readonly Dictionary<string, Sprite> ArrowSprites = new Dictionary<string, Sprite>()
    {
        { "Left",     TexLoader.MakeSprite(Properties.Resources.ArrowL) },
        { "Right",    TexLoader.MakeSprite(Properties.Resources.ArrowR) }
    };

    private Sprite BoxSprite = TexLoader.MakeSprite(Properties.Resources.MenuBox3);

    public TextMeshProUGUI UIText;
    public Canvas canvas;

    // Arrows
    private Dictionary<Change, (ArrowButtons, ArrowButtons)> Arrows = new Dictionary<Change, (ArrowButtons, ArrowButtons)>();

    // Text
    private Dictionary<Change, WeaponText> TextObjects = new Dictionary<Change, WeaponText>();

    private void Start() // Initialize
    {
        CreateMenu();
    }

    private void CreateMenu()
    {
        GameObject HeartBox = CreateBox();
        Transform BoxParent = HeartBox.transform;

        // Header -- Weapons
        CreateHeader(BoxParent, Change.Weapon);

        // Weapons
        CreateText(BoxParent, Change.Weapon);
        CreateArrows(BoxParent, Change.Weapon);

        // Traits
        CreateText(BoxParent, Change.Trait);
        CreateArrows(BoxParent, Change.Trait);


        // Header -- Curses
        CreateHeader(BoxParent, Change.Curse);

        // Curses
        CreateText(BoxParent, Change.Curse);
        CreateArrows(BoxParent, Change.Curse);

    }

    private GameObject CreateBox()
    {
        GameObject box = new GameObject();
        box.name = "MenuBox";
        box.layer = Layer;
        box.transform.SetParent(Parent.transform);
        box.transform.localPosition = new Vector3(0f, -100f, 0);

        Image img = box.AddComponent<Image>();
        img.sprite = BoxSprite;
        img.SetNativeSize();
        img.preserveAspect = true;
        TexLoader.ChangeOpacity(ref img, 0.8f);

        BoxHelper helper = box.AddComponent<BoxHelper>();
        helper.menuInstance = this;
        helper.img = img;
        helper.canvas = canvas;

        box.transform.localScale = new Vector3(0.5f, 0.5f, 0);

        return box;
    }

    private Vector3 UIImageSize(Image img)
    {
        float w = img.sprite.rect.width;
        float h = img.sprite.rect.height;
        return new Vector3(w, h, 0);
    }

    private void CreateHeader(Transform parent, Change type)
    {
        Dictionary<Change, float> GetY = new Dictionary<Change, float>()
        {
            { Change.Weapon, 343f },
            { Change.Curse, -163f },
        };

        float y = GetY[type];

        GameObject textBox = new GameObject();
        textBox.name = $"UIText_Header_{type}";
        textBox.layer = Layer;
        textBox.transform.SetParent(parent);

        TextMeshProUGUI textMesh = textBox.AddComponent<TextMeshProUGUI>();
        textMesh.font = UIText.font;
        textMesh.fontSize = UIText.fontSize * 0.7f;
        textMesh.text = $"Choose a {type}:";

        textBox.transform.localPosition = new Vector3(0, y, 0);
        textMesh.alignment = TextAlignmentOptions.Center;
    }

    private void CreateText(Transform parent, Change type)
    {
        // DECIDE THINGS.
        Dictionary<Change, float> GetY = new Dictionary<Change, float>()
        {
            { Change.Weapon,   189f },
            { Change.Trait,    20f  },
            { Change.Curse,   -314f }
        };

        float y = GetY[type];

        GameObject textBox = new GameObject();
        textBox.name = $"UIText_{type}";
        textBox.layer = Layer;
        textBox.transform.SetParent(parent);

        TextMeshProUGUI textMesh = textBox.AddComponent<TextMeshProUGUI>();
        textMesh.font = UIText.font;
        textMesh.fontSize = UIText.fontSize;
        textMesh.text = GetText(type);

        textBox.transform.localPosition = new Vector3(0, y, 0);
        textMesh.alignment = TextAlignmentOptions.Center;

        WeaponText textScript = textBox.AddComponent<WeaponText>();
        textScript.menuInstance = this;
        textScript.tmp = textMesh;

        // Add with a reference to the Change
        TextObjects.Add(type, textScript);
    }

    private void CreateArrows(Transform parent, Change type)
    {
        // DECIDE THINGS.
        Dictionary<Change, float> GetY = new Dictionary<Change, float>()
        {
            { Change.Weapon,  193f  },
            { Change.Trait,   30f   },
            { Change.Curse,  -304f  }
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
        img.sprite = ArrowSprites["Left"];
        img.SetNativeSize();

        ArrowButtons arrL = leftArrow.AddComponent<ArrowButtons>();
        arrL.menuInstance = this;
        arrL.img = img;
        arrL.isLeft = true;
        arrL.type = type;
        arrL.Normal = ArrowSprites["Left"];


        // Right arrow
        GameObject rightArrow = new GameObject();
        rightArrow.name = $"RightArrow_{type}";
        rightArrow.layer = Layer;
        rightArrow.transform.SetParent(parent);
        rightArrow.transform.localPosition = new Vector3(360, y, 0);
        rightArrow.transform.localScale = new Vector3(0.24f, 0.24f, 0);

        Image img2 = rightArrow.AddComponent<Image>();
        img2.sprite = ArrowSprites["Right"];
        img2.SetNativeSize();

        ArrowButtons arrR = rightArrow.AddComponent<ArrowButtons>();
        arrR.menuInstance = this;
        arrR.img = img2;
        arrR.isLeft = false;
        arrR.type = type;
        arrR.Normal = ArrowSprites["Right"];

        Arrows.Add(type, (arrL, arrR));
    }

    private string GetText(Change type)
    {
        switch (type)
        {
            case Change.Weapon:
                return NameManager.GetWeaponName(WeaponPatches.Weapon);
            case Change.Trait:
                return NameManager.GetTraitName(WeaponPatches.Trait);
            case Change.Curse:
                return NameManager.GetCurseName(WeaponPatches.Curse);
        }
        return "??";
    }

    public void UpdateText(Change type)
    {
        // TODO: Make this dynamically display something else and not just weapon names
        switch (type)
        {
            case Change.Weapon:
                TextObjects[type].tmp.text = NameManager.GetWeaponName(WeaponPatches.Weapon);
                break;
            case Change.Trait:
                TextObjects[type].tmp.text = NameManager.GetTraitName(WeaponPatches.Trait);
                break;
            case Change.Curse:
                TextObjects[type].tmp.text = NameManager.GetCurseName(WeaponPatches.Curse);
                break;
        }
    }

    // Update() holds key presses and all that jazz. c:
    private void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            Arrows[Change.Weapon].Item2.ArrowClick();
        }

        if (Input.GetKeyDown("k"))
        {
            Arrows[Change.Trait].Item2.ArrowClick();
        }

        if (Input.GetKeyDown("l"))
        {
            Arrows[Change.Curse].Item2.ArrowClick();
        }
    }
}
