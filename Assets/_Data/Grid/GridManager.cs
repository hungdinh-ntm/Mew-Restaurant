using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : LoadMonoBehaviour
{
    [SerializeField] protected int width = 20;
    [SerializeField] protected int height = 15;

    [SerializeField] protected float cellSize = 1f;

    protected GridCell[,] grid;

    private static GridManager instance;
    public static GridManager Instance => instance;

    protected override void Awake()
    {
        base.Awake();

        if (instance != null)
        {
            Debug.LogError("Only 1 GridManager allow to exist");
        }

        instance = this;
    }

    protected override void Start()
    {
        this.GenerateGrid();
    }

    protected virtual void GenerateGrid()
    {
        this.grid = new GridCell[this.width, this.height];

        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                this.grid[x, y] = new GridCell(x, y, this.cellSize);
            }
        }

        Debug.Log("Generate Grid Success!");
        Debug.Log($"Grid Size: {this.width} x {this.height}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);

                Gizmos.DrawWireCube(pos, Vector3.one * this.cellSize);
            }
        }
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int y = Mathf.FloorToInt(worldPos.y / cellSize);

        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x * this.cellSize,
            gridPos.y * this.cellSize,
            0);
    }

    private GridCell oldCell;

    private void Update()
    {
        GridCell cell = GetCell(PlayerCtrl.Instance.transform.position);

        if (cell == null) return;

        if (cell != oldCell)
        {
            oldCell = cell;
            Debug.Log(cell.GridPos);
        }
    }

    public GridCell GetCell(Vector2Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= this.width) return null;
        if (gridPos.y < 0 || gridPos.y >= this.height) return null;

        return this.grid[gridPos.x, gridPos.y];
    }

    public GridCell GetCell(Vector3 worldPos)
    {
        return GetCell(WorldToGrid(worldPos));
    }
}
