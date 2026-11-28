using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Vector3 spawnPosition;
    public TMP_Text startButtonText;


    // spawn player
    void Start()
    {
        SpawnPlayer();
        SetupStartButton();
    }

    void SetupStartButton()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            startButtonText.text = "Waiting for host...";
        }
        else
        {
            startButtonText.text = "Start";
        }
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        SetupStartButton();
    }

    public void SpawnPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }

    public void OnStartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;


        photonView.RPC("LoadGameplayScene", RpcTarget.All);
    }

    [PunRPC]
    private void LoadGameplayScene()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }
}
