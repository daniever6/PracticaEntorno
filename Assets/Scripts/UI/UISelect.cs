using Netcode;
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
        public Username Username;
        public static int selectedCharacter = 0;

        private void Start()
        {
            StartButton.onClick.AddListener(StartGame);
            Next.onClick.AddListener(NextCharacter);
            Previous.onClick.AddListener(previousCharacter);
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
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

    }
}