namespace Game.General.GeneralBehaviour
{
    public class StunBh
    {
        private int _stunCounter;
        private int _stunDuration;
        public bool IsStunned { get; private set; }
        
        private TurnManager _turnManager;

        private bool _isPlayerBh;

        public StunBh(TurnManager turnManager, SubscribeHandler subscribeHandler, bool isPlayerBh)
        {
            _turnManager = turnManager;
            subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);

            _isPlayerBh = isPlayerBh;
        }

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

        private void Subscribe()
        {
            if (_isPlayerBh) _turnManager.OnPlayerTurn += UpdateStunTime;
            else _turnManager.OnEnemiesTurn += UpdateStunTime;
        }

        private void Unsubscribe()
        {
            if (_isPlayerBh) _turnManager.OnPlayerTurn -= UpdateStunTime;
            else _turnManager.OnEnemiesTurn -= UpdateStunTime;
        }
    }
}