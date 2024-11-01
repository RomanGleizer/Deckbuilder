using Table.Scripts.Entities;
using Table.Scripts.EntityProperties;
using UnityEngine;
using UnityEditor;

namespace Table.Scripts.Generation
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 5;
        [SerializeField] private float spacing = 1.3f;
        [SerializeField] private Color highlightColor = Color.yellow;
        [SerializeField] private Field field;

        [ContextMenu("Generate Grid")]
        public Cell[,] GenerateGrid()
        {
            var cells = new Cell[rows, columns];

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var position = new Vector2(column * (1 + spacing), -row * (1 + spacing));
                    var cellObject = Instantiate(cellPrefab, position, Quaternion.identity, field.transform);
                    if (!cellObject.TryGetComponent(out Cell cell))
                        continue;

                    var cellView = new CellView(cellObject.transform, highlightColor);
                    cell.Initialize(row, column, isHidden: false, cellView);

                    cells[row, column] = cell;

                    Undo.RegisterCreatedObjectUndo(cellObject, "GenerateCell");
                }
            }

            return cells;
        }
    }
}