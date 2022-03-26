using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HappyGarden 
{
	internal class ProfileManager : EternalSingleton<ProfileManager>
	{
		[SerializeField] private ProfileData profile = new ProfileData();
		private string profilePath;

		private void Start()
		{
			MatchPathes();
			Load();
		}

		private void MatchPathes()
		{
#if UNITY_IOS || UNITY_ANDROID
			profilePath = System.IO.Path.Combine(Application.persistentDataPath, "profile.gd");
#else
        profilePath = System.IO.Path.Combine(Application.dataPath, "profile.gd");
#endif
		}

		private void Load()
		{
			if (File.Exists(profilePath))
			{
				Debug.Log("Profile Exists");
				LoadProfileData();
			}
			else
			{
				Debug.Log("New Profile");

				InventoryManager.Instance.ResetInventory();
				AudioManager.Instance.Load();
			}
		}

		private void OnApplicationFocus(bool pauseStatus)
		{
			if (!pauseStatus)
			{
				SaveProfileData();
			}
		}

		internal void SaveProfileData()
		{
			Debug.Log("<color=magenta>Save Profile</color>");

			profile.Save(InventoryManager.Instance.inventory, AudioManager.Instance.audioData);

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(profilePath);

			bf.Serialize(file, profile);
			file.Close();

			Debug.Log("<color=magenta>-------------Profile Saved-----------------</color>");
		}

		internal void LoadProfileData()
		{
			Debug.Log("<color=cyan>Load Profiles</color>");

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(profilePath, FileMode.Open);
			profile = (ProfileData)bf.Deserialize(file);

			InventoryManager.Instance.Load();
			AudioManager.Instance.Load();

			file.Close();

			Debug.Log("<color=cyan>-------------Profiles Loaded-----------------</color>");
		}

		internal void ResetSaves()
		{
			profile = new ProfileData();

			InventoryManager.Instance.ResetInventory();
			InventoryManager.Instance.Load();
			AudioManager.Instance.Load();

			SaveProfileData();
		}

		internal Inventory GetInventory()
		{
			return profile.inventory;
		}

		internal AudioData GetAudioData()
		{
			return profile.audioData;
		}

		internal string GetLocalization()
		{
			return profile.localization;
		}

		internal void SetLocalization(string value)
		{
			profile.localization = value;
		}

		internal void IncreaseLastUnlockLevel()
		{
			profile.lastUnlockedLevel++;
		}

		internal int GetLastUnlockedLevel()
		{
			return profile.lastUnlockedLevel;
		}

		internal void SetVibration(bool value)
		{
			profile.vibration = value;
		}

		internal bool GetVibration()
		{
			return profile.vibration;
		}

		internal void AddDoneLevelId(string id)
		{
			if (!IsLevelDone(id))
			{
				profile.doneLevels.Add(id);
			}
		}

		internal List<string> GetListOfDoneLevels()
		{
			return profile.doneLevels;
		}

		internal bool IsLevelDone(string value)
		{
			for (int i = 0; i < profile.doneLevels.Count; i++)
			{
				if (value == profile.doneLevels[i])
				{
					return true;
				}
			}

			return false;
		}
	}


	[Serializable]
	internal class ProfileData
	{
		[HideInInspector]
		internal bool vibration = true;

		[HideInInspector]
		internal string localization = "";
		
		internal int lastUnlockedLevel = 0;

		internal List<string> doneLevels = new List<string>();
		
		[HideInInspector]
		internal Inventory inventory = new Inventory();
		[HideInInspector]
		internal AudioData audioData = new AudioData();
		[HideInInspector]
		
		internal void Save(Inventory tempInventory, AudioData tempAudioData)
		{
			inventory = tempInventory;
			audioData = tempAudioData;
		}
	}
}

