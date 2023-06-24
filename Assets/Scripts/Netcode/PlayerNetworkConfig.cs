using Movement.Components;
using System;
using System.Numerics;
using UI;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Netcode
{
    public class PlayerNetworkConfig : NetworkBehaviour
    {

        public GameObject[] characterPrefab;
        public NetworkVariable<int> selected = new NetworkVariable<int>(-1);


        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            

            


        }
        //Aqui es para llamarlo cuando es el momento adecuado, paso por cada jugador e instancio su personaje en la escena, no se llama el metodo en OnNetworkSpawn
        [ServerRpc]
        public void InstantiateCharacterServerRpc(ulong id)
        {
            //Se instancia el personaje con el indice que se ha sincronizado cuando se ha elegido el personaje en UISelect.
            GameObject character = Instantiate(characterPrefab[selected.Value]);
            character.GetComponent<NetworkObject>().SpawnWithOwnership(id);
            character.transform.SetParent(transform, false);
            character.GetComponent<NetworkRigidbody2D>().enabled = true;
            character.GetComponent<FighterMovement>().enabled = false;
           

            
        }

        //public void SetNumber(int s)
        //{
        //    selected.Value = s;
        //    SetNumberServerRpc(s);
        //}
        //[ServerRpc]
        //public void SetNumberServerRpc(int s)
        //{
        //    selected.Value = s;
        //}


        //private void SetCharacter(int elegido)
        //{
        //    if (!IsLocalPlayer) return;
        //    numero = elegido;
        //    SetMyCharacterServerRpc(elegido);
        //}
        //[ServerRpc]
        //private void SetMyCharacterServerRpc(int elegido)
        //{
        //    if (!IsLocalPlayer) numero = elegido;
        //    SetPlayerNameClientRpc(elegido);
        //}
        //[ClientRpc]
        //private void SetPlayerNameClientRpc(int elegido)
        //{
        //    if (!IsLocalPlayer && !IsServer) numero = elegido ;
        //}
    }
}
