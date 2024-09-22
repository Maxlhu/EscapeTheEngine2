using UnityEngine;
using UnityEngine.UI; // Include this if you're using Unity's UI system for the button

public class ChangeSpriteOnEvent : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite defaultSprite; // The default sprite
    public Sprite alternativeSprite; // The sprite to change to when the event is triggered

    private void Start()
    {
        // Ensure the spriteRenderer is not null
        if (!spriteRenderer)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Set the default sprite at the start
        spriteRenderer.sprite = defaultSprite;
    }

    // This method will be called when the event is triggered
    public void ChangeSprite()
    {
        // Check if the current sprite is the default sprite
        if (spriteRenderer.sprite == defaultSprite)
        {
            // Change to the alternative sprite
            spriteRenderer.sprite = alternativeSprite;
        }
        else
        {
            // Otherwise, change back to the default sprite
            spriteRenderer.sprite = defaultSprite;
        }
    }
}