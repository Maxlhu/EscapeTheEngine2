using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public ChestCode chestCode; // Reference to the ChestCode script

    // This function will be assigned to the button click
    public void OnButtonPress()
    {
        // Get the button name using gameObject.name
        string digit = gameObject.name.ToString();
        Debug.Log("Button Pressed: " + digit);

        if (digit != null)
        {
            // Pass the digit to your ChestCode script to handle the input
            chestCode.EnterDigit(digit);
        }
    }
}
