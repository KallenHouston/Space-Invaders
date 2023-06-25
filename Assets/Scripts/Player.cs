using System.Collections; 
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float playerSpeed = 5.0f;
    public int lives = 3; 
    public float iframesDuration = 1.0f; 
    private int currentlives; 
    private bool bulletActive;
    private bool isInvincible; 

    private void Start()
    {
        currentlives = lives;
    }

    private void Update()
    {
        if (!isInvincible)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                shoot();
            }
        }
    }

    private void shoot()
    {
        if (!bulletActive)
        {
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
        if (!isInvincible && other.gameObject.layer == LayerMask.NameToLayer("Missle"))
        {
            takeDamage();
        }

        if (!isInvincible && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            die();
        }
    }

    private void takeDamage()
    {
        currentlives--;
        Debug.Log("Player hit! Lives left: " + currentlives);

        if (currentlives <= 0)
        {
            die();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        GetComponent<Collider2D>().enabled = false;
        Debug.Log("Invincibility frames started");

        // Blink the player sprite for the duration of invincibility frames
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        for (float i = 0; i < iframesDuration; i += 0.1f)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        renderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isInvincible = false;
        Debug.Log("Invincibility frames ended");
    }

    private void die()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0.0f;
    }
}