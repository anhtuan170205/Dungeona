using UnityEngine;
using System.Collections.Generic;
using System;

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

        Room testRoom1 = new Room(new RectInt(3, 6, 6, 10));
        Room testRoom2 = new Room(new RectInt(15, 4, 10, 12));
        Hallway testHallway = new Hallway(HallwayDirection.Right, new Vector2Int(6, 3), testRoom1);
        testHallway.EndPosition = new Vector2Int(0, 5);
        testHallway.EndRoom = testRoom2;
        m_level.AddRoom(testRoom1);
        m_level.AddRoom(testRoom2);
        m_level.AddHallway(testHallway);

        DrawLayout(roomRect);
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

    private void DrawLayout(RectInt roomCandidateRect = new RectInt())
    {
        var renderer = m_levelLayoutDisplay.GetComponent<Renderer>();
        var layoutTexture = (Texture2D)renderer.sharedMaterial.mainTexture;

        layoutTexture.Reinitialize(m_width, m_length);
        m_levelLayoutDisplay.transform.localScale = new Vector3(m_width, m_length, 1);
        layoutTexture.FillWithColor(Color.black);

        Array.ForEach(m_level.Rooms, room => layoutTexture.DrawRectangle(room.Area, Color.white));
        Array.ForEach(m_level.Hallways, hallway => layoutTexture.DrawLine(hallway.StartPositionAbsolute, hallway.EndPositionAbsolute, Color.white));

        layoutTexture.DrawRectangle(roomCandidateRect, Color.white);

        foreach (Hallway hallway in m_openDoorways)
        {
            layoutTexture.SetPixel(hallway.StartPositionAbsolute.x, hallway.StartPositionAbsolute.y, hallway.StartDirection.GetColor());
        }

        layoutTexture.SaveAsset();
    }
}
