using UnityEngine;

namespace GameCore.Root
{
    [DefaultExecutionOrder(-1)]
    public class RootController : MonoBehaviour
    {
        private GameInitialization _gameInitialization;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _gameInitialization = new GameInitialization(gameObject);
            _gameInitialization.InitSystems();
        }
    }
}