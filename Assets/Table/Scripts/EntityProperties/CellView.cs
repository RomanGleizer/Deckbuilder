using UnityEngine;

namespace Table.Scripts.EntityProperties
{
    /// <summary>
    /// Class responsible for cell visual representation.
    /// </summary>
    public class CellView : MonoBehaviour
    {
        /// <summary>
        /// Activates highlighting for the cell.
        /// </summary>
        public void ActivateHighlighting()
        {
            Debug.Log("Cell is highlighted");
        }

        /// <summary>
        /// Deactivates highlighting for the cell.
        /// </summary>
        public void DeactivateHighlighting()
        {
            Debug.Log("Cell highlighting is removed");
        }
    }
}