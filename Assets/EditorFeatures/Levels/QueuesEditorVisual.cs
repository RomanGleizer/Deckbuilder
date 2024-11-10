using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
public class QueuesEditorVisual : MonoBehaviour
{
    [SerializeField] private RowPlacement[] _queueVisual;

    public void UpdateVisual(Stack<EntityType>[] stacks)
    {
        _queueVisual = new RowPlacement[stacks.Length];
        for (int i = 0; i < stacks.Length; ++i)
        {
            _queueVisual[i] = new RowPlacement(stacks[i]);
        }
    }
}

#endif
