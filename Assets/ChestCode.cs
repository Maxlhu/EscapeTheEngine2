using UnityEngine;
using UnityEngine.UI;

public class ChestCode : MonoBehaviour
{
    public string correctCode = "1234"; // The correct code to open the chest
    //public Text inputDisplay; // UI Text element to display the inputted code
    private string enteredCode = ""; // Stores the player's entered code
    private bool isChestOpen = false; // Track if the chest is already open

    public GameObject chestClosedSprite; // The sprite for the closed chest
    public GameObject chestOpenSprite; // The sprite for the open chest

    private void Start()
    {
        // Make sure the chest starts closed
        chestOpenSprite.SetActive(false);
        chestClosedSprite.SetActive(true);
    }

    // This function will be called whenever the player presses a digit button
    public void EnterDigit(string digit)
    {
        Debug.Log("Digit Received: " + digit);
        if (isChestOpen)
            return; // If the chest is open, don't allow further inputs

        // Append the digit to the enteredCode
        enteredCode += digit;

        //// Update the display text
        //inputDisplay.text = enteredCode;

        // If the code is 4 digits long, check if it's correct
        if (enteredCode.Length == 4)
        {
            CheckCode();
        }
    }

    // Checks if the entered code matches the correct code
    private void CheckCode()
    {
        if (enteredCode == correctCode)
        {
            OpenChest(); // Open the chest if the code is correct
        }
        else
        {
            ResetCode(); // Reset the input if the code is incorrect
        }
    }

    // Opens the chest
    private void OpenChest()
    {
        Debug.Log("Correct code! Chest is opening.");
        isChestOpen = true;

        // Change the chest's visual state
        chestClosedSprite.SetActive(false);
        chestOpenSprite.SetActive(true);

        //// Optionally disable further input
        //inputDisplay.text = "Unlocked";
    }

    // Resets the entered code
    private void ResetCode()
    {
        Debug.Log("Incorrect code, try again.");
        enteredCode = ""; // Clear the entered code
        //inputDisplay.text = ""; // Clear the display text
    }
}
