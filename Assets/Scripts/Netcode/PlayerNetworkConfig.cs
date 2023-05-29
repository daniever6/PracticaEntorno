using Movement.Components;
using System;
using System.Numerics;
using UI;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Netcode
{
    public class PlayerNetworkConfig : NetworkBehaviour
    {

        public GameObject[] characterPrefab;
        private NetworkVariable<int> selectedCharacterIndex = new NetworkVariable<int>(0);

        public int SelectedCharacterIndex
        {
            get { return selectedCharacterIndex.Value; }
            set { selectedCharacterIndex.Value = value; }
        }


        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;
            selectedCharacterIndex.Value = UISelect.selectedCharacter;
            InstantiateCharacterServerRpc(OwnerClientId);
        }
        [ServerRpc]
        public void InstantiateCharacterServerRpc(ulong id)
        {
            Debug.Log(id + "  Ha elegido personaje  " + selectedCharacterIndex.Value);
            GameObject character = Instantiate(characterPrefab[selectedCharacterIndex.Value]);
            character.GetComponent<NetworkObject>().SpawnWithOwnership(id);
            character.transform.SetParent(transform, false);
            character.GetComponent<NetworkRigidbody2D>().enabled = true;
            character.GetComponent<FighterMovement>().enabled = false;
            UIHandler.playergList.Add(character);
            Debug.Log("Jugadores dentro de la escena " +  UIHandler.playergList.Count);

            
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
