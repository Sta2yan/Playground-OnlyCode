using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Utils
{
    public interface IConditionCheck
    {
        bool ConditionMet { get; }

        void Check();
        void Uncheck();
    }
}
