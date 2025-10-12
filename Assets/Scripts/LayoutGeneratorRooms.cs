using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Random = System.Random;

public class LayoutGeneratorRooms : MonoBehaviour
{
    [SerializeField] private int m_seed = Environment.TickCount;
    [SerializeField] private int m_width = 64;
    [SerializeField] private int m_length = 64;
    [SerializeField] private int m_minRoomWidth = 3;
    [SerializeField] private int m_maxRoomWidth = 10;
    [SerializeField] private int m_minRoomLength = 3;
    [SerializeField] private int m_maxRoomLength = 10;
    [SerializeField] private int m_doorDistanceFromEdge = 1;
    [SerializeField] private int m_minCorridorLength = 3;
    [SerializeField] private int m_maxCorridorLength = 5;
    [SerializeField] private int m_maxRoomCount = 15;
    [SerializeField] private int m_minRoomDistance = 1;
    [SerializeField] private GameObject m_levelLayoutDisplay;
    [SerializeField] private List<Hallway> m_openDoorways;

    private Random m_random;
    private Level m_level;

    [ContextMenu("Generate Level Layout")]
    public void GenerateLevel()
    {
        m_random = new Random(m_seed);
        m_openDoorways = new List<Hallway>();
        m_level = new Level(m_width, m_length);
        var roomRect = GetStartRoomRect();
        Room room = new Room(roomRect);
        List<Hallway> hallways = room.CalculateAllPossibleDoorways(room.Area.width, room.Area.height, m_doorDistanceFromEdge);
        hallways.ForEach(h => h.StartRoom = room);
        hallways.ForEach(h => m_openDoorways.Add(h));
        m_level.AddRoom(room);

        Hallway selectedEntryway = m_openDoorways[m_random.Next(m_openDoorways.Count)];
        AddRooms();

        DrawLayout(selectedEntryway, roomRect);
    }

    [ContextMenu("Generate new Seed")]
    public void GenerateNewSeed()
    {
        m_seed = Environment.TickCount;
    }

    [ContextMenu("Generate new Seed and Level")]
    public void GenerateNewSeedAndLevel()
    {
        GenerateNewSeed();
        GenerateLevel();
    }

    private RectInt GetStartRoomRect()
    {
        int roomWidth = m_random.Next(m_minRoomWidth, m_maxRoomWidth);
        int availableWidthX = m_width / 2 - roomWidth;
        int randomX = m_random.Next(0, availableWidthX);
        int roomX = randomX + (m_width / 4);

        int roomLength = m_random.Next(m_minRoomLength, m_maxRoomLength);
        int availableLengthY = m_length / 2 - roomLength;
        int randomY = m_random.Next(0, availableLengthY);
        int roomY = randomY + (m_length / 4);

        return new RectInt(roomX, roomY, roomWidth, roomLength);
    }

    private void DrawLayout(Hallway selectedEntryway = null, RectInt roomCandidateRect = new RectInt(), bool isDebug = false)
    {
        var renderer = m_levelLayoutDisplay.GetComponent<Renderer>();
        var layoutTexture = (Texture2D)renderer.sharedMaterial.mainTexture;

        layoutTexture.Reinitialize(m_width, m_length);
        m_levelLayoutDisplay.transform.localScale = new Vector3(m_width, m_length, 1);
        layoutTexture.FillWithColor(Color.black);

        Array.ForEach(m_level.Rooms, room => layoutTexture.DrawRectangle(room.Area, Color.white));
        Array.ForEach(m_level.Hallways, hallway => layoutTexture.DrawLine(hallway.StartPositionAbsolute, hallway.EndPositionAbsolute, Color.white));

        if (isDebug)
        {
            layoutTexture.DrawRectangle(roomCandidateRect, Color.white);
            m_openDoorways.ForEach(doorway => layoutTexture.SetPixel(doorway.StartPositionAbsolute.x, doorway.StartPositionAbsolute.y, doorway.StartDirection.GetColor()));
        }

        if (selectedEntryway != null && isDebug)
        {
            layoutTexture.SetPixel(selectedEntryway.StartPositionAbsolute.x, selectedEntryway.StartPositionAbsolute.y, Color.cyan);
        }

        layoutTexture.SaveAsset();
    }

    private Hallway SelectHallwayCandidate(RectInt roomCandidateRect, Hallway entryway)
    {
        Room room = new Room(roomCandidateRect);
        List<Hallway> hallwayCandidates = room.CalculateAllPossibleDoorways(room.Area.width, room.Area.height, m_doorDistanceFromEdge);
        HallwayDirection requiredDirection = entryway.StartDirection.GetOppositeDirection();
        List<Hallway> filteredHallwayCandidates = hallwayCandidates.Where(hallwayCandidate => hallwayCandidate.StartDirection == requiredDirection).ToList();
        return filteredHallwayCandidates.Count > 0 ? filteredHallwayCandidates[m_random.Next(filteredHallwayCandidates.Count)] : null;
    }

