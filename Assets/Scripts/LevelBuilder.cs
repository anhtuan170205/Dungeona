using UnityEngine;
using Unity.AI.Navigation;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private LayoutGeneratorRooms m_layoutGenerator;
    [SerializeField] private MarchingSquares m_marchingSquares;
    [SerializeField] private NavMeshSurface m_navMeshSurface;

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
        Level level = m_layoutGenerator.GenerateLevel();
        m_marchingSquares.CreateLevelGeometry();
        m_navMeshSurface.BuildNavMesh();

        Room startRoom = level.StartRoom;
        Vector2 roomCenter = startRoom.Area.center;
        Vector3 playerStartPosition = LevelToWorldPosition(roomCenter);

        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = playerStartPosition;
    }
    
    public Vector3 LevelToWorldPosition(Vector2 levelPosition)
    {
        int scale = SharedLevelData.Instance.Scale;
        return new Vector3((levelPosition.x - 1) * scale, 0, (levelPosition.y - 1) * scale);
    }
}
