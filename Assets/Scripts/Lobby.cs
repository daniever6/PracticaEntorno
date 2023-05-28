using Movement.Components;
using Netcode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class Lobby : NetworkBehaviour
{


    public NetworkVariable<bool> ready = new NetworkVariable<bool>();
    void Start()
    {
      

    }    

}

