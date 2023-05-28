using Unity.Netcode;
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
        private NetworkVariable<int> selectedCharacter = new NetworkVariable<int>(0);

        private void Start()
        {
            StartButton.onClick.AddListener(StartGameServerRpc);
            Next.onClick.AddListener(NextCharacter);
            Previous.onClick.AddListener(previousCharacter);
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
            if(selectedCharacter.Value < 0)
            {
                selectedCharacter.Value += characters.Length;
            }
            characters[selectedCharacter.Value].SetActive(true);
        }

        [ServerRpc]
        private void StartGameServerRpc()
        {
            PlayerPrefs.SetInt("selectedCharacter", selectedCharacter.Value);
            selectPlayerClientRpc();
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            
        }

        [ClientRpc]
        private void selectPlayerClientRpc()
        {
            PlayerPrefs.SetInt("selectedCharacter", selectedCharacter.Value);
        }
    }
}