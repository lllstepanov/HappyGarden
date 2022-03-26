using System.Collections;
using UnityEngine;

namespace HappyGarden 
{
	internal class ShyDestroyer : MonoBehaviour
	{
		[SerializeField] private int seconds;

		private void Start()
		{
			StartCoroutine(WaitThenDestroy(seconds));
		}

		IEnumerator WaitThenDestroy(float seconds)
		{
			yield return new WaitForSeconds(seconds);

			Destroy(gameObject);
		}
	}
}
