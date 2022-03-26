using UnityEngine;
using System.Collections.Generic;
using System;
using HappyGarden.UI;

namespace HappyGarden.Gameplay
{
    internal class Figure : MonoBehaviour
    {
        internal event Action OnCompleted;

        private Animator anim;
        
        private int amountOfParts = 0;

        [SerializeField] private List<FigurePart> figureParts = new List<FigurePart>();

        [SerializeField] private int winCoins;

        [SerializeField] private GameObject facePoint;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            OnCompleted += CompleteFigure;
            OnCompleted += PlaySound;
            OnCompleted += AddCoins;
            OnCompleted += AddFace;
            OnCompleted += FXManager.Instance.ShowConfetti;
            OnCompleted += UIMain.Instance.ShowNextLevelButton;
            OnCompleted += LevelManager.Instance.IncreaseLastUnlockedLevel;

            SetUp();
        }

        private void PlaySound()
        {
            AudioManager.Instance.PlayAudioFile("win_sound");
        }

        private void AddCoins()
        {
            CoinHolder.Instance.AddCoins(winCoins);
        }

        private void SetUp() 
        {
            for (int i = 0; i < figureParts.Count; i++)
            {
                figureParts[i].OnCompleted += CheckPart;
            }
        }

        private void CheckPart() 
        {
            amountOfParts++;

            if (amountOfParts == figureParts.Count) {
                OnCompleted?.Invoke();
            }
        }

        private void CompleteFigure() 
        {
            anim.SetTrigger("Finish");
        }

        private void AddFace() 
        {
            GameObject face = FaceManager.Instance.GetFace();

            face.transform.parent = facePoint.transform;
            face.transform.localPosition = new Vector3(0,0,0);
        }
    }
}