using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Lock : MonoBehaviour
{
    public NetworkVariable<TextMeshProUGUI> display_player_name = new NetworkVariable<TextMeshProUGUI>();

    public void Awake()
    {
        display_player_name.Value.text = Username.username.usernameText;
    }

    void Update()
    {
        updateClientRpc();
    }
    [ServerRpc]
    private void updateServerRpc()
    {
        transform.localScale = new Vector3(0.00626f / transform.parent.localScale.x, 0.00626f / transform.parent.localScale.y, 0.00626f / transform.parent.localScale.z);
        updateClientRpc();
    }

    [ClientRpc]
    private void updateClientRpc()
    {
        transform.localScale = new Vector3(0.00626f / transform.parent.localScale.x, 0.00626f / transform.parent.localScale.y, 0.00626f / transform.parent.localScale.z);
    }
}