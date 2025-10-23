using UnityEngine;

[CreateAssetMenu(fileName = "New Tileset", menuName = "Custom/Procedural Generation/Tileset")]
public class Tileset : ScriptableObject
{
    [SerializeField] Color wallColor;
    [SerializeField] GameObject[] tiles = new GameObject[16];

    public Color WallColor => wallColor;

    public GameObject GetTile(int tileIndex)
    {
        if (tileIndex < 0 || tileIndex >= tiles.Length) { return null; }
        return tiles[tileIndex];
    }
}
