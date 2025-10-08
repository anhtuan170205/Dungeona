using UnityEngine;

public class Room
{
    private RectInt m_area;
    public RectInt Area { get { return m_area; } }

    public Room(RectInt area)
    {
        m_area = area;
    }
}
