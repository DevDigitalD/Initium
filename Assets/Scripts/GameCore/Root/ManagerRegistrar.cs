using System;
using System.Collections.Generic;
using GameCore.Character;
using GameCore.Character.Manager;
using GameCore.HealthSystem;

namespace GameCore.Root
{
    public class ManagerRegistrar
    {
        public readonly IReadOnlyDictionary<Type, Type> ManagersType = new Dictionary<Type, Type>
        {
            {typeof(CharacterManager), typeof(ICharacterManager)},
            {typeof(HealthManager), typeof(IHealthManager)}
        };
    }
}