using Fusion;
using UnityEngine;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    // Start is called before the first frame update
    public GameObject PlayerPrefab; // đối tượng player
    public Transform SpawnPosition;
    public void PlayerJoined(PlayerRef player)
    {
        //Kiểm tra người chơi có đang điều khiển hay không
        if (player == Runner.LocalPlayer)
        {
            //gọi API lấy thông tin
            // vị trí tạo đối tượng player
            var position = SpawnPosition;
            //Tạo đô
            Runner.Spawn(PlayerPrefab, position.position, 
                Quaternion.identity
                ,Runner.LocalPlayer,(runner, obj) =>
                {
                    //var _player = obj.GetComponent<PlayerSetUp>();
                    ////_player.SetUpCamera();
                    //var playerGun = obj.GetComponent<PlayerGun>();
                    //if(playerGun != null)
                    //{
                    //    playerGun.networkRunner = runner;
                    //}
                });
        };
    }
}