    private Vector2Int CalculateRoomPosition(Hallway entryway, int roomWidth, int roomLength, int distance, Vector2Int endPosition)
    {
        Vector2Int roomPosition = entryway.StartPositionAbsolute;
        switch (entryway.StartDirection)
        {
            case HallwayDirection.Left:
                roomPosition.x -= distance + roomWidth;
                roomPosition.y -= endPosition.y;
                break;
            case HallwayDirection.Top:
                roomPosition.x -= endPosition.x;
                roomPosition.y = distance + 1;
                break;
            case HallwayDirection.Right:
                roomPosition.x += distance + 1;
                roomPosition.y -= endPosition.y;
                break;
            case HallwayDirection.Bottom:
                roomPosition.x -= endPosition.x;
                roomPosition.y -= distance + roomLength;
                break;
        }
        return roomPosition;
    }

    private Room ConstructAdjacentRoom(Hallway selectedEntryway)
    {
        RectInt roomCandidateRect = new RectInt
        {
            width = m_random.Next(m_minRoomWidth, m_maxRoomWidth),
            height = m_random.Next(m_minRoomLength, m_maxRoomLength)
        };
        Hallway selectedExit = SelectHallwayCandidate(roomCandidateRect, selectedEntryway);
        if (selectedExit == null) { return null; }

        int distance = m_random.Next(m_minCorridorLength, m_maxCorridorLength + 1);
        Vector2Int roomCandidatePosition = CalculateRoomPosition(selectedEntryway, roomCandidateRect.width, roomCandidateRect.height, distance, selectedExit.StartPosition);
        roomCandidateRect.position = roomCandidatePosition;

        if (!IsRoomCandidateValid(roomCandidateRect)) { return null; }

        Room newRoom = new Room(roomCandidateRect);
        selectedEntryway.EndRoom = newRoom;
        selectedEntryway.EndPosition = selectedExit.StartPosition;
        return newRoom;
    }

    private void AddRooms()
    {
        while (m_openDoorways.Count > 0 && m_level.Rooms.Length < m_maxRoomCount)
        {
            Hallway selectedEntryway = m_openDoorways[m_random.Next(0, m_openDoorways.Count)];
            Room newRoom = ConstructAdjacentRoom(selectedEntryway);

            if (newRoom == null)
            {
                m_openDoorways.Remove(selectedEntryway);
                continue;
            }
            m_level.AddRoom(newRoom);
            m_level.AddHallway(selectedEntryway);

            selectedEntryway.EndRoom = newRoom;
            List<Hallway> newOpenHallways = newRoom.CalculateAllPossibleDoorways(newRoom.Area.width, newRoom.Area.height, m_doorDistanceFromEdge);
            newOpenHallways.ForEach(h => h.StartRoom = newRoom);

            m_openDoorways.Remove(selectedEntryway);
            m_openDoorways.AddRange(newOpenHallways);
        }
    }

    private bool IsRoomCandidateValid(RectInt roomCandidateRect)
    {
        RectInt levelRect = new RectInt(1, 1, m_width - 2, m_length - 2);
        return levelRect.Contains(roomCandidateRect)
                && !IsRoomOverlapping(roomCandidateRect, m_level.Rooms, m_level.Hallways, m_minRoomDistance)
                && !IsHallwayOverlapping(roomCandidateRect, m_level.Hallways);
    }

    private bool IsRoomOverlapping(RectInt roomCandidateRect, Room[] existingRooms, Hallway[] existingHallways, int minRoomDistance)
    {
        RectInt paddedRoomRect = new RectInt
        {
            x = roomCandidateRect.x - minRoomDistance,
            y = roomCandidateRect.y - minRoomDistance,
            width = roomCandidateRect.width + 2 * minRoomDistance,
            height = roomCandidateRect.height + 2 * minRoomDistance
        };
        foreach (Room room in existingRooms)
        {
            if (paddedRoomRect.Overlaps(room.Area))
            {
                return true;
            }
        }
        foreach (Hallway hallway in existingHallways)
        {
            if (paddedRoomRect.Overlaps(hallway.Area))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsHallwayOverlapping(RectInt roomCandidateRect, Hallway[] existingHallways)
    {
        foreach (Hallway hallway in existingHallways)
        {
            if (roomCandidateRect.Overlaps(hallway.Area))
            {
                return true;
            }
        }
        return false;
    }
}
