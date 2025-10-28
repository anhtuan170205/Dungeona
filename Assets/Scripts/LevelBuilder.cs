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
        m_layoutGenerator.GenerateLevel();
        m_marchingSquares.CreateLevelGeometry();
        m_navMeshSurface.BuildNavMesh();
    }
}
