using Game.Table.Scripts.Entities;
using Table.Scripts.Entities;
using Table.Scripts.EntityProperties;
using UnityEditor;
using UnityEngine;

namespace Game.Table.Scripts.Generation
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 5;
        [SerializeField] private float spacing;
        [SerializeField] private Color highlightColor = Color.yellow;
        [SerializeField] private Field field;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float topOffset = 0.5f;
        [SerializeField] private float rightOffset = 0.5f;
        
        [ContextMenu("Generate Grid")]
        public void GenerateGrid()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
                if (mainCamera == null)
                {
                    Debug.LogError("Main camera cannot be found in the scene.");
                    return;
                }
            }

            var cameraHeight = 2f * mainCamera.orthographicSize;
            var cameraWidth = cameraHeight * mainCamera.aspect;
    
            var cellSize = cellPrefab.GetComponent<Renderer>().bounds.size;
    
            // Стартовая позиция в правом верхнем углу
            var startPosition = new Vector2(
                mainCamera.transform.position.x + (cameraWidth / 2) - (cellSize.x / 2) - rightOffset,
                mainCamera.transform.position.y + (cameraHeight / 2) - (cellSize.y / 2) - topOffset
            );

            var cells = new Cell[rows, columns];

            for (var row = 0; row < rows; row++)
            for (var column = columns - 1; column >= 0; column--)
            {
                var position = new Vector2(
                    startPosition.x - (columns - 1 - column) * (cellSize.x + spacing),
                    startPosition.y - row * (cellSize.y + spacing)
                );

                var cellObject = Instantiate(cellPrefab, position, Quaternion.identity, field.transform);
                if (!cellObject.TryGetComponent(out Cell cell))
                    continue;

                var cellView = new CellView(cellObject.transform, highlightColor);
                cell.Initialize(row, column, isHidden: false, cellView);

                cells[row, column] = cell;

                Undo.RegisterCreatedObjectUndo(cellObject, "GenerateCell");
            }
        }

    }
}
