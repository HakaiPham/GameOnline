using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scene3Manager : NetworkBehaviour, INetworkRunnerCallbacks
{
    public NetworkPrefabRef _MalePlayerPrefapt;
    public NetworkPrefabRef _FemalePlayerPrefapt;
    public NetworkPrefabRef[] _FruitPrefabs;

    public NetworkRunner _runner;
    public NetworkSceneManagerDefault _sceneManager;

    public NetworkObject fruitObj;

    public Transform positionSpawn;
    public AudioSource backgroundMusic;

    private void Start()
    {
        ConnectToFusion();
    }
    //Khoi tao cac bien
    private void Awake()
    {
        if(positionSpawn == null)
        {
            positionSpawn = GameObject.Find("SpawnPosition").GetComponent<Transform>();
        }
        if(_runner == null)
        {
            GameObject obj = new GameObject("NetworkRunner");
            _runner = obj.AddComponent<NetworkRunner>();
            _sceneManager = obj.AddComponent<NetworkSceneManagerDefault>();
            _runner.AddCallbacks(this);

            //InvokeRepeating(nameof(SpawnFruit), 5, 5);

        }
        //ConnectToFusion();
    }
    private NetworkObject _fruitObj;
    public void SpawnFruit()
    {
        var fruitPrefab = _FruitPrefabs[UnityEngine.Random.Range(0, _FruitPrefabs.Length)];
        var positon = new Vector2(UnityEngine.Random.Range(-5.62f, 4f), 1);
        Debug.Log(_runner!= null);
        Debug.Log(fruitPrefab);

        if (_runner != null) _fruitObj = _runner
                .Spawn(fruitPrefab, positon, Quaternion.identity, null, (ru, ob ) =>
        {
            Fruit fr = ob.GetComponent<Fruit>();
            if (fr != null)
            {
                fr.obj = ob;
                fr.runner = ru;
            }
        }
        );
    }
    async void ConnectToFusion()
    {
        Debug.Log("Connecting to Fusion Network....");
        _runner.ProvideInput = true; //cho pheps nguoi choi nhap Input
        string sessionName = "MyGameSession";//tên phiên

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared, // Chế độ Shared Mode
            SceneManager = _sceneManager,
            SessionName = sessionName,
            PlayerCount = 5,// số lượng người chơi tối đa
            IsVisible = true,//Có hiển thị phiên hay không
            IsOpen = true,//Có cho phép người chơi khác tham gia hay không
        };
        //Kết nối mạng vào Fusion
        var result = await _runner.StartGame(startGameArgs);
        if (result.Ok)
        {
            Debug.Log("Connect Successfully");

            InvokeRepeating(nameof(SpawnFruit), 5, 5);

        }
        else
        {
            Debug.LogError("Failed to connect: "+result.ShutdownReason);
        }
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }
    //Hàm này chạy khi player kết nối mạng thành công
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log(".........Player Joined: " + player);
        //Thực hiện Spawn nv
        if (_runner.LocalPlayer != player) return;
        //Thực hiện spawn nhân vật
        var playerClass = PlayerPrefs.GetString("PlayerClass");
        var prefab = playerClass.Equals("Male")? _MalePlayerPrefapt:_FemalePlayerPrefapt;
        _runner.Spawn(
                prefab,
                positionSpawn.position,
                Quaternion.identity,
                player,
                (r, obj) =>
                {
                    Debug.Log("Player Spawn: " + obj);
                }
            );
        var playerName = PlayerPrefs.GetString("PlayerName");

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
