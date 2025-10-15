using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum HallwayDirection
{
    Undefined,
    Top,
    Bottom,
    Left,
    Right
}

public static class HallwayDirectionExtension
{
    private static Color yellow = new Color(1, 1, 0, 1);
    private static readonly Dictionary<HallwayDirection, Color> DIRECTION_COLOR_MAP = new Dictionary<HallwayDirection, Color>
    {
        { HallwayDirection.Top, Color.cyan },
        { HallwayDirection.Bottom, Color.green },
        { HallwayDirection.Left, yellow },
        { HallwayDirection.Right, Color.magenta }
    };

    public static Color GetColor(this HallwayDirection direction)
    {
        return DIRECTION_COLOR_MAP.TryGetValue(direction, out Color color) ? color : DIRECTION_COLOR_MAP[HallwayDirection.Undefined];
    }

    public static Dictionary<Color, HallwayDirection> GetColorToDirectionMap()
    {
        return DIRECTION_COLOR_MAP.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
    }
    
    public static HallwayDirection GetOppositeDirection(this HallwayDirection direction)
    {
        Dictionary<HallwayDirection, HallwayDirection> oppositeDirectionMap = new Dictionary<HallwayDirection, HallwayDirection>
        {
            { HallwayDirection.Top, HallwayDirection.Bottom },
            { HallwayDirection.Bottom, HallwayDirection.Top },
            { HallwayDirection.Left, HallwayDirection.Right },
            { HallwayDirection.Right, HallwayDirection.Left },
        };
        return oppositeDirectionMap.TryGetValue(direction, out HallwayDirection oppositeDirection) ? oppositeDirection : HallwayDirection.Undefined;
    }
}
