using UnityEngine;

public class MarchingSquares : MonoBehaviour
{
    [SerializeField] private Texture2D m_levelTexture;
    [ContextMenu("Create Level Geometry")]
    public void CreateLevelGeometry()
    {
        TextureBasedLevel level = new TextureBasedLevel(m_levelTexture);
        for (int y = 0; y < level.Length - 1; y++)
        {
            string row = "";
            for (int x = 0; x < level.Width - 1; x++)
            {
                row += level.IsBlocked(x, y) ? "0" : "";
            }
            Debug.Log(row);
        }
    }
}
