using UnityEngine;
using UnityEngine.UI;
using HappyGarden.Gameplay;

namespace HappyGarden.UI
{
	internal class UIMain : Singleton<UIMain>
	{
		[SerializeField]
		private GameObject nextLevelButton;

        [SerializeField]
        private Text currentLevelText;

        private void Start()
        {
            nextLevelButton.GetComponent<Button>().onClick.AddListener(() => HideNextLevelButton());
            nextLevelButton.GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.CreateLevel());
        }

        internal void SetNewLevelText(int levelNumber) {
            currentLevelText.text = LocalizationManager.Instance.GetString("level_label") + " " + levelNumber.ToString();
        }

        internal void ShowNextLevelButton() 
        {
            nextLevelButton.SetActive(true);
        }

        private void HideNextLevelButton()
        {
            nextLevelButton.SetActive(false);
        }
    }
}
