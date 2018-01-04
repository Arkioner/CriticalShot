using UnityEngine;
using System.Collections.Generic;
using CriticalShot.Player;

namespace CriticalShot.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static Dictionary<string, PlayerManager> _players = new Dictionary<string, PlayerManager>();
        private const string PlayerIdPrefix = "Player_";
        private GUIStyle _guiStyle;

        void Start()
        {
            _guiStyle = new GUIStyle();
            _guiStyle.normal.textColor = Color.black;
        }

        public static void RegisterPlayer(string netId, PlayerManager player)
        {
            string playerId = PlayerIdPrefix + netId;
            player.transform.name = playerId;
            _players.Add(playerId, player);
        }

        public static void UnRegisterPlayer(string playerId)
        {
            _players.Remove(playerId);
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(20, 400, 300, 500));
            GUILayout.BeginVertical();
            
            foreach (string playerId in _players.Keys)
            {
                GUILayout.Label(playerId + " - " + _players[playerId].transform.name, _guiStyle);
            }
            
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        public static PlayerManager GetPlayerManager(string playerId)
        {
            return _players[playerId];
        }
    }
}