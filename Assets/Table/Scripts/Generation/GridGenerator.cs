using UnityEngine;
using Table.Scripts.Entities;
using Table.Scripts.EntityProperties;

namespace Table.Scripts.Generation
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 5;
        [SerializeField] private float spacing = 0.5f;
        
        public Cell[,] GenerateGrid()
        {
            var cells = new Cell[rows, columns];

            for (var row = 0; row < rows; row++)
                for (var column = 0; column < columns; column++)
                {
                    var position = new Vector2(column * (1 + spacing), row * (1 + spacing));
                    
                    var cellObject = Instantiate(cellPrefab, position, Quaternion.identity);
                    if (!cellObject.TryGetComponent(out Cell cell))
                        continue;
                    
                    if (!cellObject.TryGetComponent(out CellView cellView))
                        continue;
                    
                    cell.Initialize(row, column, isHidden: false, cellView);
                    cells[row, column] = cell;
                }

            return cells;
        }
    }
}