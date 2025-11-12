using UnityEngine;
using Unity.AI.Navigation;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private LayoutGeneratorRooms _layoutGenerator;
    [SerializeField] private MarchingSquares _marchingSquares;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private RoomDecorator _roomDecorator;

    private void Start()
    {
        BuildRandomLevel();
    }

    [ContextMenu("Build Random Level")]
    public void BuildRandomLevel()
    {
        SharedLevelData.Instance.GenerateSeed();
        BuildLevel();
    }

    [ContextMenu("Build Level")]
    public void BuildLevel()
    {
        Level level = _layoutGenerator.GenerateLevel();
        _marchingSquares.CreateLevelGeometry();
        _roomDecorator.PlaceItems(level);
        _navMeshSurface.BuildNavMesh();

        Room startRoom = level.StartRoom;
        Vector2 roomCenter = startRoom.Area.center;
        Vector3 playerStartPosition = LevelToWorldPosition(roomCenter);

        GameObject player = GameObject.FindWithTag("Player");
        UnityEngine.AI.NavMeshAgent playerNavMeshAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (playerNavMeshAgent == null)
        {
            player.transform.position = playerStartPosition;
        }
        else
        {
            playerNavMeshAgent.Warp(playerStartPosition);
        }

    }
    
    public Vector3 LevelToWorldPosition(Vector2 levelPosition)
    {
        int scale = SharedLevelData.Instance.Scale;
        return new Vector3((levelPosition.x - 1) * scale, 0, (levelPosition.y - 1) * scale);
    }
}
