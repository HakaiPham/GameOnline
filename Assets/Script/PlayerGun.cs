using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerGun : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefapt;
    public Transform fireTransform;

    public NetworkRunner networkRunner;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(networkRunner is not null && networkRunner.LocalPlayer.IsRealPlayer) 
            {
                var bullet = networkRunner.Spawn(bulletPrefapt, fireTransform.position, fireTransform.rotation);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * 3f;
                //bullet.GetComponent<Rigidbody2D>().AddForce(fireTransform.forward * 20f, ForceMode2D.Impulse);
            }
        }
    }
}
