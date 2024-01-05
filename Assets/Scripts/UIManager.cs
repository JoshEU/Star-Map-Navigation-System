using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This Class contains the logic for managing the main parts of the UI
// Also contains the Handling of any Input Errors alongside their designated Error messages popping up on screen
public class UIManager : MonoBehaviour {
    [SerializeField]
    private BasicCameraMovement basicCameraMovementScript;
    [SerializeField]
	private DijkstrasPathfinding dijkstrasPathfindingScript;
    [SerializeField]
    private NavigatePath navigatePathScript;
    [SerializeField]
    private AudioManager audioManagerScript;
    public bool isPaused = false;
    public static bool canDoPathfinding = false;
    // Feature Panel UI:
    [SerializeField]
    private Button fixedViewBtn;
    [SerializeField]
    private Button dynamicViewBtn;
    [SerializeField]
    private Button flyShipBtnObj;
    [SerializeField]
    private Sprite defaultBtnSprite;
    [SerializeField]
    private Sprite pressedBtnSprite;
    [SerializeField]
    private GameObject pathfindingPanelObj;
    [SerializeField]
    public GameObject originUIPanel;
    [SerializeField]
    public GameObject destinationUIPanel;
    [SerializeField]
    private GameObject uiObj;
    [SerializeField]
    private GameObject quitConfirmationPanelObj;
    [SerializeField]
    private TextMeshProUGUI pathfindingErrorText;

