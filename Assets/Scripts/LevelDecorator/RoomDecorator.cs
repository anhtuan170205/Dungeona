using UnityEngine;
using Random = System.Random;

public class RoomDecorator : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private LayoutGeneratorRooms _layoutGenerator;

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
        GameObject testGameObject = new GameObject("TestObject");
        testGameObject.transform.SetParent(decorationsTransform);
    }
}
