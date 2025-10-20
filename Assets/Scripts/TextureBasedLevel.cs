using UnityEngine;

public class TextureBasedLevel : ILevel
{
    Texture2D m_levelTexture;
    public TextureBasedLevel(Texture2D levelTexture)
    {
        m_levelTexture = levelTexture;
    }

    public int Length => m_levelTexture.height;
    public int Width => m_levelTexture.width;

    public bool IsBlocked(int x, int y)
    {
        if (x < 0 || y < 0 || x >= m_levelTexture.width || y >= m_levelTexture.height)
        {
            return true;
        }
        Color pixel = m_levelTexture.GetPixel(x, y);
        return Color.black.Equals(pixel) ? true  : false;
    }

    public int Floor(int x, int y)
    {
        return 0;
    }
}
