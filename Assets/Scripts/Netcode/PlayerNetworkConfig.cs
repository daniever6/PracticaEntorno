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
        public int Number = 0;


        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;
            InstantiateCharacterServerRpc(OwnerClientId);
        }
        [ServerRpc]
        public void InstantiateCharacterServerRpc(ulong id)
        {
            int selectCharacter = PlayerPrefs.GetInt("selectedCharacter");
            GameObject characterGameObject = Instantiate(characterPrefab[selectCharacter]);
            characterGameObject.GetComponent<NetworkObject>().SpawnWithOwnership(id);
            characterGameObject.transform.SetParent(transform, false);
            characterGameObject.GetComponent<NetworkRigidbody2D>().enabled = true;
            
        }
    }
}
