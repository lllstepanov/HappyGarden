using UnityEngine;

namespace HappyGarden
{
	internal class FXManager : Singleton<FXManager>
	{
		[SerializeField] private GameObject confetti;
		[SerializeField] private GameObject sparks;

		internal void ShowConfetti()
		{
			AudioManager.Instance.PlayAudioFile("pop (1)");

			confetti.SetActive(true);
			confetti.GetComponent<ParticleSystem>().Play();
		}

		internal void CreateSparks(Vector3 pos)
		{
			GameObject sparksGO = Instantiate(sparks);
			sparksGO.transform.transform.position = pos;
		}

	}
}
	