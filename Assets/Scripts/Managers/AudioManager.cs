using UnityEngine;
using System;
using System.Collections;

namespace HappyGarden
{
	internal class AudioManager :EternalSingleton<AudioManager>
	{
		internal AudioData audioData = new AudioData();

		[SerializeField] private AudioSource effectsSource;
		[SerializeField] private AudioSource musicSource;

		internal void Load()
		{
			audioData = ProfileManager.Instance.GetAudioData();
		}

		private void Start()
		{
			StartCoroutine(WaitThenStart(0.5f));
		}

		IEnumerator WaitThenStart(float seconds)
		{
			yield return new WaitForSeconds(seconds);

			Pause(audioData.paused);
			SetMusicVolume((float)audioData.musicVolume);
			SetEffectsVolume((float)audioData.effectsVolume);
		}


		internal void SetMusicVolume(float volume)
		{
			audioData.musicVolume = volume;
			musicSource.volume = (float)audioData.musicVolume;
		}

		internal void SetEffectsVolume(float volume)
		{
			audioData.effectsVolume = volume;
			effectsSource.volume = (float)audioData.effectsVolume;
		}

		internal void PlayAudioFile(string audioName)
		{
			if (!audioData.paused)
			{
				AudioClip audioClip = Resources.Load<AudioClip>("Audio/" + audioName);
				effectsSource.PlayOneShot(audioClip);
			}
		}

		internal void ChangeBackgroundMusic(string audioName)
		{
			musicSource.clip = Resources.Load<AudioClip>("Audio/" + audioName);

			if (!audioData.paused)
			{
				musicSource.Stop();
				musicSource.Play();
			}
		}

		internal void Pause(bool value)
		{
			if (value)
			{
				audioData.paused = true;

				musicSource.Pause();
				effectsSource.Stop();
			}
			else
			{
				audioData.paused = false;

				musicSource.UnPause();
				effectsSource.Play();
			}
		}
	}

	[Serializable]
	internal class AudioData
	{
		internal bool paused = false;

		internal double musicVolume = 1;
		internal double effectsVolume = 1;
	}
}