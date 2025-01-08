using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards
{
    public class CardTriggerHandler : MonoBehaviour
    {
        public static Cell CurrentCell { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var cell = other.GetComponent<Cell>();
            if (cell != null)
            {
                CurrentCell = cell;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var cell = other.GetComponent<Cell>();
            if (cell == CurrentCell)
            {
                CurrentCell = null;
            }
        }
    }
}