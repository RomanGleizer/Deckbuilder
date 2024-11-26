namespace Game.General.GeneralBehaviour
{
    public class StunBh
    {
        private int _stunCounter;
        private int _stunDuration;
        public bool IsStunned { get; private set; }
        
        public void StartStun(int stunDuration)
        {
            _stunDuration = stunDuration;
            _stunCounter = 0;
            IsStunned = true;
        }
        
        public void UpdateStunTime()
        {
            if (!IsStunned) return;

            _stunCounter++;

            if (_stunCounter >= _stunDuration)
            {
                EndStun();
            }
        }
        
        private void EndStun()
        {
            IsStunned = false;
            _stunCounter = 0;
            _stunDuration = 0;
        }
    }
}