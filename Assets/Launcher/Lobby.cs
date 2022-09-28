using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviourPunCallbacks
{ 
    public SceneTransition SceneTransition;
    public Text LogText;
    public InputField NickName;
    public InputField NameRoom;


    // Start is called before the first frame update
    public void Start()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.JoinLobby();
    }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        Log(roomInfo.Name);
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(NameRoom.text, new Photon.Realtime.RoomOptions { MaxPlayers = 5});
        Log("Creating room...");
    }

    public override void OnCreatedRoom()
    {
        Log("Room \"" + NameRoom.text + "\" is created!");
    }

    public void JoinRoom()
    {
        PhotonNetwork.NickName = NickName.text;
        Log("Joining to room: " + NameRoom.text);
        PhotonNetwork.JoinRoom(NameRoom.text);
    }

    public override void OnJoinedRoom()
    {
        Log("Joined to Room: " + NameRoom.text + ", with nick: " + NickName.text);
        /* PhotonNetwork.LoadLevel("Play");*/
        /*SceneTransition.SwitchToScene("Play");*/
        SceneManager.LoadScene("Play");
    }

    public void ShowNick()
    {
        Log("Player NickName: " + PhotonNetwork.NickName);
    }

    public  void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }

}
