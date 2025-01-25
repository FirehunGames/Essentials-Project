using UnityEngine;
using TMPro;
using System; // Required for Type handling
using System.Collections;

public class UpdateCollectibleCount : MonoBehaviour
{
    private TextMeshProUGUI collectibleText; // Reference to the TextMeshProUGUI component

    // Reference to the LevelCompleted script
    public LevelCompleted bedroomCompleted;
    public LevelCompleted kitchenCompleted;

    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();
        if (collectibleText == null)
        {
            Debug.LogError("UpdateCollectibleCount script requires a TextMeshProUGUI component on the same GameObject.");
            return;
        }
        if (bedroomCompleted == null)
        {
            Debug.LogError("LevelCompleted script reference is not assigned.");
        }
        UpdateCollectibleDisplay(); // Initial update on start
    }

    void Update()
    {
        UpdateCollectibleDisplay();
    }

    public void UpdateCollectibleDisplay()
    {
        int totalCollectibles = 0;

        // Check and count objects of type Collectible
        Type collectibleType = Type.GetType("CollectibleBedroom");
        if (collectibleType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectibleType, FindObjectsSortMode.None).Length;
        }

        // Optionally, check and count objects of type Collectible2D as well if needed
        Type collectible2DType = Type.GetType("Collectible2D");
        if (collectible2DType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectible2DType, FindObjectsSortMode.None).Length;
        }

        if (totalCollectibles == 0)
        {
            collectibleText.text = "Congratulations, you can proceed to the next room";

            // Open the door
            if (bedroomCompleted != null)
            {
                bedroomCompleted.OpenDoor();
                kitchenCompleted.OpenDoor();
            }
            else
            {
                Debug.LogError("LevelCompleted script reference is null.");
            }
        }
        else
        {
            // Update the collectible count display
            collectibleText.text = $"Diamonds remaining: {totalCollectibles}";
        }
    }
}
