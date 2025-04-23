using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPropertive : NetworkBehaviour
{
    // Start is called before the first frame update
    [Networked, OnChangedRender(nameof(OnHealthChange))]
    public float currentHp { get; set; }
    public float maxHp { get; set; }

    public TextMeshProUGUI hpText;
    void Start()
    {
        maxHp = 100f;
        currentHp = maxHp;
    }
    public void OnHealthChange()
    {
        hpText.text = "" + currentHp + "/" + maxHp;
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            currentHp -= 10;
            //Destroy(gameobject) không xài trong mạng
            //Dùng NetworkRunner.Despawn(networkObject);
        }
    }
}
