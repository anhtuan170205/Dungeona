using UnityEngine;
using System.Collections.Generic;

public class Level
{
    private int m_width;
    private int m_length;
    private List<Hallway> m_hallways;
    private List<Room> m_rooms;

    public int Width => m_width;
    public int Length => m_length;

    public Room[] Rooms => m_rooms.ToArray();
    public Hallway[] Hallways => m_hallways.ToArray();

    public Level(int width, int length)
    {
        m_width = width;
        m_length = length;
        m_hallways = new List<Hallway>();
        m_rooms = new List<Room>();
    }

    public void AddRoom(Room newRoom) => m_rooms.Add(newRoom);
    public void AddHallway(Hallway newHallway) => m_hallways.Add(newHallway);
}
