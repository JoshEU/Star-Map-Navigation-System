using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This Class contains the logic neccessary for an AI Nav Agent to follow the Shortest Path once it has been found
public class NavigatePath : MonoBehaviour {
    // Script References:
    [SerializeField]
    private PathfindingConstraints pathfindingConstraintsScript;
    [SerializeField]
    private UIManager uiManagerScript;
    [SerializeField]
    private UIEasing uiEasingScript;
    [SerializeField]
    private NPCSpawner npcSpawnerScript;
    [SerializeField]
    private AudioManager audioManagerScript;
    // UI References:
    [SerializeField]
    private Button pickOriginBtn;
    [SerializeField]
    private Button pickDestinationBtn;
    [SerializeField]
    private Button doPathfindingBtn;
    [SerializeField]
    private GameObject featuresPanelObj;
    [SerializeField]
    private GameObject featuresPanelTitleObj;
    [SerializeField]
    private GameObject starDescriptionPanelObj;
    [SerializeField]
    private GameObject flyingInProgressPanelObj;
    [SerializeField]
    private GameObject selfDestructPanelObj;
    [SerializeField]
    private TextMeshProUGUI countdownText;
    [SerializeField]
    private GameObject uiObj;
    // Particle System References:
    [SerializeField]
    private ParticleSystem explosionPS;
    [SerializeField]
    private ParticleSystem rocketBoostPS;
    // Nav Agent Variables:
    public List<Vector3> shipFlyRoute = new List<Vector3>();
    public float flyingSpeed = 1.0f;
    public float fuelAmount = 10000.0f;
    public static bool returnToOrigin = false;
    public bool isSelfDestructing = false;
    private bool moveShip = false;
    private bool hasArrivedAtDestination = false;
    private int currentStarOnRoute = 0;
    private int totalStarsInRoute = 0;
    private int countdownNumber = 3;

