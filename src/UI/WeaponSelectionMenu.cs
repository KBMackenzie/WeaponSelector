﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using WeaponSelector.Utils;
using WeaponSelector.Choices;

namespace WeaponSelector.UI;

internal class WeaponSelectionMenu : MonoBehaviour
{
    public static WeaponSelectionMenu? Instance;

    public GameObject? Parent { get; private set; }
    public Canvas? Canvas { get; private set; }
    public TextMeshProUGUI? TextMesh { get; private set; }

    public readonly MenuState State = new MenuState();

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

    private readonly Dictionary<MenuOption, Arrows> ArrowButtons = new();
    private readonly Dictionary<MenuOption, MenuText> TextObjects = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateMenu();
    }

    public void Initialize(GameObject parent, Canvas canvas, TextMeshProUGUI textMesh)
    {
        Parent = parent;
        Canvas = canvas;
        TextMesh = textMesh;
    }

    private void CreateMenu()
    {
        GameObject HeartBox = CreateBox();
        Transform BoxParent = HeartBox.transform;

        // Header -- Weapons
        CreateHeader(BoxParent, MenuOption.Weapon);

        // Weapons
        CreateText(BoxParent, MenuOption.Weapon);
        CreateArrows(BoxParent, MenuOption.Weapon);

        // Traits
        CreateText(BoxParent, MenuOption.Trait);
        CreateArrows(BoxParent, MenuOption.Trait);


        // Header -- Curses
        CreateHeader(BoxParent, MenuOption.Curse);

        // Curses
        CreateText(BoxParent, MenuOption.Curse);
        CreateArrows(BoxParent, MenuOption.Curse);

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

    private void CreateHeader(Transform parent, MenuOption option)
    {
        float yPosition = option switch
        {
            MenuOption.Weapon =>  343f,
            MenuOption.Curse  => -163f,
            _                 => default,
        };

        GameObject textBox = new GameObject();
        textBox.name = $"UIText_Header_{option}";
        textBox.layer = Layer;
        textBox.transform.SetParent(parent);

        TextMeshProUGUI textMesh = textBox.AddComponent<TextMeshProUGUI>();
        textMesh.font = TextMesh?.font;
        textMesh.fontSize = (TextMesh?.fontSize ?? 12) * 0.7f;
        textMesh.text = $"Choose a {option}:";

        textBox.transform.localPosition = new Vector3(0, yPosition, 0);
        textMesh.alignment = TextAlignmentOptions.Center;
    }

    private void CreateText(Transform parent, MenuOption option)
    {
        float yPosition = option switch
        {
            MenuOption.Weapon =>  189f,
            MenuOption.Trait  =>  20f,
            MenuOption.Curse  => -314f,
            _                 => default,
        };

        GameObject textBox = new GameObject();
        textBox.name = $"UIText_{option}";
        textBox.layer = Layer;
        textBox.transform.SetParent(parent);

        TextMeshProUGUI textMesh = textBox.AddComponent<TextMeshProUGUI>();
        textMesh.font = TextMesh?.font;
        textMesh.fontSize = TextMesh?.fontSize ?? 12;
        textMesh.text = GetText(option);

        textBox.transform.localPosition = new Vector3(0, yPosition, 0);
        textMesh.alignment = TextAlignmentOptions.Center;

        MenuText menuText = textBox.AddComponent<MenuText>();
        menuText.Initialize(textMesh);

        TextObjects.Add(option, menuText);
    }

    private void CreateArrows(Transform parent, MenuOption option)
    {
        float yPosition = option switch
        {
            MenuOption.Weapon =>  193f,
            MenuOption.Trait  =>  30f,
            MenuOption.Curse  => -304f,
            _                 => default,
        };

        GameObject leftArrow = new GameObject();
        leftArrow.name = $"LeftArrow_{option}";
        leftArrow.layer = Layer;
        leftArrow.transform.SetParent(parent);
        leftArrow.transform.localPosition = new Vector3(-360f, yPosition, 0);
        leftArrow.transform.localScale = new Vector3(0.24f, 0.24f, 0);

        Image leftArrowImage = leftArrow.AddComponent<Image>();
        leftArrowImage.sprite = ArrowSprites[Direction.Left];
        leftArrowImage.SetNativeSize();

        ArrowButton leftArrowButton = leftArrow.AddComponent<ArrowButton>();
        leftArrowButton.Initialize( 
            Direction.Left,
            option,
            leftArrowImage,
            ArrowSprites[Direction.Left]
        );

        GameObject rightArrow = new GameObject();
        rightArrow.name = $"RightArrow_{option}";
        rightArrow.layer = Layer;
        rightArrow.transform.SetParent(parent);
        rightArrow.transform.localPosition = new Vector3(360, yPosition, 0);
        rightArrow.transform.localScale = new Vector3(0.24f, 0.24f, 0);

        Image rightArrowImage = rightArrow.AddComponent<Image>();
        rightArrowImage.sprite = ArrowSprites[Direction.Right];
        rightArrowImage.SetNativeSize();

        ArrowButton rightArrowButton = rightArrow.AddComponent<ArrowButton>();
        rightArrowButton.Initialize(
            Direction.Right,
            option,
            rightArrowImage,
            ArrowSprites[Direction.Left]
        );

        ArrowButtons.Add(option, new Arrows(leftArrowButton, rightArrowButton));
    }

    private string GetText(MenuOption option)
        => option switch
        {
            MenuOption.Weapon => NameManager.GetWeaponName(State.Weapon),
            MenuOption.Trait  => NameManager.GetTraitName(State.Trait),
            MenuOption.Curse  => NameManager.GetCurseName(State.Curse),
            _ => "??"
        };

    public void UpdateText(MenuOption option)
    {
        TextObjects[option].SetText(GetText(option));
    }

    private void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            ArrowButtons[MenuOption.Weapon].Right.ArrowClick();
        }

        if (Input.GetKeyDown("k"))
        {
            ArrowButtons[MenuOption.Trait].Right.ArrowClick();
        }

        if (Input.GetKeyDown("l"))
        {
            ArrowButtons[MenuOption.Curse].Right.ArrowClick();
        }
    }
}
