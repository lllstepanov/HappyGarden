using UnityEngine;
using UnityEngine.UI;

namespace HappyGarden.UI {
	internal class ComboHolder : Singleton<ComboHolder>
	{
		[SerializeField]
		private GameObject comboCircleGO;

		[SerializeField]
		private Text comboText;

		[SerializeField]
		private Image comboCircle;

		private float speed = 2;
		private float comboTimeLeft = 0;

		private int comboCounter = 0;

		private void Update()
		{
			if (comboCounter > 0)
			{
				ComboCountdown();
			}
		}

		internal void AddCombo()
		{
			comboTimeLeft = 4;

			if (comboCounter < 12)
			{
				comboCounter++;
			}

			comboCircleGO.SetActive(true);

			comboText.text = "x" + comboCounter.ToString();
		}

		internal int GetComboCounter()
		{
			return comboCounter;
		}

		private void ComboCountdown()
		{
			comboTimeLeft -= Time.deltaTime * speed;

			if (comboCounter > 0)
			{
				comboCircle.fillAmount = ((comboTimeLeft / 4) * 100) / 100;
			}

			if (comboTimeLeft <= 0)
			{
				ResetCombo();
			}
		}

		internal void ResetCombo()
		{
			comboCircleGO.SetActive(false);

			comboCounter = 0;
			comboCircle.fillAmount = 0;
			comboText.text = "";
		}
	}

}