	private void ResetProperties() {
        // Gets the total amount of stars in the route that the Nav Agent will be visiting
        totalStarsInRoute = shipFlyRoute.Count;
        // Reset Nav Agent path
        currentStarOnRoute = 0;
        // Reset Nav Agent Position
        gameObject.transform.position = PickOriginDestination.originObj.transform.position; 
        // Reset bool
        hasArrivedAtDestination = false;
        // Make Nav Agent Visible
        gameObject.SetActive(true);
        // Reset Fuel Amount
        fuelAmount = pathfindingConstraintsScript.fuelAmountSlider.value;
    }
    // This Function is executed once the FlyShip Button in the UI is clicked
    public void FlyShip() {
        npcSpawnerScript.canSpawnSpaceship = false;
        ResetProperties();
        StartCoroutine(LiftOffDelay());
    }
    // This Co-routine contains the Self-Destruct sequence logic, which occurs once the Ships fuel tank is empty
    private IEnumerator SelfDestructSequence() {
        // Stop Rocket Boost SFX
        audioManagerScript.StopRocketBoostAudio();
        // Play SelfDestructSequence SFX
        audioManagerScript.SelfDestructSequenceAudio();
        isSelfDestructing = true;
        // Stop displaying the Rocket Boost VFX
        rocketBoostPS.Stop();
        // Hide the Main UI
        uiObj.SetActive(false);
        // Wait for SelfDestructSequence SFX to stop playing
        yield return new WaitForSeconds(3.1f);
        //Show the Self-destruct panel only
        selfDestructPanelObj.SetActive(true);
        // Play SelfDestructCountdown SFX
        audioManagerScript.SelfDestructCountdownAudio();
        // Commence Countdown: 3,2,1,0
        countdownNumber = 3;
        yield return new WaitForSeconds(1.0f);
        countdownNumber = 2;
        yield return new WaitForSeconds(1.0f);
        countdownNumber = 1;
        yield return new WaitForSeconds(1.0f);
        countdownNumber = 0;
        // Play Explosion SFX
        audioManagerScript.ExplosionAudio();
        yield return new WaitForSeconds(1.0f);

        selfDestructPanelObj.SetActive(false); // Hide the Self Destruct Panel 
        explosionPS.Play();
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2.01f); // Wait for the Explosion VFX & SFX to end
        // Hide the Flying In Progress panel
        flyingInProgressPanelObj.SetActive(false);
        // Show the Main UI again
        isSelfDestructing = false;
        uiObj.SetActive(true);
        // Ease In the Features Panel & Star Description Panel
        uiEasingScript.ShowFeaturesPanel();
        uiEasingScript.ShowPathfindingFeaturesPanel();
        uiEasingScript.pathfindingPanelFlyingObj.SetActive(false);
        uiEasingScript.ShowStarDescriptionPanel();
        featuresPanelObj.SetActive(true);
        starDescriptionPanelObj.SetActive(true);
        featuresPanelObj.GetComponent<CanvasGroup>().interactable = true;
        starDescriptionPanelObj.GetComponent<CanvasGroup>().interactable = true;
        // Hide SpaceShip
        gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        // Set its position back to the Origin Star & Set its rotation to point to the first star to visit on route
        gameObject.transform.position = PickOriginDestination.originObj.transform.position;
        gameObject.transform.LookAt(shipFlyRoute[1], Vector3.back);
        //Re-enable the interaction for buttons
        pickOriginBtn.interactable = true;
        pickDestinationBtn.interactable = true;
        // Allow NPC Ships to spawn in
        npcSpawnerScript.canSpawnSpaceship = true;
    }
    private IEnumerator LiftOffDelay() {
        yield return new WaitForSeconds(0.20f);
        moveShip = true;
        // Play Rocket Boost SFX
        audioManagerScript.RocketBoostAudio();
    }
    private void CheckIfReachedDestination() {
        // Check when the Nav Agent has reached the Destination star
        if (gameObject.transform.position == shipFlyRoute[shipFlyRoute.Count - 1]) {
            if (returnToOrigin == true) {
                // Backtrack the route just taken, starting with the star before the destinaion star
                currentStarOnRoute = shipFlyRoute.Count - 2;
                hasArrivedAtDestination = true;
                // Allow NPC Ships to spawn in
                npcSpawnerScript.canSpawnSpaceship = true;
            } else if (returnToOrigin == false) {
                hasArrivedAtDestination = true;
                moveShip = false;
                // Allow NPC Ships to spawn in
                npcSpawnerScript.canSpawnSpaceship = true;
                // Make UI interactable
                pickOriginBtn.interactable = true;
                pickDestinationBtn.interactable = true;
                // Hide the Flying In Progress panel
                flyingInProgressPanelObj.SetActive(false);
                // Show the Features panel
                featuresPanelObj.SetActive(true);
                featuresPanelTitleObj.SetActive(true);
            }
        }
    }
    private void CheckIfReachedCurrentStarOnRoute() {
        // Check if has reached its next star on route and if it has, then set its next star to be the one it is supposed to travel to to reach the destination star.
        if (gameObject.transform.position == shipFlyRoute[currentStarOnRoute] && hasArrivedAtDestination == false) {
            currentStarOnRoute = currentStarOnRoute + 1;
            // Make UI un-interactable
            pickOriginBtn.interactable = false;
            pickDestinationBtn.interactable = false;
            doPathfindingBtn.interactable = false;
        }
    }
    private void ReturnToOriginStar() {
        // Backtracks to the Origin Star from the Destination Star
        if (hasArrivedAtDestination == true && returnToOrigin == true) {
            if (gameObject.transform.position == shipFlyRoute[currentStarOnRoute]) {
                currentStarOnRoute = currentStarOnRoute - 1;
                if (gameObject.transform.position == PickOriginDestination.originObj.transform.position) {
                    // Stop Rocket Boost SFX
                    audioManagerScript.StopRocketBoostAudio();
                    // Play ReachedDestination SFX
                    audioManagerScript.ReachedDestinationAudio();
                    // Ease Out the Flying Panel
                    uiEasingScript.HideFlyingPanel();
                    uiEasingScript.HidePathfindingFlyingPanel();
                    // Ease In the Features Panel
                    uiEasingScript.ShowFeaturesPanel();
                    uiEasingScript.ShowPathfindingFeaturesPanel();
                    featuresPanelObj.SetActive(true);
                    featuresPanelObj.GetComponent<CanvasGroup>().interactable = true;
                    // Hide the SpaceShip
                    gameObject.SetActive(false);
                    gameObject.transform.LookAt(shipFlyRoute[1], Vector3.back);
                    moveShip = false;
                    // Make UI interactable
                    pickOriginBtn.interactable = true;
                    pickDestinationBtn.interactable = true;
                    // Hide the Flying In Progress panel
                    flyingInProgressPanelObj.SetActive(false);
                    // Show the Features panel
                    featuresPanelObj.SetActive(true);
                    featuresPanelTitleObj.SetActive(true);
                }
            }
        }
    }
    private void ConsumeFuel() {
        // Checks if the SpaceShip is in motion and what its Flight Speed is set to.
        // This will determine the rate at which it depletes fuel [Slower = consume less | Faster = consume more]
        if (moveShip == true) {
            switch (flyingSpeed) {
                case 1:
                    fuelAmount -= 100 * Time.deltaTime;
                    break;
                case 2:
                    fuelAmount -= 1500 * Time.deltaTime;
                    break;
                case 3:
                    fuelAmount -= 2500 * Time.deltaTime;
                    break;
                case 4:
                    fuelAmount -= 5000 * Time.deltaTime;
                    break;
                case 5:
                    fuelAmount -= 7500 * Time.deltaTime;
                    break;
            }
            if (fuelAmount <= 0.0f) {
                // Visibly set it to 0 fuel
                fuelAmount = 0.0f;
                moveShip = false;
                StartCoroutine(SelfDestructSequence());
            }
        }
    }
    private void FixedUpdate() {
        // Make Nav Agent Move on route to the Destination Star
        if (moveShip) {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, shipFlyRoute[currentStarOnRoute], Time.deltaTime * flyingSpeed);
            gameObject.transform.LookAt(shipFlyRoute[currentStarOnRoute], Vector3.back);
        } 
    }
    void Update() {
        CheckIfReachedDestination();

        CheckIfReachedCurrentStarOnRoute();

        ReturnToOriginStar();

        ConsumeFuel();

        // Updates the countdown number in real-time for the Self-Destruct Sequence
        countdownText.text = countdownNumber.ToString();
    }
    // Hides the Nav Agent when the it has arrived at the destination and stays on the Destination stars trigger
    private void OnTriggerStay(Collider collision) {
        if (collision.gameObject.name == PickOriginDestination.destinationObj.name && hasArrivedAtDestination == true && returnToOrigin == false) {
            // Stop Rocket Boost SFX
            audioManagerScript.StopRocketBoostAudio();
            // Play ReachedDestination SFX
            audioManagerScript.ReachedDestinationAudio();
            // Ease Out the Flying Panel
            uiEasingScript.HideFlyingPanel();
            uiEasingScript.HidePathfindingFlyingPanel();
            // Ease In the Features Panel
            uiEasingScript.ShowFeaturesPanel();
            uiEasingScript.ShowPathfindingFeaturesPanel();
            // Hide the SpaceShip
            gameObject.SetActive(false);
            // Set Nav Agent at Origin Star Position & Set its rotation to look at the first star on route - needed in case the user doesn't change the Origin and Destination Star
            gameObject.transform.position = shipFlyRoute[0];
            gameObject.transform.LookAt(shipFlyRoute[1], Vector3.back);
            // Allow the user to choose either a new Origin or Destination Star once the Nav Agent has reached their destination
            pickOriginBtn.interactable = true;
            pickDestinationBtn.interactable = true;
        }
    }
}