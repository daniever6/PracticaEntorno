using Unity.Netcode;
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

        private void Start()
        {
            timer.gameObject.GetComponent<Timer>().enabled = false;
            StartButton.gameObject.SetActive(false);
            hostButton.onClick.AddListener(OnHostButtonClicked);
            clientButton.onClick.AddListener(OnClientButtonClicked);
            StartButton.onClick.AddListener(OnStartButtonClicked);


        }

        private void OnHostButtonClicked()
        {
            NetworkManager.Singleton.StartHost();
            debugPanel.SetActive(false);
            StartButton.gameObject.SetActive(true);
            
        }

        private void OnClientButtonClicked()
        {
            NetworkManager.Singleton.StartClient();
            debugPanel.SetActive(false);

        }
        private void OnStartButtonClicked()
        {
            timer.gameObject.GetComponent<Timer>().enabled = true;
            StartButton.gameObject.SetActive(false);

        }

    }
}