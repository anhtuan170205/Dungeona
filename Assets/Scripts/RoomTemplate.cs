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
    [SerializeField] private Texture2D m_layoutTexture;

    public int NumberOfRooms => m_numberOfRooms;
    public int MinRoomWidth => m_minRoomWidth;
    public int MaxRoomWidth => m_maxRoomWidth;
    public int MinRoomLength => m_minRoomLength;
    public int MaxRoomLength => m_maxRoomLength;
    public Texture2D LayoutTexture => m_layoutTexture;

    public RectInt GenerateRoomCandidateRect(System.Random random)
    {
        if (m_layoutTexture != null)
        {
            return new RectInt
            {
                width = m_layoutTexture.width,
                height = m_layoutTexture.height
            };
        }
        RectInt roomCandidateRect = new RectInt
        {
            width = random.Next(m_minRoomWidth, m_maxRoomWidth),
            height = random.Next(m_minRoomLength, m_maxRoomLength)
        };

        return roomCandidateRect;
    }
}
