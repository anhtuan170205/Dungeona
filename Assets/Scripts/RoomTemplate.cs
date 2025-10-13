using UnityEngine;
using System;

[Serializable]
public class RoomTemplate
{
    [SerializeField] string m_name;
    [SerializeField] private int m_numberOfRooms;
    [SerializeField] private int m_minRoomWidth = 3;
    [SerializeField] private int m_maxRoomWidth = 10;
    [SerializeField] private int m_minRoomLength = 3;
    [SerializeField] private int m_maxRoomLength = 10;

    public int NumberOfRooms => m_numberOfRooms;
    public int MinRoomWidth => m_minRoomWidth;
    public int MaxRoomWidth => m_maxRoomWidth;
    public int MinRoomLength => m_minRoomLength;
    public int MaxRoomLength => m_maxRoomLength;
}
