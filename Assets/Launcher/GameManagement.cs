using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviourPunCallbacks
{
    public GameObject Player;

    public GameObject PlayerPrefab;

    public GameObject InputFieldObj;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-2, -5), 1, Random.Range(-5, -20));
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos,Quaternion.Euler(0,0,0));
        PhotonNetwork.Instantiate(InputFieldObj.name, pos, Quaternion.Euler(0, 0, 0));
        InputFieldObj.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() //функция, которая вызовется когда клиент отсоедениться от комнаты
    {
       /* SceneManager.LoadScene("Lobby");*/
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player(0) entered to room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        Debug.LogFormat("Player(0) left from room", newPlayer.NickName);
    }
}
