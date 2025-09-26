using System;
using Collectibles;
using generalScripts;
using UnityEngine;

namespace MainCharacterScripts
{
    public class PlayerLevelController : MonoBehaviour
    {
        private GameUIManager _gameUIManager;
        private Droplet _droplet;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        [Obsolete("Obsolete")]
        private void Start()
        {
            if (_droplet == null)
            {
                _droplet = GetComponent<Droplet>();
            }
            
            if (_gameUIManager == null)
            {
                _gameUIManager = FindObjectOfType<GameUIManager>();
            }
        }

        public void UpdateLevel(int value)
        {
            Debug.Log("Level Up!");
            _gameUIManager.UpdateLevel(value);
            
        }
    }
}
