using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScalar : MonoBehaviour
{
    private GridLayoutGroup grid;
    private RectOffset gridPadding;
    private RectTransform parent;

    public int rows = 6;
    public int cols = 7;
    public float spacing = 10;

    Vector2 lastSize;

    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        grid.spacing = new Vector2(spacing, spacing);
        parent = GetComponent<RectTransform>();
        gridPadding = grid.padding;
        lastSize = Vector2.zero;

        Screen.fullScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSize == parent.rect.size)
        {
            return;
        }

        var paddingX = gridPadding.left + gridPadding.right;
        var cellSize = Mathf.Round((parent.rect.width - paddingX - (rows - 1) * spacing) / rows);
        grid.cellSize = new Vector2(cellSize, cellSize);
    }
}
