using UnityEngine;
using System.Collections.Generic;

public enum HallwayDirection
{
    Undefined,
    Top,
    Bottom,
    Left,
    Right
}

public static class HallwayDirectionExtensions
{
    private static readonly Dictionary<HallwayDirection, Color> DIRECTION_COLOR_MAP = new Dictionary<HallwayDirection, Color>
    {
        { HallwayDirection.Undefined, Color.gray },
        { HallwayDirection.Top, Color.green },
        { HallwayDirection.Bottom, Color.red },
        { HallwayDirection.Left, Color.blue },
        { HallwayDirection.Right, Color.yellow }
    };
    
    public static Color GetColor(this HallwayDirection direction)
    {
        return DIRECTION_COLOR_MAP.TryGetValue(direction, out Color color) ? color : DIRECTION_COLOR_MAP[HallwayDirection.Undefined];
    }
}
