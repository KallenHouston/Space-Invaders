using UnityEngine;

public class Boss : MonoBehaviour
{
    // The boss ship's health
    public int health = 3;

    // The action to perform when the boss is destroyed
    public System.Action dead;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            health--;

            Debug.Log($"Boss health: {health}");

            if (health <= 0)
            {
                Debug.Log("Boss destroyed!");

                // Destroy the boss and perform the dead action
                if (dead != null)
                {
                    dead.Invoke();

                }
                gameObject.SetActive(false);
                Time.timeScale = 0.0f;
            }
        }
    }
}