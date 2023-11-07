using System;
using System.Collections.Generic;
using GameCore.Character;

namespace GameCore.AppInitialization
{
    public class ManagerRegistrar
    {
        public readonly IReadOnlyDictionary<Type, Type> ManagersType = new Dictionary<Type, Type>
        {
            {typeof(CharacterManager), typeof(ICharacterManager)}
        };
    }
}