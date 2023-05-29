using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lock : MonoBehaviour
{
    public TextMeshProUGUI display_player_name;

    public void Awake()
    {
        display_player_name.text = Username.username.usernameText;
    }

    void Update()
    {
        transform.localScale = new Vector3(0.00626f / transform.parent.localScale.x, 0.00626f / transform.parent.localScale.y, 0.00626f / transform.parent.localScale.z);
    }
}