using Netcode;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UISelect : NetworkBehaviour
    {
        public Button StartButton;
        public Button Previous;
        public Button Next;
        public GameObject debugPanel;
        public GameObject Letras;
        public Button hostButton;
        public Button clientButton;
        public GameObject[] characters;
        public Username Username;
        public int selectedCharacter = 0;
        public static NetworkVariable<int> selectedCharacterIndex = new NetworkVariable<int>(0);
        public static NetworkVariable<GameObject> character;
        public static NetworkVariable<List<GameObject>> playerList = new NetworkVariable<List<GameObject>>();



        private void Start()
        {
            debugPanel.SetActive(true);
            StartButton.onClick.AddListener(StartGame);
            Next.onClick.AddListener(NextCharacter);
            Previous.onClick.AddListener(previousCharacter);
            hostButton.onClick.AddListener(OnHostButtonClicked);
            clientButton.onClick.AddListener(OnClientButtonClicked);
        }
        public void NextCharacter()
        {
            characters[selectedCharacter].SetActive(false);
            selectedCharacter = (selectedCharacter + 1) % characters.Length;
            characters[selectedCharacter].SetActive(true);
        }
        public void previousCharacter()
        {
            characters[selectedCharacter].SetActive(false);
            selectedCharacter--;
            if (selectedCharacter < 0)
            {
                selectedCharacter += characters.Length;
            }
            characters[selectedCharacter].SetActive(true);
        }
        private void StartGame()
        {
            //Username.SetScene();
            Letras.SetActive(false);
            StartButton.gameObject.SetActive(false);
            Username.gameObject.SetActive(false);
            NetworkManager.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

        }
            

        private void OnHostButtonClicked()
        {

            
            debugPanel.SetActive(false);
            NetworkManager.Singleton.StartHost();
            addPlayerServerRpc();





        }
        private void OnClientButtonClicked()
        {
            
            debugPanel.SetActive(false);
            NetworkManager.Singleton.StartClient();
            addPlayerServerRpc();


        }


        //[ServerRpc]
        //private void addPlayerToSceneServerRpc(GameObject player)
        //{
        //        UIHandler.playerList.Value.Add(ForceNetworkSerializeByMemcpy<GameObject> player);
        //}


        //Metodo que mete el jugador a la lista de tipo gamobject,como network variable asi para permitir sincronizarlo en el servidor, se comprueba que si este jugador ya existe dentro de la escena
        //si existe no se mete, sino si se mete en la lista.
        [ServerRpc]
        private void addPlayerServerRpc()
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (playerList.Value.Contains(player))
                {
                    ;
                }
                else
                {
                    playerList.Value.Add(player);
                    
                    //addPlayerToSceneServerRpc(player);
                }
            }
        }
    }
}