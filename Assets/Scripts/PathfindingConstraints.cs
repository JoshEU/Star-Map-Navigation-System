using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This Class contains the logic for altering the Pathfinding Constraints as well as displaying them whilst the Nav Agent is Flying.
public class PathfindingConstraints : MonoBehaviour {
    // Script References:
    [SerializeField]
    private NavigatePath navigatePathScript;
    // Pathfinding Constraints prior to flying
    [SerializeField]
    private Slider shipSpeedSlider;
    [SerializeField]
    private TextMeshProUGUI shipSpeedCurrentValue;
    [SerializeField]
    public Slider fuelAmountSlider;
    [SerializeField]
    private TextMeshProUGUI fuelAmountCurrentValue;
    [SerializeField]
    private Slider maxJumpDistanceSlider;
    [SerializeField]
    private TextMeshProUGUI maxJumpDistanceCurrentValue;
    [SerializeField]
    private Slider returnToOriginSlider;
    // Pathfinding Constraints whilst flying
    [SerializeField]
    private TextMeshProUGUI currentShipSpeedValue;
    [SerializeField]
    private TextMeshProUGUI startingFuelAmountValue;
    [SerializeField]
    private TextMeshProUGUI currentMaxJumpDistanceValue;
    [SerializeField]
    private TextMeshProUGUI currentReturnToOriginValue;
    [SerializeField]
    private TextMeshProUGUI currentFuelRemainingText;
    public int maxJumpDistance = 5;

    public void ChangeShipSpeed() {
        navigatePathScript.flyingSpeed = shipSpeedSlider.value;
        shipSpeedCurrentValue.text = shipSpeedSlider.value.ToString();
    }
    public void ChangeFuelAmountValue() {
        navigatePathScript.fuelAmount = fuelAmountSlider.value;
        fuelAmountCurrentValue.text = fuelAmountSlider.value.ToString();
    }
    public void ChangeMaxJumpDistance() {
       maxJumpDistance = (int)maxJumpDistanceSlider.value;
        maxJumpDistanceCurrentValue.text = maxJumpDistanceSlider.value.ToString();
    }
    public void ChangeReturnToOriginValue() {
        if(returnToOriginSlider.value == 0) {
            NavigatePath.returnToOrigin = false;
        }
        else if (returnToOriginSlider.value == 1) {
            NavigatePath.returnToOrigin = true;
        }
    }
    // This Function resets the Pathfinding Constraints to their default values
    // Occurs once the ResetToDefaults Button is clicked
    public void ResetToDefaults() {
        shipSpeedSlider.value = 1;
        fuelAmountSlider.value = 10000;
        maxJumpDistanceSlider.value = 5;
        returnToOriginSlider.value = 0;
        ChangeShipSpeed();
        ChangeFuelAmountValue();
        ChangeMaxJumpDistance();
        ChangeReturnToOriginValue();
    }
    // This Function is called when the 'Fly Ship' button is clicked.
    // It will display a UI Panel showcasing all the Current Pathfinding Constraints 
    public void ShowCurrentConstraints() {
        // Retrieve the current Pathfinding Constraint values when the ship is flying and show these in the 'Flying UI Panel'
        currentShipSpeedValue.text = shipSpeedCurrentValue.text;
        startingFuelAmountValue.text = fuelAmountCurrentValue.text;
        currentMaxJumpDistanceValue.text = maxJumpDistanceCurrentValue.text;
        if(returnToOriginSlider.value == 1) {
            currentReturnToOriginValue.text = "YES";
        }
        else if(returnToOriginSlider.value == 0) {
            currentReturnToOriginValue.text = "NO";
        }
    }
	private void Update() {
        // Update how much Fuel the ship has remaining in real-time every frame
        currentFuelRemainingText.text = navigatePathScript.fuelAmount.ToString();
    }
}