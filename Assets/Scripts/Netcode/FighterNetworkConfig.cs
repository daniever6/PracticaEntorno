using Cinemachine;
using Movement.Components;
using Systems;
using Unity.Netcode;

namespace Netcode
{
    public class FighterNetworkConfig : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            
            
        }
        //ese metodo se ha sacado de OnNetworkSpawn para poder llamar cuando es necesario, de momento se queda fuera para no spawnear nada en una escena no deseada
        [ServerRpc]
        public void FighterNetworkConfigServerRpc()
        {
            FighterMovement fighterMovement = GetComponent<FighterMovement>();
            InputSystem.Instance.Character = fighterMovement;
            ICinemachineCamera virtualCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
            virtualCamera.Follow = transform;
        }
    }
}