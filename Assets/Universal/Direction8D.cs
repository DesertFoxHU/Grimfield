using System;

/// <summary>
/// 8-directional Directions
/// </summary>
[Flags]
public enum Direction8D
{
    None = 0,
    North = 1,
    East = 2,
    South = 4,
    West = 8,
    NorthEast = 16,
    NorthWest = 32,
    SouthEast = 64,
    SouthWest = 128,
    All = North | East | South | West | NorthEast | NorthWest | SouthEast | SouthWest
}
