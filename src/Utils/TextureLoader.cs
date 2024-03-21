using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace WeaponSelector.Utils;

public static class TextureLoader
{
    private static readonly FilterMode filterMode = FilterMode.Bilinear;

    public static Texture2D TextureFromBytes(byte[] array, float opacity = 0)
    {
        var texture = new Texture2D(1, 1);
        texture.filterMode = filterMode;
        ImageConversion.LoadImage(texture, array);

        if (opacity > 0 && opacity <= 1) {
            texture = ChangeOpacity(texture, opacity);
        }
        return texture;
    }

    public static void ChangeOpacity (Image image, float opacity)
    {
        Color color = image.color;
        color.a = opacity;
        image.color = color;
    }

    public static Texture2D ChangeOpacity(Texture2D texture, float opacity)
    {
        for (int y = 0; y < texture.height; y++)    // go through the y-axis.
        {
            for (int x = 0; x < texture.width; x++) // go through the x-axis.
            {
                Color32 color = new Color(
                    texture.GetPixel(x, y).r,
                    texture.GetPixel(x, y).g,
                    texture.GetPixel(x, y).b,
                    opacity
                );
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    public static Sprite SpriteFromTexture(Texture2D texture)
    {
        Rect spriteRect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(texture, spriteRect, pivot);
    }

    public static Sprite MakeSprite(byte[] image, float opacity = 0)
        => SpriteFromTexture(TextureFromBytes(image, opacity));
}
