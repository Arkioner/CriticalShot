using System;
using CriticalShot.Managers;
using UnityEngine;
using UnityEngine.Networking;

namespace CriticalShot.Player
{
    [RequireComponent(typeof(PlayerManager))]
    public class PlayerSetup : NetworkBehaviour
    {
        [SerializeField] private Behaviour[] _componentsToDisable;

        [SerializeField] private string _remoteLayerName = "RemotePlayer";

        private Camera sceneCamera;

        void Start()
        {
            if (!isLocalPlayer)
            {
                DisableComponents();
                AssignRemoteLayer();
            }
            else
            {
                sceneCamera = Camera.main;
                if (sceneCamera != null)
                {
                    sceneCamera.gameObject.SetActive(false);
                }
            }
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            PlayerManager playerManager = GetComponent<PlayerManager>();

            GameManager.RegisterPlayer(netId.ToString(), playerManager);
        }

        private void DisableComponents()
        {
            foreach (Behaviour componentToDisable in _componentsToDisable)
            {
                componentToDisable.enabled = false;
            }
        }

        private void AssignRemoteLayer()
        {
            gameObject.layer = LayerMask.NameToLayer(_remoteLayerName);
        }

        private void OnDisable()
        {
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(true);
            }

            GameManager.UnRegisterPlayer(transform.name);
        }
    }
}