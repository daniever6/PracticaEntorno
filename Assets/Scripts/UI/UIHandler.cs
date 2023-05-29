using Movement.Components;
using Netcode;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        public GameObject debugPanel;
        public Button hostButton;
        public Button clientButton;
        public Button StartButton;
        public GameObject timer;
        public GameObject esperando;
        public static List<GameObject> playergList = new List<GameObject>();
        public static NetworkVariable<int> selectedCharacterIndex = new NetworkVariable<int>();
        


        private void Start()
        {
            timer.gameObject.GetComponent<Timer>().enabled = false;
            StartButton.gameObject.SetActive(false);
            hostButton.onClick.AddListener(OnHostButtonClicked);
            clientButton.onClick.AddListener(OnClientButtonClicked);
            StartButton.onClick.AddListener(OnStartButtonClickedServerRpc);
            esperando.SetActive(false);
            selectedCharacterIndex.Value = UISelect.ui.selectedCharacter.Value;


        }

        private void OnHostButtonClicked()
        {
            
            NetworkManager.Singleton.StartHost();
            debugPanel.SetActive(false);
            StartButton.gameObject.SetActive(true);
            esperando.SetActive(true);



        }

        private void OnClientButtonClicked()
        {

            NetworkManager.Singleton.StartClient();
            debugPanel.SetActive(false);
            esperando.SetActive(true);



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




        }






    }
}