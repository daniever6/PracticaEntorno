using Movement.Components;
using System.Numerics;
using UI;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Netcode
{
    public class PlayerNetworkConfig : NetworkBehaviour
    {

        public GameObject[] characterPrefab;
        
        public static NetworkVariable<int> selectCharacter= new NetworkVariable<int>(0);


        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;
            InstantiateCharacterServerRpc(OwnerClientId);
        }
        [ServerRpc]
        public void InstantiateCharacterServerRpc(ulong id)
        {
            Debug.Log(id + "  Ha elegido personaje  " + characterPrefab[PlayerPrefs.GetInt("Selected")].name);
            GameObject character = Instantiate(characterPrefab[PlayerPrefs.GetInt("Selected")]);
            character.GetComponent<NetworkObject>().SpawnWithOwnership(id);
            character.transform.SetParent(transform, false);
            character.GetComponent<NetworkRigidbody2D>().enabled = true;
            character.GetComponent<FighterMovement>().enabled = false;
            UIHandler.playergList.Add(character);
            Debug.Log("Jugadores dentro de la escena " +  UIHandler.playergList.Count);

            
        }
    }
}
