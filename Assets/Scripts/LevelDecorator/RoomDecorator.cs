using UnityEngine;
using Random = System.Random;

public class RoomDecorator : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private LayoutGeneratorRooms _layoutGenerator;
    [SerializeField] private Texture2D _levelTexture;
    [SerializeField] private Texture2D _decoratedTexture;

    private Random _random;

    [ContextMenu("Place Items")]
    public void PlaceItemsFromMenu()
    {
        SharedLevelData.Instance.ResetRandom();
        Level level = _layoutGenerator.GenerateLevel();
        PlaceItems(level);
    }

    public void PlaceItems(Level level)
    {
        _random = SharedLevelData.Instance.Rand;
        Transform decorationsTransform = _parent.transform.Find("Decorations");

        if (decorationsTransform == null)
        {
            GameObject decorationsGameObject = new GameObject("Decorations");
            decorationsTransform = decorationsGameObject.transform;
            decorationsTransform.SetParent(_parent.transform);
        }
        else
        {
            decorationsTransform.DestroyAllChildren();
        }

        TileType[,] levelDecorated = InitializeDecoratorArray(level);
        GenerateTextureFromTileType(levelDecorated);
    }

    private TileType[,] InitializeDecoratorArray(Level level)
    {
        TextureBasedLevel textureBasedLevel = new TextureBasedLevel(_levelTexture);
        TileType[,] levelDecorated = new TileType[level.Width, level.Length];

        for (int y = 0; y < _levelTexture.height; y++)
        {
            for (int x = 0; x < _levelTexture.width; x++)
            {
                bool isBlocked = textureBasedLevel.IsBlocked(x, y);
                if (isBlocked)
                {
                    levelDecorated[x, y] = TileType.Wall;
                }
                else
                {
                    levelDecorated[x, y] = TileType.Floor;
                }
            }
        }
        return levelDecorated;
    }

    private void GenerateTextureFromTileType(TileType[,] tileTypes)
    {
        int width = tileTypes.GetLength(0);
        int height = tileTypes.GetLength(1);

        Color32[] pixels = new Color32[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                pixels[y * width + x] = tileTypes[x, y].GetColor();
            }
        }

        _decoratedTexture.Reinitialize(width, height);
        _decoratedTexture.SetPixels32(pixels);
        _decoratedTexture.Apply();
        _decoratedTexture.SaveAsset();
    }
}
