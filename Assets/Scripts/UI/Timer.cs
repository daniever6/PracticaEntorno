using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Timer : NetworkBehaviour
{
    public TMPro.TextMeshProUGUI timerText;
    private NetworkVariable<float> currentTime = new NetworkVariable<float>(90);
    

    void Start()
    {
    }

    void Update()
    {
        UpdateServerRpc();
    }
    [ServerRpc]
    public void UpdateServerRpc()
    {
        currentTime.Value -= 1 * Time.deltaTime;
        timerText.text = currentTime.Value.ToString("0");

        if (currentTime.Value <= 0)
        {
            currentTime.Value = 0;
            Debug.Log("End game");
        }
        UpdateClientRpc();

    }
    [ClientRpc]
    public void UpdateClientRpc()
    {
        timerText.text = currentTime.Value.ToString("0");

        if (currentTime.Value <= 0)
        {
            currentTime.Value = 0;
            Debug.Log("End game");
        }
    }
    
}
