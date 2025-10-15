using UnityEngine;
using System.Collections.Generic;

public class Room
{
    private RectInt m_area;
    public RectInt Area => m_area;
    public Texture2D LayoutTexture { get; }

    public Room(RectInt area)
    {
        m_area = area;
    }

    public Room(int x, int y, Texture2D layoutTexture)
    {
        m_area = new RectInt(x, y, layoutTexture.width, layoutTexture.height);
        LayoutTexture = layoutTexture;
    }
    
    public List<Hallway> CalculateAllPossibleDoorways(int width, int length, int minDistanceFromEdge)
    {
        if (LayoutTexture == null)
        {
            return CalculateAllPossibleDoorwaysForRectangularRooms(width, length, minDistanceFromEdge);
        }
        return CalculateAllPossibleDoorwaysForTexturedRooms(LayoutTexture);
    }

    public List<Hallway> CalculateAllPossibleDoorwaysForRectangularRooms(int width, int length, int minDistanceFromEdge)
    {
        List<Hallway> hallwayCandidates = new List<Hallway>();
        int top = length - 1;

        int minX = Mathf.Clamp(minDistanceFromEdge, 0, width - 1);
        int maxX = Mathf.Clamp(width - minDistanceFromEdge, minX + 1, width);

        for (int x = minX; x < maxX; x++)
        {
            hallwayCandidates.Add(new Hallway(HallwayDirection.Bottom, new Vector2Int(x, 0)));
            hallwayCandidates.Add(new Hallway(HallwayDirection.Top, new Vector2Int(x, top)));
        }

        int right = width - 1;

        int minY = Mathf.Clamp(minDistanceFromEdge, 0, length - 1);
        int maxY = Mathf.Clamp(length - minDistanceFromEdge, minY + 1, length);

        for (int y = minY; y < maxY; y++)
        {
            hallwayCandidates.Add(new Hallway(HallwayDirection.Left, new Vector2Int(0, y)));
            hallwayCandidates.Add(new Hallway(HallwayDirection.Right, new Vector2Int(right, y)));
        }

        return hallwayCandidates;
    }

    public List<Hallway> CalculateAllPossibleDoorwaysForTexturedRooms(Texture2D layoutTexture)
    {
        List<Hallway> possibleHallwayPositions = new List<Hallway>();
        int width = layoutTexture.width;
        int height = layoutTexture.height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = layoutTexture.GetPixel(x, y);
                HallwayDirection direction = GetHallwayDirection(pixelColor);
                if (direction != HallwayDirection.Undefined)
                {
                    Hallway hallway = new Hallway(direction, new Vector2Int(x, y));
                    possibleHallwayPositions.Add(hallway);
                }
            }
        }
        return possibleHallwayPositions;
    }

    private HallwayDirection GetHallwayDirection(Color pixelColor)
    {
        Dictionary<Color, HallwayDirection> colorToDirectionMap = HallwayDirectionExtension.GetColorToDirectionMap();
        return colorToDirectionMap.TryGetValue(pixelColor, out HallwayDirection direction) ? direction : HallwayDirection.Undefined;
    }
}
