using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float playerSpeed = 5.0f;
    private bool bulletActive;



    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) ||  Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    } 

    private void shoot()
    {
        if (!bulletActive) {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.destroy += BulletDestroyed;
            bulletActive = true;
        }
    }

    private void BulletDestroyed()
    {
        bulletActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Add this later
        if(other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
            other.gameObject.layer == LayerMask.NameToLayer("Missle"))
        {

        }
    }    
}
