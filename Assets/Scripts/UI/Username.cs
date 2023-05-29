using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class Username : MonoBehaviour
{
    public static Username username;
    public TMP_InputField usernameInput;

    public string usernameText;

    private void Awake()
    {
        if (username == null) {
            username = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
        Destroy(gameObject);
        }
    }

    public void Update(){
        SetUsernameServerRpc();
    }

    public void SetScene(){
        SceneManager.LoadSceneAsync("GameScene");
    }
    [ServerRpc]
    public void SetUsernameServerRpc()
    {
        this.usernameText = this.usernameInput.text;
    }

}
