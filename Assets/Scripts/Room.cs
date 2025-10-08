using UnityEngine;
using System.Collections.Generic;

public class Room
{
    private RectInt m_area;
    public RectInt Area { get { return m_area; } }

    public Room(RectInt area)
    {
        m_area = area;
    }

    public List<Hallway> CalculateAllPossibleDoorways(int width, int length, int minDistanceFromEdge)
    {
        List<Hallway> hallwayCandidates = new List<Hallway>();
        hallwayCandidates.Add(new Hallway(new Vector2Int(0, 0)));
        hallwayCandidates.Add(new Hallway(new Vector2Int(width, length)));
        return hallwayCandidates;
    }
}
