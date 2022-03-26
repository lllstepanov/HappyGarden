using UnityEngine;
using System.Collections.Generic;
using System;
using HappyGarden.UI;

namespace HappyGarden.Gameplay
{
    internal class FigurePart : MonoBehaviour
    {
        private Animator anim;

        internal event Action OnCompleted;

        [SerializeField] private List<CollisionDetecter> collisionDetecters = new List<CollisionDetecter>();

        private int amountOfDetections = 0;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            LineManager.OnCompleted += CheckDetections;
            LineManager.OnCompleted += () => amountOfDetections = 0;

            OnCompleted += ShowPart;
            OnCompleted += ResetDetecters;
            OnCompleted += PlaySound;
            OnCompleted += CreateSparksFX;
            OnCompleted += ComboHolder.Instance.AddCombo;

            SetUpDetecters();
        }

        private void CreateSparksFX() 
        {
            FXManager.Instance.CreateSparks(transform.position);
        }

        private void PlaySound() 
        {
            AudioManager.Instance.PlayAudioFile("pop (1)");
        }

        private void SetUpDetecters() 
        {
            for (int i = 0; i < collisionDetecters.Count; i++)
            {
                collisionDetecters[i].OnCollision += IncreaseAmountOfDerections;
            }
        }

        private void ResetDetecters() 
        {
            for (int i = 0; i < collisionDetecters.Count; i++)
            {
                collisionDetecters[i].OnCollision -= IncreaseAmountOfDerections;
            }
        }

        private void CheckDetections() 
        {
            if (amountOfDetections == collisionDetecters.Count) {
                OnCompleted?.Invoke();
            }
        }

        private void IncreaseAmountOfDerections() {
            amountOfDetections++;
        }

        private void ShowPart() 
        {
            anim.SetBool("Show", true);
        }
    }
}
