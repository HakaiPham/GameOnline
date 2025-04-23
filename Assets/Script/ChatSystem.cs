using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using Unity.VisualScripting;

public class ChatSystem : NetworkBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textMessage;
    public TMP_InputField inputField;
    private void Start()
    {
        textMessage = GameObject.Find("TextMessage").GetComponent<TextMeshProUGUI>();
        inputField = GameObject.Find("InputChat").GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener((message) =>
        {
            PressEnterToSend();
        });
    }
    public override void Spawned()
    {
        // hàm này dc gọi khi đối tượng dc tạo ra
    }
    //Sources: Nguồn gửi
    //Targets: đích nhận
    public void PressEnterToSend()
    {
        var message = inputField.text;
        if (string.IsNullOrWhiteSpace(message)) return;
        var playerId = Runner.LocalPlayer.PlayerId;
        RpcChat("Player " + playerId + ": " + message);
        inputField.text = "";
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcChat(string message)
    {
        //Tên hàm bắt đầu bằng Rpc + tên hàm
        //hàm phải public
        // trả về void
        Debug.Log(message);
        textMessage.text += message +"\n";
    }
}
