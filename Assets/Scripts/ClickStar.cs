using UnityEngine;
using TMPro;

// This Class handles when the user is clicking on a star to see its description
public class ClickStar : MonoBehaviour {
    // Star Properties:
    [SerializeField]
    public TextMeshProUGUI starTitle; // Accessed inside GenerateStas.cs script
    [SerializeField]
    public TextMeshProUGUI starNameText; // Accessed inside GenerateStas.cs script
    [SerializeField]
    public TextMeshProUGUI isHabitableText; // Accessed inside GenerateStas.cs script
    [SerializeField]
    public TextMeshProUGUI threatLevelText; // Accessed inside GenerateStas.cs script
    [SerializeField]
    public TextMeshProUGUI connectionNumberText; // Accessed inside GenerateStas.cs script
    // Script References:
    [SerializeField]
    private BasicCameraMovement basicCameraMovementScript;
    [SerializeField]
    private ClickStar clickStarScript;
    [SerializeField]
    private AudioManager audioManagerScript;
    // UI References:
    [SerializeField]
    private TextMeshProUGUI originConfirmationText;
    [SerializeField]
    public GameObject originUIPanel;
    [SerializeField]
    private TextMeshProUGUI destinationConfirmationText;
    [SerializeField]
    public GameObject destinationUIPanel;
    // Other Variables:
    public static string originStarText; // Accessed inside PickOriginDestination.cs script & GenerateStars.cs script
    public static string destinationStarText; // Accessed inside PickOriginDestination.cs script & GenerateStars.cs script
    public static GameObject clickedOnStar;
    public static bool isConfirmationUIEnabled = false;
    
	void Update() {
        // Checks if the User has pressed the 'LMB' and is hovering over a star
        if (Input.GetMouseButtonDown(0)) {
            // Shoots a ray on the mouse pointers position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100000f)) {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 5.0f);
                // Displays a stars description
                if (hit.collider.gameObject.tag == "Star") {
                    // Play Star Click SFX
                    audioManagerScript.StarClickAudio();
                    // Prints the stars name as the title
                    starTitle.text = hit.transform.gameObject.GetComponent<Star>().starName;
                    // Prints the stars name
                    starNameText.text = "Name of star clicked on: " + hit.transform.gameObject.GetComponent<Star>().starName;
                    // Prints the stars Habitable status
                    isHabitableText.text = "Habitable: " + hit.transform.gameObject.GetComponent<Star>().isHabitable;
                    // Prints the stars Threat level
                    threatLevelText.text = "Threat Level: " + hit.transform.gameObject.GetComponent<Star>().threatLevel;
                    // Prints Connection Number 
                    connectionNumberText.text = "Connections: " + hit.transform.gameObject.GetComponent<Star>().numberOfConnections;

                    // Called when the user is either picking an 'Origin' or 'Destination' Star and they click on a star [they must be in the Dynamic Camera State as well]
                    if (StateManager.currentState == 2 && PickOriginDestination.isPickingOrigin == true) {
                        // Show Confirmation UI for origin
                        originUIPanel.SetActive(true);
                        originConfirmationText.text = "Are you sure you want to pick '" + hit.transform.gameObject.GetComponent<Star>().starName + "' as the Origin?";
                        Cursor.lockState = CursorLockMode.None;
                        basicCameraMovementScript.enabled = false;
                        clickStarScript.enabled = false;
                        // Set the star in the origin confirmation text to be the one just clicked on
                        originStarText = hit.transform.gameObject.GetComponent<Star>().starName;
                    } 
                    else if (StateManager.currentState == 3 && PickOriginDestination.isPickingDestination == true) {
                        // Show Confirmation UI for destination
                        destinationUIPanel.SetActive(true);
                        destinationConfirmationText.text = "Are you sure you want to pick '" + hit.transform.gameObject.GetComponent<Star>().starName + "' as the Destination?";
                        Cursor.lockState = CursorLockMode.None;
                        basicCameraMovementScript.enabled = false;
                        clickStarScript.enabled = false;
                        // Set the star in the destination confirmation text to be the one just clicked on
                        destinationStarText = hit.transform.gameObject.GetComponent<Star>().starName;
                    }
                    clickedOnStar = hit.transform.gameObject;
                }
            }
        }
        // Checks if the picking of an Origin or Destination Star confirmation panel is enabled
        if(originUIPanel.activeSelf || destinationUIPanel.activeSelf) {
            isConfirmationUIEnabled = true;
        } else {
            isConfirmationUIEnabled = false;
        }
    }
}