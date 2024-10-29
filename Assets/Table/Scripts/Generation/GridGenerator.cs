using UnityEngine;
using Table.Scripts.Entities;
using Table.Scripts.EntityProperties;
using UnityEditor;

namespace Table.Scripts.Generation
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 5;
        [SerializeField] private float spacing = 0.5f;

        [SerializeField] private Field _field;
        
        [ContextMenu("Generate Grid")]
        public Cell[,] GenerateGrid()
        {
            //Undo.SetCurrentGroupName("GenerateGrid");
            //int group = Undo.GetCurrentGroup();

            var cells = new Cell[rows, columns];

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var position = new Vector2(column * (1 + spacing), row * (1 + spacing));

                    var cellObject = Instantiate(cellPrefab, position, Quaternion.identity);
                    cellObject.transform.SetParent(_field.transform);

                    if (!cellObject.TryGetComponent(out Cell cell))
                        continue;

                    cell.Initialize(row, column, isHidden: false);
                    cells[row, column] = cell;

                    Undo.RegisterCreatedObjectUndo(cellObject, "GenerateCell");
                }
            }

            //Undo.CollapseUndoOperations(group);
            return cells;
        }
    }
}