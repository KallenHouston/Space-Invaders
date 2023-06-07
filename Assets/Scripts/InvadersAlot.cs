using UnityEngine;
using UnityEngine.SceneManagement;

public class InvadersAlot : MonoBehaviour
{
    public Invaders[] pref;
    public int Rows = 5;
    public int Cols = 11;
    public AnimationCurve speed;
    public Bullet misslePrefab;
    public float missleRate = 1.0f;
    public int numberskilled { get; private set; }
    public int numbersalive => totalInvaders - numberskilled;
    public int totalInvaders => Rows * Cols;
    public float percentdeath => (float)numberskilled / (float)totalInvaders;

    private Vector3 Direction = Vector2.right;

    private void Awake()
    {
        for (int row = 0; row < Rows; row++)
        {
            float width = 2.0f * (Cols - 1);
            float height = 2.0f * (Rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            for (int col = 0; col < Cols; col++)
            {
                Vector3 position = new Vector3(centering.x + (col * 2.0f), centering.y + (row * 2.0f), 0.0f);

                Invaders invader = Instantiate(pref[row], transform);
                invader.dead += InvaderDeath;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(missle), missleRate, missleRate);
    }

    private void Update()
    {
        transform.position += Direction * speed.Evaluate(percentdeath) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Direction == Vector3.right && invader.position.x >= rightEdge.x - 1.0f)
            {
                Rowadvanced();
            }
            else if (Direction == Vector3.left && invader.position.x <= leftEdge.x + 1.0f)
            {
                Rowadvanced();
            }
        }
    }

    private void Rowadvanced()
    {
        Direction.x *= -1.0f;

        Vector3 pos = transform.position;
        pos.y -= 1.0f;
        transform.position = pos;
    }

    private void missle()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1.0f / (float)numbersalive))
            {
                Instantiate(misslePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderDeath()
    {
        numberskilled++;
        
        //add more later on:


    }
}
