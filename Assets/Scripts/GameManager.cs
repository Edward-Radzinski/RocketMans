using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections.Generic;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPref;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PlayersTop _playersTop;

    private List<Player> _players = new List<Player>();
    private GameObject _me;

    private void Start()
    {
        _me = PhotonNetwork.Instantiate(_playerPref.name, _spawnPoint.position, Quaternion.identity);
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }

    private void Update()
    {
        _playersTop.SetText(_players);
    }

    public override void OnLeftRoom()//me
    {
        SceneManager.LoadScene("Lobby");
        //PhotonNetwork.LoadLevel("Lobby");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)//other
    {
        Debug.LogFormat("Player {0} exited", otherPlayer.NickName);
        _playersTop.ResetText();
        _players.Clear();
        AddPlayer(_me.GetComponent<Player>());

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
