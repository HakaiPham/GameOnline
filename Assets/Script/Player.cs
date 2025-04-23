using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    // Start is called before the first frame update
    public float speed = 3;
    Rigidbody2D rb;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Debug.Log("Horizontal: " + horizontalInput);
            float verticalInput = Input.GetAxis("Vertical");
            Debug.Log("Vertical: " + verticalInput);

            if (horizontalInput != 0 || verticalInput != 0)
            {
                transform.position += new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed;
            }
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                rb.linearVelocity = Vector3.up * 3;
            }
        }
    }
    //void Update()
    //{
    //    //Nếu có quyền thì di chuyển
    //    if (Object.HasStateAuthority)
    //    {
    //        float horizontalInput = Input.GetAxis("Horizontal");
    //        Debug.Log("Horizontal: "+horizontalInput);
    //        float verticalInput = Input.GetAxis("Vertical");
    //        Debug.Log("Vertical: " + verticalInput);

    //        if (horizontalInput != 0 || verticalInput != 0)
    //        {
    //            transform.position += new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed;
    //        }
    //    }
    //}
}
