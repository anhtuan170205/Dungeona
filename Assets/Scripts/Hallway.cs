using UnityEngine;

public class Hallway
{
    private Vector2Int m_startPosition;
    private Vector2Int m_endPosition;

    private Room m_startRoom;
    private Room m_endRoom;

    public Room StartRoom { get { return m_startRoom; } set { m_startRoom = value; } }
    public Room EndRoom { get { return m_endRoom; } set { m_endRoom = value; } }

    public Vector2Int StartPositionAbsolute { get { return m_startPosition + m_startRoom.Area.position; } }
    public Vector2Int EndPositionAbsolute { get { return m_endPosition + m_endRoom.Area.position; } }

    public Hallway(Vector2Int startPosition, Room startRoom = null)
    {
        m_startPosition = startPosition;
        m_startRoom = startRoom;
    }

}
