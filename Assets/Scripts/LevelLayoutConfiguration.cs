using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Custom/Procedural Generation/Level Layout Configuration")]
public class LevelLayoutConfiguration : ScriptableObject
{
    [SerializeField] private int m_width = 64;
    [SerializeField] private int m_length = 64;
    [SerializeField] private int m_minRoomWidth = 3;
    [SerializeField] private int m_maxRoomWidth = 10;
    [SerializeField] private int m_minRoomLength = 3;
    [SerializeField] private int m_maxRoomLength = 10;
    [SerializeField] private int m_doorDistanceFromEdge = 1;
    [SerializeField] private int m_minHallwayLength = 3;
    [SerializeField] private int m_maxHallwayLength = 5;
    [SerializeField] private int m_maxRoomCount = 15;
    [SerializeField] private int m_minRoomDistance = 1;

    public int Width => m_width;
    public int Length => m_length;
    public int MinRoomWidth => m_minRoomWidth;
    public int MaxRoomWidth => m_maxRoomWidth;
    public int MinRoomLength => m_minRoomLength;
    public int MaxRoomLength => m_maxRoomLength;
    public int DoorDistanceFromEdge => m_doorDistanceFromEdge;
    public int MinHallwayLength => m_minHallwayLength;
    public int MaxHallwayLength => m_maxHallwayLength;
    public int MaxRoomCount => m_maxRoomCount;
    public int MinRoomDistance => m_minRoomDistance;
}
