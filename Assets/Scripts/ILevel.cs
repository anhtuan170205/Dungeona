using UnityEngine;

public interface ILevel
{
    int Length { get; }
    int Width { get; }

    public bool IsBlocked(int x, int y);
    public int Floor(int x, int y);
}
