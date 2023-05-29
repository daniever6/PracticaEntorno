using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Conectados : NetworkBehaviour
{
    public TMPro.TextMeshProUGUI timerText;
    public static NetworkVariable<int> Players = new NetworkVariable<int>(0);


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateServerRpc();
    }
    [ServerRpc]
    public void UpdateServerRpc()
    {
        timerText.text = Players.Value.ToString("0") + "conectados";
        UpdateClientRpc();
    }
    [ClientRpc]
    public void UpdateClientRpc()
    {
        timerText.text = Players.Value.ToString("0") + "conectados";

    }
}
