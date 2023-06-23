using Netcode;
using Unity.Netcode;
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
        public Button hostButton;
        public Button clientButton;
        public GameObject[] characters;
        public Username Username;
        public int selectedCharacter = 0;
        public static NetworkVariable<int> selectedCharacterIndex = new NetworkVariable<int>(0);


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
            Username.SetScene();
            SelectCharacterServerRpc(selectedCharacter);
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
        private void OnHostButtonClicked()
        {

            
            debugPanel.SetActive(false);
            NetworkManager.Singleton.StartHost();

        }
        private void OnClientButtonClicked()
        {
            
            debugPanel.SetActive(false);
            NetworkManager.Singleton.StartClient();

        }
        [ServerRpc]
        private void SelectCharacterServerRpc(int selectedCharacter)
        {
            selectedCharacterIndex.Value = selectedCharacter;
        }

    }
}