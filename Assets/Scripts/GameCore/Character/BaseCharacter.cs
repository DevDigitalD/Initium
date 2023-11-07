using UnityEngine;

namespace GameCore.Character
{
    public abstract class BaseCharacter : MonoBehaviour, ICharacter
    {
        private protected int health;
        private protected int damage;
        private protected int armor;
    }
}