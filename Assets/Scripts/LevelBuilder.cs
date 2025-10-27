using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private LayoutGeneratorRooms m_layoutGenerator;
    [SerializeField] private MarchingSquares m_marchingSquares;

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
    }
}
