using Movement.Components;
using Netcode;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler : NetworkBehaviour
    {
        public Button StartButton;
        public GameObject timer;
        public GameObject esperando;
        public static NetworkVariable<List<GameObject>> playerList= new NetworkVariable<List<GameObject>>();
        public List<GameObject> playergListInScene = new List<GameObject>();
        public bool empezado = false;
        public float time = 90;
        public GameObject winner;
        

        

        private void Start()
        {
            //Por cada jugador que existe en la escena anterior, se translada su datos a la lista local
            foreach(var player in UISelect.playerList.Value)
            {
                //Se le instancia lo que necesite para jugar
                player.GetComponent<FighterNetworkConfig>().FighterNetworkConfigServerRpc();
                player.GetComponent<PlayerNetworkConfig>().InstantiateCharacterServerRpc(player.GetComponent<PlayerNetworkConfig>().OwnerClientId);
                playerList.Value.Add(player);
            }
            //NO hay timer antes de empiece el juego
            timer.gameObject.GetComponent<Timer>().enabled = false;
            //El boton no sera visible sino eres host
            if (!IsHost)
            {
                StartButton.gameObject.SetActive(false);
            }

            StartButton.onClick.AddListener(OnStartButtonClickedServerRpc);
            esperando.SetActive(true);

            //Sino funciona borra


;


        }
        private void FixedUpdate()
        {
            if (empezado == true && IsHost)
            {

                time -= Time.deltaTime;
                PlayerRemainingServerRpc();
                timelimitServerRpc();
            }
          
        }

        
        #region Condicion de victoria
//Metodo de contar cuantos jugadores quedan en la escena,y como eso lo tiene que conllevar el servidor, se poner serverRpc ya que el servidor ejecuta con los inputs del cliente.
        [ServerRpc]
        private void PlayerRemainingServerRpc()
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                playerList.Value.Add(player);
            }
            if (playergListInScene.Count == 1)
            {
                foreach (GameObject player in playerList.Value)
                {

                    if (player.GetComponent<FighterMovement>().OwnerClientId == playergListInScene[0].GetComponent<FighterMovement>().OwnerClientId)
                    {
                        winner = player;

                    }
                }
            }
            playergListInScene = null;
        }


        //Metodo de contar cuantos tiempo queda ,y como eso lo tiene que conllevar el servidor, se poner serverRpc ya que el servidor ejecuta con los inputs del cliente.
        [ServerRpc]
        private void timelimitServerRpc()
        {
            if (time == 0)
            {
                int vida = 0;
                int posicion = 0;
                foreach (GameObject player in playergListInScene)
                {
                    if (player.GetComponent<FighterMovement>().vida.Value > vida)
                    {
                        vida = (int)player.GetComponent<FighterMovement>().vida.Value;
                        posicion = playergListInScene.IndexOf(player);
                    }

                }

                winner = playergListInScene[posicion];
            }
        }




        //Metodo start que si le da el host, todo el mundo empieza la partida.
        //Inicialmente ningun jugador se puede mover debido que se haya bloqueado sus fightermovement
        //Se ejecuta el calculo en el servidor y lo devuelve a los clientes
        [ServerRpc]
        private void OnStartButtonClickedServerRpc()
        {
            timer.gameObject.GetComponent<Timer>().enabled = true;
            StartButton.gameObject.SetActive(false);
            esperando.SetActive(false);
            foreach (GameObject player in playerList.Value)
            {
                player.GetComponent<FighterMovement>().enabled = true;
            }

            OnStartButtonClickedClientRpc();
            empezado = true;


        }
        [ClientRpc]
        private void OnStartButtonClickedClientRpc()
        {
            foreach (GameObject player in playerList.Value)
            {
                player.GetComponent<FighterMovement>().enabled = true;
            }


        }





    }
}