    IEnumerator HideErrorTextDelay() {
        yield return new WaitForSeconds(2.5f);
        pathfindingErrorText.text = "";
    }
    // This function is called when the 'Pathfinding' Button is clicked inside the 'Feature Panel' UI
    // It checks whether both the 'Origin' star and the 'Destination' Star has been selected before proceeding with the Pathfinding Algorithm
    // If not it will produce and error message, which displays what the issue is
    public void CheckOriginAndDestinationSelection() {
        if (PickOriginDestination.hasPickedOriginStar == true && PickOriginDestination.hasPickedDestinationStar == true) {
            // Carries out the Pathfinding in DijkstrasPathfinding.cs script
            pathfindingPanelObj.SetActive(true); 
            flyShipBtnObj.interactable = true;
            canDoPathfinding = true;
        } 
        else if (PickOriginDestination.hasPickedOriginStar == true && PickOriginDestination.hasPickedDestinationStar == false) {
            // Plays Pathfinding Error SFX
            audioManagerScript.PathfindingErrorAudio();
            // Displays Error Message
            pathfindingErrorText.text = "ERROR: You Still Need to Select a Destination Star!";
            canDoPathfinding = false;
            flyShipBtnObj.interactable = false;
        } 
        else if (PickOriginDestination.hasPickedOriginStar == false && PickOriginDestination.hasPickedDestinationStar == true) {
            // Plays Pathfinding Error SFX
            audioManagerScript.PathfindingErrorAudio();
            // Displays Error Message
            pathfindingErrorText.text = "ERROR: You Still Need to Select an Origin Star!";
            canDoPathfinding = false;
            flyShipBtnObj.interactable = false;
        } 
        else if (PickOriginDestination.hasPickedOriginStar == false && PickOriginDestination.hasPickedDestinationStar == false) {
            // Plays Pathfinding Error SFX
            audioManagerScript.PathfindingErrorAudio();
            // Displays Error Message
            pathfindingErrorText.text = "ERROR: You Still Need to Select an Origin & Destination Star!";
            canDoPathfinding = false;
            flyShipBtnObj.interactable = false;
        }
        StartCoroutine(HideErrorTextDelay());
    }
    // This Function displays the Quit Confirmation Panel and slows down time when the 'ESC' key is pressed on the keyboard
    private void QuitConfirmationPanel() {
        // Check if the user was either in Dynamic Camera View or picking an Origin Star or a Destination Star prior to activating the Quit Confirmation panel
        // Disable Camera Movement & Set the cursor back to its previous state
        if (StateManager.currentState == 1 || StateManager.currentState == 2 || StateManager.currentState == 3) {
            // Disable Camera Movement
            basicCameraMovementScript.enabled = false;
            // Set Cursor back to None
            Cursor.lockState = CursorLockMode.None;
        }
        // If Self-Destruct Sequence is playing - stop time [so Co-routines don't continue to play]
        if (navigatePathScript.isSelfDestructing == true) {
            Time.timeScale = 0.0f;
        } 
        else if(navigatePathScript.isSelfDestructing == false) {
            Time.timeScale = 0.25f;
        }
        isPaused = true;
        uiObj.SetActive(false);
        quitConfirmationPanelObj.SetActive(true);
    }
    // This Function will exit the application upon being pressed
    public void QuitYesBtn() {
        Application.Quit();
    }
    // This Function will Hide the Quit Confirmation Panel and return to normal time
    public void QuitNoBtn() {
        isPaused = false;
        Time.timeScale = 1.0f;
        audioManagerScript.UnPauseAudio();
        // Checks if the Spaceship was commencing its Self-Destruct Sequence prior to displaying the Quit Confirmation Panel
        if (navigatePathScript.isSelfDestructing == true) {
            quitConfirmationPanelObj.SetActive(false);
        } 
        else if (navigatePathScript.isSelfDestructing == false) {
            quitConfirmationPanelObj.SetActive(false);
            uiObj.SetActive(true);
        }
        // Check if the user was either in Dynamic Camera View or picking an Origin Star or a Destination Star prior to activating the Quit Confirmation panel
        // Re-enable Camera Movement & Set the cursor back to its previous state only if they were picking a star and none of the Origin/Destination Panels were active 
        if (StateManager.currentState == 1 || StateManager.currentState == 2 && originUIPanel.activeSelf == false || StateManager.currentState == 3 && destinationUIPanel.activeSelf == false) {
            // Re-enable Camera Movement
            basicCameraMovementScript.enabled = true;
			// Set Cursor back to Locked
			Cursor.lockState = CursorLockMode.Locked;
		}
        else if (originUIPanel.activeSelf == true || destinationUIPanel.activeSelf == true) {
            // Set Cursor back to None
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Start() {
        GenerateStars.hasEverGeneratedStars = false;
    }
    void Update() {
        // Check if the User presses the Escape Key to Quit
        // IF they do then display the Quit Confirmation Panel & Pause any audio that was playing
        if (Input.GetKey(KeyCode.Escape)) {
            audioManagerScript.PauseAudio();
            QuitConfirmationPanel();
        }
        // Will only allow the changing between camera views after the first set of stars has been generaeted
        // This is because the user will not be able to enter the Dynamic View and roam around in the empty scene and potentially get lost
        if (GenerateStars.hasEverGeneratedStars == true) {
            // Set buttons color back to white to indicate that they are interactable (using either '1' or '2' on the keyboard)
            fixedViewBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
            dynamicViewBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
            // Only carry this out if the user isn't picking an 'Origin' or 'Destination' star
            if(PickOriginDestination.isPickingOrigin == false && PickOriginDestination.isPickingDestination == false) {
                // Change Camera to Fixed View - Simulates a button press when '1' key is pressed down
                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    StateManager.currentState = 0;
                    // Changes Fixed View buttons image to a pressed sprite - indicate to the user that they are selecting this button
                    fixedViewBtn.image.sprite = pressedBtnSprite;
                }   
                // Change Camera to Dynamic View - Simulates a button press when '2' key is pressed down
                else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    StateManager.currentState = 1;
                    // Changes Dynamic View buttons image to a pressed sprite - indicate to the user that they are selecting this button
                    dynamicViewBtn.image.sprite = pressedBtnSprite;
                }
                // Will change sprite back to default when either '1' or '2' on the keyboard is lifted up
                if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2)) {
                    fixedViewBtn.image.sprite = defaultBtnSprite;
                    dynamicViewBtn.image.sprite = defaultBtnSprite;
                } 
            }
            // Closes the Pathfinding Panel whilst the user is selecting either an Origin or Destination Star
            if(PickOriginDestination.isPickingOrigin == true || PickOriginDestination.isPickingDestination == true) {
                pathfindingPanelObj.SetActive(false);
            }
        }
    }
}