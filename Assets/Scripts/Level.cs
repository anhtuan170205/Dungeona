using UnityEngine;
using System.Collections.Generic;

public class Level
{
    private int m_width;
    private int m_length;
    private List<Hallway> m_hallways;
    private List<Room> m_rooms;

    public int Width { get { return m_width; } }
    public int Length { get { return m_length; } }

    public Level(int width, int length)
    {
        m_width = width;
        m_length = length;
        m_hallways = new List<Hallway>();
        m_rooms = new List<Room>();
    }
}
