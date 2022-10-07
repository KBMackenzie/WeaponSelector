using Lamb.UI.PauseMenu;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace WeaponSelector
{
    internal class WeaponMenu : MonoBehaviour
    {
        public GameObject Parent;
        LayerMask Layer = LayerMask.NameToLayer("UI");
        static LoadTexture TexLoader = new LoadTexture(FilterMode.Bilinear);

        public readonly Dictionary<string, Sprite> ArrowSprites = new Dictionary<string, Sprite>()
        {
            { "Left",     TexLoader.MakeSprite(Properties.Resources.ArrowL) },
            { "Right",    TexLoader.MakeSprite(Properties.Resources.ArrowR) }
        };

        Sprite BoxSprite = TexLoader.MakeSprite(Properties.Resources.MenuBox3);

        public TextMeshProUGUI UIText;
        public Canvas canvas;

        // Arrows
        Dictionary<Change, (ArrowButtons, ArrowButtons)> Arrows = new Dictionary<Change, (ArrowButtons, ArrowButtons)>();

        // Text
        Dictionary<Change, WeaponText> TextObjects = new Dictionary<Change, WeaponText>();

        // Library!
        Library library = new Library();

        void Start() // Initialize
        {
            CreateMenu();
        }

        void CreateMenu()
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

        GameObject CreateBox()
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

        Vector3 UIImageSize(Image img)
        {
            float w = img.sprite.rect.width;
            float h = img.sprite.rect.height;
            return new Vector3(w, h, 0);
        }

        void CreateHeader(Transform parent, Change type)
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

        void CreateText(Transform parent, Change type)
        {
            // DECIDE THINGS.
            Dictionary<Change, float> GetY = new Dictionary<Change, float>()
            {
                { Change.Weapon,   189f  },
                { Change.Trait,    20f },
                { Change.Curse,   -314f }
            };

            float y = GetY[type];

            GameObject textBox = new GameObject();
            textBox.name = "UIText_" + type.ToString();
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

        void CreateArrows(Transform parent, Change type)
        {
            // DECIDE THINGS.
            Dictionary<Change, float> GetY = new Dictionary<Change, float>()
            {
                { Change.Weapon,  193f  },
                { Change.Trait,   30f },
                { Change.Curse,   -304f }
            };

            float y = GetY[type];

            // Left arrow
            GameObject leftArrow = new GameObject();
            leftArrow.name = "LeftArrow_" + type.ToString();
            leftArrow.layer = Layer;
            leftArrow.transform.SetParent(parent);
            leftArrow.transform.localPosition = new Vector3(-360f, y, 0);
            leftArrow.transform.localScale = new Vector3(1.2f, 1.2f, 0);

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
            rightArrow.name = "RightArrow_" + type.ToString();
            rightArrow.layer = Layer;
            rightArrow.transform.SetParent(parent);
            rightArrow.transform.localPosition = new Vector3(360, y, 0);
            rightArrow.transform.localScale = new Vector3(1.2f, 1.2f, 0);

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

        string GetText(Change type)
        {
            switch (type)
            {
                case Change.Weapon:
                    return library.WeaponNames[WeaponPatches.Weapon];
                case Change.Trait:
                    return library.TraitNames[WeaponPatches.Trait];
                case Change.Curse:
                    return library.CurseNames[WeaponPatches.Curse];
            }
            return "??";
        }

        public void UpdateText(Change type)
        {
            // TODO: Make this dynamically display something else and not just weapon names
            switch (type)
            {
                case Change.Weapon:
                    TextObjects[type].tmp.text = library.WeaponNames[WeaponPatches.Weapon];
                    break;
                case Change.Trait:
                    TextObjects[type].tmp.text = library.TraitNames[WeaponPatches.Trait];
                    break;
                case Change.Curse:
                    TextObjects[type].tmp.text = library.CurseNames[WeaponPatches.Curse];
                    break;
            }
        }
    }
    
}
