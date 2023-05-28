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


        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;
            Debug.Log(PlayerPrefs.GetInt("selectedCharacter"));
            InstantiateCharacterServerRpc(OwnerClientId);
            
            
        }
        [ServerRpc]
        public void InstantiateCharacterServerRpc(ulong id)
        {
            Debug.Log(characterPrefab[PlayerPrefs.GetInt("selectedCharacter")]);
            Debug.Log("Numero del perosnaje elegido",characterPrefab[PlayerPrefs.GetInt("selectedCharacter")]);
            GameObject characterGameObject = Instantiate(characterPrefab[PlayerPrefs.GetInt("selectedCharacter")]);
            
            characterGameObject.GetComponent<NetworkObject>().SpawnWithOwnership(id);
            characterGameObject.transform.SetParent(transform, false);
            characterGameObject.GetComponent<NetworkRigidbody2D>().enabled = true;
            
        }
    }
}
