using UnityEngine;
using System.Collections;

namespace Assets.Source.Plauground.IAp
{
    public interface ICoroutine
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
        void StopCoroutine(IEnumerator waitCoroutine);
    }
}
