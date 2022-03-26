using System;
using UnityEngine;

namespace HappyGarden.Gameplay
{
    internal class CollisionDetecter : MonoBehaviour
    {
        internal event Action OnCollision;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCollision?.Invoke();
        }
    }
}
