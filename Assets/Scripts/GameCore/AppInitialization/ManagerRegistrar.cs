using System;
using System.Collections.Generic;

namespace GameCore.AppInitialization
{
    public class ManagerRegistrar
    {
        public readonly IReadOnlyDictionary<Type, Type> ManagersType = new Dictionary<Type, Type>
            { };
    }
}