using TableMechanics.Entities;
using UnityEngine;

namespace GeneralMechanics.Initialization
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