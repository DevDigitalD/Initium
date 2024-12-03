using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Character.Manager
{
    public class CharacterManager : MonoBehaviour, ICharacterManager
    {
        [SerializeField] private List<CharactersTemplateScriptableObject> _charactersTemplateList;
        private GameObject _playerCharacter;
        private List<EnemyCharacter.EnemyCharacter> _enemyCharacters;
        public void Init()
        {
            
        }

        public void Release()
        {
            
        }
    }
}