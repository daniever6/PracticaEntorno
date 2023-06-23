using Movement.Components;
using Netcode;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler : NetworkBehaviour
    {
        public Button StartButton;
        public GameObject timer;
        public GameObject esperando;
        public static List<GameObject> playerList= new List<GameObject>();
        public List<GameObject> playergListInScene = new List<GameObject>();
        public bool empezado = false;

        

        private void Start()
        {
            timer.gameObject.GetComponent<Timer>().enabled = false;
            if (IsHost)
            {
                StartButton.gameObject.SetActive(true);
            }
            StartButton.onClick.AddListener(OnStartButtonClickedServerRpc);
            esperando.SetActive(true);
            playerList.Sort(gameObject.GetComponentInChildren<FighterMovement>().OwnerClientId);
;


        }
        private void FixedUpdate()
        {
            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                playerList.Add(player);
            }
            foreach (GameObject player in playerList)
            {
                foreach (GameObject playerScene in playergListInScene)
                {
                    if (player != playerScene)
                    {
                        playerList.Remove(player);
                    }
                }
            }
          
        }

        [ServerRpc]
        private void OnStartButtonClickedServerRpc()
        {
            timer.gameObject.GetComponent<Timer>().enabled = true;
            StartButton.gameObject.SetActive(false);
            esperando.SetActive(false);
            foreach (GameObject player in playergList)
            {
                player.GetComponent<FighterMovement>().enabled = true;
            }

            OnStartButtonClickedClientRpc();


        }
        [ClientRpc]
        private void OnStartButtonClickedClientRpc()
        {
            foreach (GameObject player in playergList)
            {
                player.GetComponent<FighterMovement>().enabled = true;
            }


        }





    }
}