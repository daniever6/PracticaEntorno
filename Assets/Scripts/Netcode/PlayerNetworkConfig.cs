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


        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;
            
            
        }
        [ServerRpc]
        public void InstantiateCharacterServerRpc(ulong id)
        {
            if(SceneManager.GetActiveScene().name == "GameScene") { 
            int selectedCharacter = UISelect.selectedCharacterIndex.Value;
            Debug.Log(id + "  Ha elegido personaje  " + selectedCharacter);
            //GameObject character = Instantiate(characterPrefab[selectedCharacter]);
            //character.GetComponent<NetworkObject>().SpawnWithOwnership(id);
            //character.transform.SetParent(transform, false);
            //character.GetComponent<NetworkRigidbody2D>().enabled = true;
            //character.GetComponent<FighterMovement>().enabled = false;
            //UIHandler.playergList.Add(character);
            Debug.Log("Jugadores dentro de la escena " +  UIHandler.playergList.Count);
                }

            
        }


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
