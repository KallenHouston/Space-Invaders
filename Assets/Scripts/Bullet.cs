using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Direction;
    public float speed;
    public Action destroy;

    private void Update()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroy != null) { 
        destroy.Invoke();
        }

        Destroy(gameObject);
    }
}
