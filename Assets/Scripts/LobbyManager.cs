using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private Text _logText;
    [SerializeField] private Text _roomName;

    private void Start()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(1000, 9999);
        Log("Player: " + PhotonNetwork.NickName);

        //PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true; //автопереключение сцен
        PhotonNetwork.ConnectUsingSettings(); //присоединение к мастер-серверу
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected!");
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel("LVL1");
    }

    public void MyCreateRoom()
    {
        if (_roomName.text.Length == 0) return;
        print(_roomName.text);
        PhotonNetwork.CreateRoom(_roomName.text, new Photon.Realtime.RoomOptions { MaxPlayers = 5 });
    }

    public void MyJoinRoom()
    {
        if (_roomName.text.Length == 0) return;
        print(_roomName.text);
        PhotonNetwork.JoinRoom(_roomName.text);
    }

    public void MyJoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private void Log(string text)
    {
        Debug.Log(text);
        _logText.text += "\n";
        _logText.text += text;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
