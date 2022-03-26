using UnityEngine;
using System;

namespace HappyGarden
{
	internal class InventoryManager : EternalSingleton<InventoryManager>
	{
		internal Inventory inventory;

		internal void Load()
		{
			inventory = ProfileManager.Instance.GetInventory();
		}

		internal void ResetInventory()
		{
			inventory = new Inventory();
		}

		internal void AddCoins(int value)
		{
			inventory.coins += value;
		}

		internal void MinusCoins(int value)
		{
			inventory.coins -= value;
		}

		internal int GetCoins()
		{
			return inventory.coins;
		}
	}


	[Serializable]
	internal class Inventory 
	{
		internal int coins = 0;
	}

}