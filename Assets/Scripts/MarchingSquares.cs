using UnityEngine;

public class MarchingSquares : MonoBehaviour
{
    [SerializeField] private Texture2D m_levelTexture;
    [SerializeField] private GameObject m_generatedLevel;
    [SerializeField] private Tileset m_tileset;

    [ContextMenu("Create Level Geometry")]
    public void CreateLevelGeometry()
    {
        m_generatedLevel.transform.DestroyAllChildren();
        int scale = SharedLevelData.Instance.Scale;
        Vector3 scaleVector = new Vector3(scale, scale, scale);
        TextureBasedLevel level = new TextureBasedLevel(m_levelTexture);
        for (int y = 0; y < level.Length - 1; y++)
        {
            for (int x = 0; x < level.Width - 1; x++)
            {
                int tileIndex = CalculateTileIndex(level, x, y);
                GameObject tilePrefab = m_tileset.GetTile(tileIndex);
                if (tilePrefab == null) { continue; }
                GameObject tileInstance = Instantiate(tilePrefab, m_generatedLevel.transform);
                tileInstance.transform.localScale = scaleVector;
                tileInstance.transform.position = new Vector3(x * scale, 0, y * scale);
                string tileName = "x " + x + " y " + y + " (Index " + tileIndex + ")";
                tileInstance.name = tileName;
            }
        }
    }

    private int CalculateTileIndex(ILevel level, int x, int y)
    {
        int topLeft = level.IsBlocked(x, y + 1) ? 1 : 0;
        int topRight = level.IsBlocked(x + 1, y + 1) ? 1 : 0;
        int bottomLeft = level.IsBlocked(x, y) ? 1 : 0;
        int bottomRight = level.IsBlocked(x + 1, y) ? 1 : 0;
        int tileIndex = topLeft + (topRight << 1) + (bottomLeft << 2) + (bottomRight << 3);
        return tileIndex;
    }
}
