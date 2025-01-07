using Game.Table.Scripts.Entities;
using UnityEditor;
using UnityEngine;

namespace Game.Table.Scripts.Generation
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 5;
        [SerializeField] private float horizontalSpacing = 0.1f;
        [SerializeField] private float verticalSpacing = 0.1f;
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
            var startPosition = new Vector2(
                mainCamera.transform.position.x + (cameraWidth / 2) - (cellSize.x / 2) - rightOffset,
                mainCamera.transform.position.y + (cameraHeight / 2) - (cellSize.y / 2) - topOffset
            );

            for (var row = 0; row < rows; row++)
                for (var column = columns - 1; column >= 0; column--)
                {
                    var position = new Vector2(
                        startPosition.x - (columns - 1 - column) * (cellSize.x + horizontalSpacing),
                        startPosition.y - row * (cellSize.y + verticalSpacing)
                    );

                    var cellObject = Instantiate(cellPrefab, position, Quaternion.identity, field.transform);
                    if (!cellObject.TryGetComponent(out Cell cell))
                    {
                        Debug.LogError("Cell prefab does not have a Cell component.");
                        continue;
                    }

                    if (cellObject.GetComponent<Renderer>() == null)
                    {
                        Debug.LogError("Cell prefab does not have a Renderer component.");
                        continue;
                    }

                    var isHidden = column is 3 or 4;
                    cell.Initialize(row, column, isHidden);

                    Undo.RegisterCreatedObjectUndo(cellObject, "GenerateCell");
                }
        }
    }
}
