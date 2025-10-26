using UnityEngine;

[CreateAssetMenu(fileName = "New Tileset", menuName = "Custom/Procedural Generation/Tileset")]
public class Tileset : ScriptableObject
{
    [SerializeField] Color wallColor;
    [SerializeField] TileVariant[] tiles = new TileVariant[16];

    public Color WallColor => wallColor;

    public GameObject GetTile(int tileIndex)
    {
        if (tileIndex < 0 || tileIndex >= tiles.Length) { return null; }
        var tile = tiles[tileIndex].GetRandomTile();
        if (tile == null)
        {
            Debug.LogWarning("Tile variant not found for index: " + tileIndex, this);
        }
        return tile;
    }
}
