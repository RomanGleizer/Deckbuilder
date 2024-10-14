using UnityEngine;

namespace Table.Scripts.EntityProperties
{
    public class CellView
    {
        public void ActivateHighlighting()
        {
            Debug.Log("Cell is highlighted");
        }
        
        public void DeactivateHighlighting()
        {
            Debug.Log("Cell highlighting is removed");
        }
    }
}