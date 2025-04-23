using UnityEngine;
using Fusion;

public class Fruit : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NetworkObject obj;
    public NetworkRunner runner;
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (runner != null && obj != null)
        {
            runner.Despawn(obj);
        }
    }
}
