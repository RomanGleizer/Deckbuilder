using Game.Table.Scripts.Entities;
using Table.Scripts.Entities;
using UnityEngine;

namespace General.Initialization.Scripts
{
    /// <summary>
    /// Initialize all objects with the "Initialize" method.
    /// </summary>
    public class ObjectInitialization : MonoBehaviour
    {
        [SerializeField] private Field _field;
        
        private void Awake()
        {
            _field.Initialize();
        }
    }
}