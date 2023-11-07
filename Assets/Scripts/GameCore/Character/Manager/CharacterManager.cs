using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameCore.Character
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