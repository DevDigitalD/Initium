using UnityEngine;

namespace GameCore.Root
{
    public abstract class InitializationStepBase
    {
        private readonly Root.GameInitialization _gameInitialization;
        protected bool DEBUG_MODE => true;

        protected InitializationStepBase(Root.GameInitialization gameInitialization)
        {
            _gameInitialization = gameInitialization;
        }

        public abstract void RunStep();

        protected virtual void Dispose()
        {
        }

        protected void OnStepComplete(object args = null)
        {
            Dispose();
            
            Debug.Log($"{GetType()} complete with time {Time.time}");
            _gameInitialization.NextStep();
        }

        protected void RepeatStep()
        {
            Dispose();
            
            _gameInitialization.RepeatStep();
        }
    }
}