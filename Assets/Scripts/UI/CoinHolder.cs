using UnityEngine;
using UnityEngine.UI;

namespace HappyGarden.UI {
    internal class CoinHolder : Singleton<CoinHolder>
    {
        [SerializeField]
        private Text coins;

        private void Start()
        {
            coins.text = InventoryManager.Instance.GetCoins().ToString();
        }

        internal void AddCoins(int value)
        {
            AudioManager.Instance.PlayAudioFile("add_coins");

            InventoryManager.Instance.AddCoins(value);
            coins.text = InventoryManager.Instance.GetCoins().ToString();
        }

        internal void MinusCoins(int value)
        {
            AudioManager.Instance.PlayAudioFile("add_coins");

            InventoryManager.Instance.MinusCoins(value);
            coins.text = InventoryManager.Instance.GetCoins().ToString();
        }
    }
}
