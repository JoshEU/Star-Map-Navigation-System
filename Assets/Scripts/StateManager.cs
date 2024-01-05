using UnityEngine;
using UnityEngine.UI;

// This Class manages the different states that the user can be in. 
// Related to the various different camera states etc.
public class StateManager : MonoBehaviour {
    [SerializeField]
    private UIManager uiManagerScript;
    [SerializeField]
    private GameObject fixedCameraView;
    [SerializeField]
    private GameObject dynamicCameraView;
    [SerializeField]
    GameObject crosshairObj;
    [SerializeField]
    private Button pathfindingBtnObj;
    [SerializeField]
    private Button flyShipBtnObj;
    public static int currentState = 0; // Used in the switch case - initialised to 0 (fixed camera state) by default
    private const int fixedCameraState = 0; // Camera can move along a fixed path surrounding the stars
    private const int dynamicCameraState = 1; // Camera can move freely inside the scene
    private const int pickOriginState = 2; // Camera can move freely inside the scene and can select a star to be the origin point
    private const int pickDestinationState = 3; // Camera can move freely inside the scene and can select a star to be the destination point

    void Start() {
        currentState = fixedCameraState;
    }
    // Checks which state the user is in every frame
    void Update() {
		switch (currentState) {
            case fixedCameraState:
                Cursor.lockState = CursorLockMode.None;
                dynamicCameraView.SetActive(false);
                fixedCameraView.SetActive(true);
                crosshairObj.SetActive(false);
                break;
            case dynamicCameraState:
                if(uiManagerScript.isPaused == false) {
                    Cursor.lockState = CursorLockMode.Locked;
                    fixedCameraView.SetActive(false);
                    dynamicCameraView.SetActive(true);
                    crosshairObj.SetActive(true);
                }
                break;
            case pickOriginState:
                fixedCameraView.SetActive(false);
                dynamicCameraView.SetActive(true);
                flyShipBtnObj.interactable = false;
                pathfindingBtnObj.interactable = true;
                break;
            case pickDestinationState:
                fixedCameraView.SetActive(false);
                dynamicCameraView.SetActive(true);
                flyShipBtnObj.interactable = false;
                pathfindingBtnObj.interactable = true;
                break;
        }
    }
}