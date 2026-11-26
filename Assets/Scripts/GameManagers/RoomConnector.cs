using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomConnector : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInputField;
    public TMP_InputField playerNameInputField;


    public void CreateRoom()
    {
        SavePlayerName();
        PhotonNetwork.CreateRoom(roomNameInputField.text, 
        new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public void JoinRoom()
    {
        SavePlayerName();
        PhotonNetwork.JoinRoom(roomNameInputField.text);
    }

    private void SavePlayerName()
    {
        if (string.IsNullOrWhiteSpace(playerNameInputField.text))
        {
            PhotonNetwork.NickName = "Player " + Random.Range(100, 999);
        }
        else
        {
            PhotonNetwork.NickName = playerNameInputField.text;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}