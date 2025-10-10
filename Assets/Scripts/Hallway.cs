using UnityEngine;

public class Hallway
{
    private Vector2Int m_startPosition;
    private Vector2Int m_endPosition;

    private HallwayDirection m_startDirection;
    private HallwayDirection m_endDirection;

    private Room m_startRoom;
    private Room m_endRoom;

    public Room StartRoom
    {
        get => m_startRoom;
        set => m_startRoom = value;
    }    
    public Room EndRoom
    {
        get => m_endRoom;
        set => m_endRoom = value;
    }

    public Vector2Int StartPositionAbsolute => m_startPosition + m_startRoom.Area.position;
    public Vector2Int EndPositionAbsolute => m_endPosition + m_endRoom.Area.position;

    public HallwayDirection StartDirection => m_startDirection;
    public HallwayDirection EndDirection
    {
        get => m_endDirection;
        set => m_endDirection = value;
    }

    public Vector2Int StartPosition
    {
        get => m_startPosition;
        set => m_startPosition = value;
    }

    public Vector2Int EndPosition
    {
        get => m_endPosition;
        set => m_endPosition = value;
    }

    public Hallway(HallwayDirection startDirection, Vector2Int startPosition, Room startRoom = null)
    {
        m_startDirection = startDirection;
        m_startPosition = startPosition;
        m_startRoom = startRoom;
    }

}
