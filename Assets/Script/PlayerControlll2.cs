using UnityEngine;
using Fusion;
using TMPro;
using Cinemachine;

public class PlayerControlll2 : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string namePlayer; // tên ngươi chơi
    public TextMeshProUGUI nameText; // hiển thị tên người chơi
    public CinemachineVirtualCamera FollowCamera; // Camera theo dõi người chơi

    //Phần của Hp
    [Networked,OnChangedRender(nameof(OnChangeHealth))]
    public int Health { get; set; }
    public TextMeshProUGUI hpText;

    private void OnChangeHealth()
    {
        hpText.text = ""+ Health;
    }
    void Start()
    {
    }

    // được gọi ngay sau khi nhân vật đã được spawn vào game
    public override void Spawned()
    {
        base.Spawned();
        FollowCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        Debug.Log(Object.HasInputAuthority);
        Debug.Log(FollowCamera != null);
        if (Object.HasInputAuthority && FollowCamera != null)
        {
            Debug.Log("Camera Follow...........");
            // chỉ có người chơi mà có input authority mới
            // được phép thực hiện các hành động sau
            FollowCamera.Follow = transform;
            FollowCamera.LookAt = transform;
        }
        if (Object.HasInputAuthority)
        {
            namePlayer = PlayerPrefs.GetString("PlayerName");
            nameText.text = "" + namePlayer;
        }
        if(Object.HasStateAuthority)
        {
            RpcUpdateHealth(300);
        }
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcUpdateHealth(int health)
    {
        Health = health;
        hpText.text = $"{Health}";
    }
    public override void FixedUpdateNetwork()
    {
        //xoay name và health text về phía camera
        if (FollowCamera != null)
        {
            ////nameText.transform.LookAt(FollowCamera.transform);
            ////hpText.transform.LookAt(FollowCamera.transform);
            if (transform.localScale.x > 0)
            {
                nameText.transform.localScale = new Vector3(-1, 0, 0);
                hpText.transform.localScale = new Vector3(-1, 0, 0);
            }
            //else
            //{
            //    nameText.transform.localScale = new Vector3(1, 0, 0);
            //    hpText.transform.localScale = new Vector3(1, 0, 0);
            //}

        }
    }
    public string NamePlayer()
    {
        return namePlayer;
    }
}
