using UnityEngine;

public class GridCell
{
    public Vector2Int GridPos;

    public Vector3 WorldPos;

    public bool Walkable = true;

    public GridCell(int x, int y, float cellSize)
    {
        GridPos = new Vector2Int(x, y);

        WorldPos = new Vector3(
            x * cellSize,
            y * cellSize,
            0f);
    }
}
