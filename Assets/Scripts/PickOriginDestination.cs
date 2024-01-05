using UnityEngine;
using TMPro;

// This Class carries out the logic for picking an Origin & Destination Star
public class PickOriginDestination : MonoBehaviour {
    public static bool isPickingOrigin = false;
    public static bool isPickingDestination = false;
    // Dynamic Camera Variables:
    [SerializeField]
    private GameObject dynamicViewCamera;
    private Vector3 camStartPos = new Vector3(0, 1, -17.5f);
    private Vector3 camStartRotation = Vector3.zero;
    // Script References:
    [SerializeField]
    private BasicCameraMovement basicCameraMovementScript;
    [SerializeField]
    private ClickStar clickStarScript;
    // UI References:
    [SerializeField]
    private GameObject originUIPanel;
    [SerializeField]
    private GameObject destinationUIPanel;
    [SerializeField]
    public TextMeshProUGUI currentOriginText;
    [SerializeField]
    public TextMeshProUGUI currentDestinationText;
    [SerializeField]
    private GameObject navAgent;
    public static GameObject originObj;
    public static GameObject destinationObj;
    public static bool hasPickedOriginStar = false;
    public static bool hasPickedDestinationStar = false;

    void Update() {
        // Change currentOrigin Text to star that was confirmed as being the origin star
        currentOriginText.text = "Origin Star: " + ClickStar.originStarText;
        // Change currentDestination Text to star that was confirmed as being the destination star
        currentDestinationText.text = "Destination Star: " + ClickStar.destinationStarText;
    }
    public void PickOriginStar() {
        // Picking Origin + Dynamic Camera View
        Cursor.lockState = CursorLockMode.Locked;
        StateManager.currentState = 2;
        isPickingOrigin = true;
        isPickingDestination = false;
    }
    public void PickDestinationStar() {
        // Picking Destination + Dynamic Camera View
        Cursor.lockState = CursorLockMode.Locked;
        StateManager.currentState = 3;
        isPickingOrigin = false;
        isPickingDestination = true;
    }
    // Check which UI is enabled, then disable it (for the confirmation UI related to picking and 'Origin' or 'Destination' Star).
    // Also disable the originDestinationUIPanel and re-enable the FeaturesUILeftPanel.
    private void CheckUIActive() {
		if (originUIPanel.activeSelf == true) {
            originUIPanel.SetActive(false);
        }
        else if( destinationUIPanel.activeSelf == true) {
            destinationUIPanel.SetActive(false);
        }
	}
    // Called when the 'Yes' button is clicked to confirm the choice of either the 'Origin' or 'Destination' Star.
    public void YesButton() {
        BasicCameraMovement.camRotation = Vector3.zero;
        // Reset Dynamic Cameras 'position' and 'rotation' values ready for next usage
        dynamicViewCamera.transform.position = camStartPos;
        dynamicViewCamera.transform.eulerAngles = BasicCameraMovement.camRotation;
        // Enable Scripts again
        basicCameraMovementScript.enabled = true;
		clickStarScript.enabled = true;
        // Change to Fixed Camera State
		StateManager.currentState = 0;
        CheckUIActive();

        // Display 'Origin or 'Destination' text above confirmed star.
        if (isPickingOrigin) {
            // Initialise Array 
            TMP_Text[] allStarHoverTextArray = FindObjectsOfType<TMP_Text>();
            // Retrieve all TMP_Text components in the scene
            foreach (TMP_Text currentStarHoverText in allStarHoverTextArray) {
                // Narrow it down so it only check the ones that have the text 'Origin'
                if (currentStarHoverText.text == "Origin") {
                    // Remove text from the previous 'Origin Star'
                    currentStarHoverText.text = "";
                }
                // Check if the star being clicked on is currently the 'Destination' star
                // If true, then overwrite it and set it as the 'Origin' star
                if (ClickStar.clickedOnStar.GetComponentInChildren<TMP_Text>().text == "Destination") {
                    ClickStar.destinationStarText = "";
                    currentStarHoverText.text = "";
                    // Set to false as the 'Origin Star' is overiding the 'Destination star'
                    hasPickedDestinationStar = false;
                }
            }                
            // Add hover text to the newly selected 'Origin Star'
            ClickStar.clickedOnStar.GetComponentInChildren<TMP_Text>().text = "Origin";
            // Assign the 'Origin' Star GameObject to be the star that has just been confirmed as the Origin star - to be accessed when carrying out the pathfinding
            originObj = ClickStar.clickedOnStar;
            // Tell DijkstrasPathfinding.cs Script that an Origin Star has been selected
            hasPickedOriginStar = true;
            // Set Nav Agent to Origin Position when it has been confirmed
            navAgent.transform.position = originObj.transform.position;
        }
        else if (isPickingDestination) {
            // Initialise Array 
            TMP_Text[] allStarHoverTextArray = FindObjectsOfType<TMP_Text>();
            // Retrieve all TMP_Text components in the scene
            foreach (TMP_Text currentStarHoverText in allStarHoverTextArray) {
                // Narrow it down so it only check the ones that have the text 'Destination'
                if (currentStarHoverText.text == "Destination") {
                    // Remove text from the previous 'Destination Star'
                    currentStarHoverText.text = "";
                }
                // Check if the star being clicked on is currently the 'Origin' star
                // If true, then overwrite it and set it as the 'Destination' star
                if (ClickStar.clickedOnStar.GetComponentInChildren<TMP_Text>().text == "Origin") {
                    ClickStar.originStarText = "";
                    currentStarHoverText.text = "";
                    // Set to false as the 'Destination Star' is overiding the 'Origin star'
                    hasPickedOriginStar = false;
                }
            }
            // Add hover text to the newly selected 'Destination Star'
            ClickStar.clickedOnStar.GetComponentInChildren<TMP_Text>().text = "Destination";
            // Assign the 'Destination' Star GameObject to be the star that has just been confirmed as the Destination star - to be accessed when carrying out the pathfinding
            destinationObj = ClickStar.clickedOnStar;
            // Tell DijkstrasPathfinding.cs Script that a Destination Star has been selected
            hasPickedDestinationStar = true;
        }
        // So Fixed View and Dynamic View states can be accessed
        isPickingOrigin = false;
        isPickingDestination = false;
    }
    // Called when the 'No' button is clicked to revert the choice of either the 'Origin' or 'Destination' Star.
    public void NoButton() {
        basicCameraMovementScript.enabled = true;
        clickStarScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        CheckUIActive();
        // Set the star text back to null if the user declined picking the star as either the 'Origin' or 'Destination'.
		if (isPickingOrigin) {
            ClickStar.originStarText = "";
		}
        else if (isPickingDestination) {
            ClickStar.destinationStarText = "";
		}
    }
}