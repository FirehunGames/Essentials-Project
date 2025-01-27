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
    public LevelCompleted roomCompleted;

    // Cached types and total counts
    private Type collectible2DType;
    private Type collectibleBedroomType;
    private Type collectibleKitchenType;
    private Type collectibleRoomType;

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
            Debug.LogError("Bedroom LevelCompleted script reference is not assigned.");
        }
        if (kitchenCompleted == null)
        {
            Debug.LogError("Kitchen LevelCompleted script reference is not assigned.");
        }
        if (roomCompleted == null)
        {
            Debug.LogError("Room LevelCompleted script reference is not assigned.");
        }

        // Cache the types once
        collectible2DType = Type.GetType("Collectible2D");
        collectibleBedroomType = Type.GetType("CollectibleBedroom");
        collectibleKitchenType = Type.GetType("CollectibleKitchen");
        collectibleRoomType = Type.GetType("CollectibleRoom");

        // Initial update on start
        UpdateCollectibleDisplay();
    }

    void Update()
    {
        // Here you might want to add a condition or event to call UpdateCollectibleDisplay, instead of calling it every frame
        UpdateCollectibleDisplay();
    }

    public void UpdateCollectibleDisplay()
    {
        int totalBedroomCollectibles = 0;
        int totalCollectibles = 0;
        int totalKitchenCollectibles = 0;
        int totalRoomCollectibles = 0;

        // Optionally, check and count objects of type Collectible2D as well if needed
        if (collectible2DType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectible2DType, FindObjectsSortMode.None).Length;
        }

        // Check and count objects of type CollectibleBedroom
        if (collectibleBedroomType != null)
        {
            totalBedroomCollectibles += UnityEngine.Object.FindObjectsByType(collectibleBedroomType, FindObjectsSortMode.None).Length;
        }

        // Check and count objects of type CollectibleKitchen
        if (collectibleKitchenType != null)
        {
            totalKitchenCollectibles += UnityEngine.Object.FindObjectsByType(collectibleKitchenType, FindObjectsSortMode.None).Length;
        }

        if (collectibleRoomType != null)
        {
            totalRoomCollectibles += UnityEngine.Object.FindObjectsByType(collectibleRoomType, FindObjectsSortMode.None).Length;
        }

        if (totalBedroomCollectibles == 0)
        {

            // Open the door to the bedroom
            if (bedroomCompleted != null)
            {
                bedroomCompleted.OpenDoor();
            }
            else
            {
                Debug.LogError("LevelCompleted script reference is null.");
            }

            if (totalKitchenCollectibles == 0)
            {
                //collectibleText.text = "Congratulations, you can proceed to the next room";

                // Open the door to the kitchen
                if (kitchenCompleted != null)
                {
                    kitchenCompleted.OpenDoor();
                }
                else
                {
                    Debug.LogError("LevelCompleted script reference is null.");
                }

                if (totalRoomCollectibles == 0)
                {
                    if (roomCompleted != null)
                    {
                        roomCompleted.OpenDoor();
                    }
                    else
                    {
                        Debug.LogError("LevelCompleted script reference is null.");
                    }                
                }
                else
                {
                    collectibleText.text = $"Coins remaining: {totalRoomCollectibles}";
                }

            }
            else
            {
                collectibleText.text = $"Fish remaining: {totalKitchenCollectibles}";
            }
        }
        else
        {
            // Update the collectible count display
            collectibleText.text = $"Diamonds remaining: {totalBedroomCollectibles}";
        }
    }
}
