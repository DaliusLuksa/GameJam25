using UnityEngine;
using UnityEngine.UIElements;

public class SpriteCombiner
{
    public static Sprite InsertSprites(Sprite sprite1, Sprite sprite2, Vector2 offset, float scale, Color innerColor)
    {
        return MergeSprites(sprite1, sprite2, offset, scale, innerColor, Vector2.zero, 1f, Color.white);
    }
    public static Sprite MergeSprites(Sprite sprite1, Sprite sprite2, Vector2 offset1, float scale1, Color innerColor1
        , Vector2 offset2, float scale2, Color innerColor2)
    {
        // Get the dimensions of the combined texture
        int width = Mathf.Max(sprite1.texture.width, sprite2.texture.width);
        int height = Mathf.Max(sprite1.texture.height, sprite2.texture.height);

        // Create a new texture for the combined result
        Texture2D combinedTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        combinedTexture.filterMode = FilterMode.Point;

        // Fill the texture with transparency
        Color[] clearPixels = new Color[width * height];
        for (int i = 0; i < clearPixels.Length; i++)
            clearPixels[i] = new Color(0, 0, 0, 0);
        combinedTexture.SetPixels(clearPixels);

        // Add the first sprite to the texture
        AddSpriteToTexture(sprite1, combinedTexture, offset2, scale2, innerColor2);

        // Add the second sprite with offset, scale, and color adjustments
        AddSpriteToTexture(sprite2, combinedTexture, offset1, scale1, innerColor1);

        // Apply changes to the texture
        combinedTexture.Apply();

        // Create and return a new sprite from the combined texture
        return Sprite.Create(combinedTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 16f); //pixels per unit 16f, change later if more pixels

    }

    private static void AddSpriteToTexture(Sprite sprite, Texture2D targetTexture, Vector2 offset, float scale, Color color)
    {
        // Get the sprite's texture and pixels
        Texture2D sourceTexture = sprite.texture;
        Rect spriteRect = sprite.rect;

        // Extract sprite pixels
        Color[] pixels = sourceTexture.GetPixels((int)spriteRect.x, (int)spriteRect.y, (int)spriteRect.width, (int)spriteRect.height);

        // Determine where to place the sprite in the target texture
        int targetX = Mathf.RoundToInt((targetTexture.width - spriteRect.width * scale) / 2 + offset.x);
        int targetY = Mathf.RoundToInt((targetTexture.height - spriteRect.height * scale) / 2 + offset.y);

        // Scale and blend the pixels into the target texture
        for (int y = 0; y < spriteRect.height; y++)
        {
            for (int x = 0; x < spriteRect.width; x++)
            {
                int scaledX = Mathf.RoundToInt(x * scale);
                int scaledY = Mathf.RoundToInt(y * scale);

                int targetPixelX = targetX + scaledX;
                int targetPixelY = targetY + scaledY;

                if (targetPixelX >= 0 && targetPixelX < targetTexture.width &&
                    targetPixelY >= 0 && targetPixelY < targetTexture.height)
                {
                    Color sourceColor = pixels[x + y * (int)spriteRect.width] * color;
                    Color targetColor = targetTexture.GetPixel(targetPixelX, targetPixelY);

                    // Alpha blending
                    Color blendedColor = Color.Lerp(targetColor, sourceColor, sourceColor.a);
                    targetTexture.SetPixel(targetPixelX, targetPixelY, blendedColor);
                }
            }
        }
    }
}
