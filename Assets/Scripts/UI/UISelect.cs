using Netcode;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UISelect : MonoBehaviour
    {
        public Button StartButton;
        public Button Previous;
        public Button Next;
        public GameObject[] characters;
        public NetworkVariable<int> selectedCharacter= new NetworkVariable<int>(0);
        public static UISelect ui;

        private void Start()
        {
            StartButton.onClick.AddListener(StartGameServerRpc);
            Next.onClick.AddListener(NextCharacter);
            Previous.onClick.AddListener(previousCharacter);
            ui = this;
        }
        public void NextCharacter()
        {
            characters[selectedCharacter.Value].SetActive(false);
            selectedCharacter.Value = (selectedCharacter.Value + 1) % characters.Length;
            characters[selectedCharacter.Value].SetActive(true);
        }
        public void previousCharacter()
        {
            characters[selectedCharacter.Value].SetActive(false);
            selectedCharacter.Value--;
            if (selectedCharacter.Value < 0)
            {
                selectedCharacter.Value += characters.Length;
            }
            characters[selectedCharacter.Value].SetActive(true);
        }
        [ServerRpc]
        private void StartGameServerRpc()
        {

            Debug.Log("UI Select eligio " + selectedCharacter.Value);
            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);

        }



    }
}