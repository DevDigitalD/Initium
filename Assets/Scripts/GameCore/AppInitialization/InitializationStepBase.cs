using UnityEngine;

namespace GameCore.AppInitialization
{
    public abstract class InitializationStepBase
    {
        private readonly AppInitialization _appInitialization;
        protected bool DEBUG_MODE => true;

        protected InitializationStepBase(AppInitialization appInitialization)
        {
            _appInitialization = appInitialization;
        }

        public abstract void RunStep();

        protected virtual void Dispose()
        {
        }

        protected void OnStepComplete(object args = null)
        {
            Dispose();
            
            Debug.Log($"{GetType()} complete with time {Time.time}");
            _appInitialization.NextStep();
        }

        protected void RepeatStep()
        {
            Dispose();
            
            _appInitialization.RepeatStep();
        }
    }
}