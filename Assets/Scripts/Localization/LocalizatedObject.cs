using System;
using UnityEngine;
using UnityEngine.UI;

namespace HappyGarden
{
	internal class LocalizatedObject : MonoBehaviour
	{
		private Text label;

		private void OnEnable()
		{
			UpdateLocalization();
		}

		internal void UpdateLocalization()
		{
			try
			{
				if (GetComponent<Text>() != null)
				{
					label = GetComponent<Text>();

					label.text = LocalizationManager.Instance.GetString(gameObject.name);
				}
				else if (GetComponent<TextMesh>() != null)
				{
					GetComponent<TextMesh>().text = LocalizationManager.Instance.GetString(gameObject.name);
				}
			}
			catch (Exception)
			{
				Debug.Log("Localization Error. GameObject: " + gameObject.name);
			}
		}
	}
}
