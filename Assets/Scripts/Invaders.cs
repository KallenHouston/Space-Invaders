using UnityEngine;

public class Invaders : MonoBehaviour
{
    // An array of sprites for the animation frames
    public Sprite[] Animation;

    // The time between animation frames
    public float animTime;

    public System.Action dead;

    // A reference to the sprite renderer component
    private SpriteRenderer spriteRenderer;

    // The current animation frame index
    private int animationFrame; 

    private void Awake()
    {
        // Get the sprite renderer component attached to this game object
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void Start()
    {
        // Start a repeating timer to call the AnimationSprite method with the specified delay and repeat rate
        InvokeRepeating("AnimationSprite", animTime, animTime);
    }

    private void AnimationSprite()
    {
        // If the animation frame index is greater than or equal to the length of the Animation array,
        // reset it to 0 to start over
        if (animationFrame >= Animation.Length)
        {
            animationFrame = 0;
        }

        // Set the sprite renderer's sprite to the current animation frame
        spriteRenderer.sprite = Animation[animationFrame];

        // Increment the animation frame index
        animationFrame++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet")) 
        {
            dead.Invoke();
            gameObject.SetActive(false);
        }
    }
}