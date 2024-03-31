using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using kcp2k;
using Org.BouncyCastle.Security;

[RequireComponent (typeof(KcpTransport))]
[RequireComponent(typeof(NetworkManagerHUD))]

public class PongNetworkManager : NetworkManager
{
    [SerializeField] private Transform _leftPaddle;
    [SerializeField] private Transform _rightPaddle;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _boardPrefab;

    private GameObject _ball;
    private GameObject _board;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform start = numPlayers == 0 ? _leftPaddle : _rightPaddle;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
        if (numPlayers == 2)
        {
            _ball = Instantiate(_ballPrefab);
            NetworkServer.Spawn(_ball);
            _board = Instantiate(_boardPrefab);
            NetworkServer.Spawn(_board);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        if (_ball)
        {
            NetworkServer.Destroy(_ball);
        }
        if (_board)
        {
            NetworkServer.Destroy(_board);
        }
    }
}


//статья хабр про networkmanager + terrain github