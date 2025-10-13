using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "Level_", menuName = "Custom/Procedural Generation/Level Layout Configuration")]
public class LevelLayoutConfigurationSO : ScriptableObject
{
    [SerializeField] private int m_width = 64;
    [SerializeField] private int m_length = 64;
    [SerializeField] private RoomTemplate[] m_roomTemplates;
    [SerializeField] private int m_doorDistanceFromEdge = 1;
    [SerializeField] private int m_minHallwayLength = 3;
    [SerializeField] private int m_maxHallwayLength = 5;
    [SerializeField] private int m_maxRoomCount = 15;
    [SerializeField] private int m_minRoomDistance = 1;

    public int Width => m_width;
    public int Length => m_length;
    public RoomTemplate[] RoomTemplates => m_roomTemplates;
    public int DoorDistanceFromEdge => m_doorDistanceFromEdge;
    public int MinHallwayLength => m_minHallwayLength;
    public int MaxHallwayLength => m_maxHallwayLength;
    public int MaxRoomCount => m_maxRoomCount;
    public int MinRoomDistance => m_minRoomDistance;

    public Dictionary<RoomTemplate, int> GetAvailableRooms()
    {
        Dictionary<RoomTemplate, int> availableRooms = new Dictionary<RoomTemplate, int>();
        for (int i = 0; i < m_roomTemplates.Length; i++)
        {
            availableRooms.Add(m_roomTemplates[i], m_roomTemplates[i].NumberOfRooms);
        }
        availableRooms = availableRooms.Where(kpv => kpv.Value > 0).ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
        return availableRooms;
    }
}
