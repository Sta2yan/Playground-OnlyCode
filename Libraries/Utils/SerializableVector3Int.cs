using System;
using UnityEngine;

namespace Agava.Utils
{
    [Serializable]
    public struct SerializableVector3Int
    {
        public int X;
        public int Y;
        public int Z;

        public Vector3Int Vector3Int => new Vector3Int(X, Y, Z);

        public SerializableVector3Int(Vector3Int vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }
    }
}
