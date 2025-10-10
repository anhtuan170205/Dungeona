using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class LayoutGeneratorRooms : MonoBehaviour
{
    [SerializeField] private int m_width = 64;
    [SerializeField] private int m_length = 64;
    [SerializeField] private int m_minRoomWidth = 3;
    [SerializeField] private int m_maxRoomWidth = 10;
    [SerializeField] private int m_minRoomLength = 3;
    [SerializeField] private int m_maxRoomLength = 10;
    [SerializeField] private GameObject m_levelLayoutDisplay;
    [SerializeField] private List<Hallway> m_openDoorways;

    private System.Random m_random;
    private Level m_level;

    [ContextMenu("Generate Level Layout")]
    public void GenerateLevel()
    {
        m_random = new System.Random();
        m_openDoorways = new List<Hallway>();
        m_level = new Level(m_width, m_length);
        var roomRect = GetStartRoomRect();
        Debug.Log(roomRect);
        Room room = new Room(roomRect);
        List<Hallway> hallways = room.CalculateAllPossibleDoorways(room.Area.width, room.Area.height, 1);
        hallways.ForEach(h => h.StartRoom = room);
        hallways.ForEach(h => m_openDoorways.Add(h));
        m_level.AddRoom(room);

        Hallway selectedEntryway = m_openDoorways[m_random.Next(m_openDoorways.Count)];
        Hallway selectedExit = SelectHallwayCandidate(new RectInt(0, 0, 5, 7), selectedEntryway);
        Debug.Log(selectedExit.StartPosition);
        Debug.Log(selectedExit.StartDirection);

        DrawLayout(selectedEntryway ,roomRect);
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

    private void DrawLayout(Hallway selectedEntryway = null, RectInt roomCandidateRect = new RectInt())
    {
        var renderer = m_levelLayoutDisplay.GetComponent<Renderer>();
        var layoutTexture = (Texture2D)renderer.sharedMaterial.mainTexture;

        layoutTexture.Reinitialize(m_width, m_length);
        m_levelLayoutDisplay.transform.localScale = new Vector3(m_width, m_length, 1);
        layoutTexture.FillWithColor(Color.black);

        Array.ForEach(m_level.Rooms, room => layoutTexture.DrawRectangle(room.Area, Color.white));
        Array.ForEach(m_level.Hallways, hallway => layoutTexture.DrawLine(hallway.StartPositionAbsolute, hallway.EndPositionAbsolute, Color.white));

        layoutTexture.DrawRectangle(roomCandidateRect, Color.white);

        m_openDoorways.ForEach(doorway => layoutTexture.SetPixel(doorway.StartPositionAbsolute.x, doorway.StartPositionAbsolute.y, doorway.StartDirection.GetColor()));

        if (selectedEntryway != null)
        {
            layoutTexture.SetPixel(selectedEntryway.StartPositionAbsolute.x, selectedEntryway.StartPositionAbsolute.y, Color.cyan);
        }

        layoutTexture.SaveAsset();
    }

    private Hallway SelectHallwayCandidate(RectInt roomCandidateRect, Hallway entryway)
    {
        Room room = new Room(roomCandidateRect);
        List<Hallway> hallwayCandidates = room.CalculateAllPossibleDoorways(room.Area.width, room.Area.height, 1);
        HallwayDirection requiredDirection = entryway.StartDirection.GetOppositeDirection();
        List<Hallway> filteredHallwayCandidates = hallwayCandidates.Where(hallwayCandidate => hallwayCandidate.StartDirection == requiredDirection).ToList();
        return filteredHallwayCandidates.Count > 0 ? filteredHallwayCandidates[m_random.Next(filteredHallwayCandidates.Count)] : null;
    }
}
