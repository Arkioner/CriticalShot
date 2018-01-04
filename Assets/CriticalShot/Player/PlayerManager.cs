using UnityEngine;
using UnityEngine.Networking;

namespace CriticalShot.Player
{
    public class PlayerManager : NetworkBehaviour
    {
        [SerializeField] private int _maxHealth = 100;

        [SyncVar] private int _currentHealth;

        private void Awake()
        {
            SetDefaults();
        }

        public void SetDefaults()
        {
            _currentHealth = _maxHealth;
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
        }
    }
}