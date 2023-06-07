using UnityEngine;

public class Bunkers : MonoBehaviour
{
    public int health = 3;
    public GameObject splatPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missle") || other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            health--;
            Vector3 impactPos = other.transform.position;
            Instantiate(splatPrefab, impactPos, Quaternion.identity, transform);
            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}