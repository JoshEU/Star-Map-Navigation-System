using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This Class will generate the chosen number of stars at random positions in a pre-defined area and of random properties in terms of its description
// Clicking it again will destory the current stars and re-generate them
public class GenerateStars : MonoBehaviour {
    // Script References
    [SerializeField]
    private BasicCameraMovement basicCameraMovementScript;
    [SerializeField]
    private StarManager starManagerScript;
    [SerializeField]
    private ClickStar clickStarScript;
    [SerializeField]
    private LineRenderers lineRenderersScript;
    [SerializeField]
    private DijkstrasPathfinding dijkstrasPathfindingScript;
    // Star Variables:
    [SerializeField]
    private GameObject[] starPrefabsArray;
    [SerializeField]
    private GameObject spawnAreaObj;
    [SerializeField]
    private Material starMaterial;
    public GameObject[] currentStarsGenerated;
    public List<Vector3> currentStarsPositions = new List<Vector3>();
    public GameObject[] lineRenderers;
    // UI References
    [SerializeField]
    private Button generateBtn;
    [SerializeField]
    private Slider numberOfStarsSlider;
    [SerializeField]
    private TextMeshProUGUI numberOfStarsText;
    [SerializeField]
    private TextMeshProUGUI shortestRouteText;
    // Bool Checks:
    private bool allStarsGenerated = true;
    public static bool drawLines = false;
    public static bool hasEverGeneratedStars = false;
    public static int numberOfStarsToGenerate;

    void Update() {
        numberOfStarsToGenerate = (int)numberOfStarsSlider.value;
        numberOfStarsText.text = numberOfStarsToGenerate.ToString();
		// Stops the user pressing the generate button again until all the stars have generated
        // Stops the user altering the number of stars to generate slider until all the stars have generated
        if (allStarsGenerated) {
            generateBtn.interactable = true;
            numberOfStarsSlider.interactable = true;
            lineRenderersScript.GenerateLines();
        }
        else if(allStarsGenerated == false) {
            generateBtn.interactable = false;
            numberOfStarsSlider.interactable = false;
        }
    }
    // Delete any Stars in the scene and generate new ones based on the users chosen amount of stars
    private IEnumerator SpawnStar() {
        // Check if any stars are currently generated in the star map - If true, then delete them before generating a new set of stars
        if(currentStarsGenerated.Length > 0) {
            foreach(GameObject star in currentStarsGenerated) {
                Object.Destroy(star);
            }
        }
        for (int i = 0; i < numberOfStarsToGenerate; i++) {
            yield return new WaitForSeconds(0.25f);
            InstantiateStars();
            currentStarsGenerated = GameObject.FindGameObjectsWithTag("Star"); 
        }
        allStarsGenerated = true;
        drawLines = true;
        // Finds all GameObjects with the tag 'Star' every time a Star is instantiated - Called once all stars have been generated in the scene
        starManagerScript.starObjectsArray = GameObject.FindGameObjectsWithTag("Star");
    }
    // Spawns a star in a random location with a random size, color and name.
    private void InstantiateStars() {
		// Current Spawning Parameters: Medium space right in front of the camera (Inside a Cube)
		float randX = Random.Range(-5.40f, 5.40f);
		float randY = Random.Range(-4.40f, 6.40f);
		float randZ = Random.Range(-5.40f, 5.40f);

		float randColorValues = Random.Range(0f, 1f);
		Vector3 randPos = new Vector3(randX, randY, randZ);
       
        Color newStarColor = new Color(r: Random.Range(0f, 1f), g: Random.Range(0f, 1f), b: Random.Range(0f, 1f));
        // Generate a random index which will decide the size of the star being generated in the star map
        int randomStarObject = Random.Range(0, 5);
        // Randomly picks a star from the Star Array (Each one will have a different name)
        GameObject newStarObj = Instantiate(starPrefabsArray[randomStarObject], randPos, starPrefabsArray[randomStarObject].transform.rotation);
        // Set its material color
        newStarObj.GetComponent<Renderer>().material.color = newStarColor;
        // Set the Emission color to be a random color (The star that is being hovered over)
        newStarObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(r: Random.Range(0f, 1f), g: Random.Range(0f, 1f), b: Random.Range(0f, 1f)));
    }
    // This function resets the properties of the stars when the 'Generate Stars' button is pressed
    // Resets both the Origin Star and the Destination Star as well as the Star Description Text
    private void ResetProperties() {
        ClickStar.originStarText = "";
        ClickStar.destinationStarText = "";
        clickStarScript.starTitle.text = "";
        clickStarScript.starNameText.text = "";
        clickStarScript.isHabitableText.text = "";
        clickStarScript.threatLevelText.text = "";
        clickStarScript.connectionNumberText.text = "";
    }
    // Generates a new set of Stars when the button is clicked
    public void GenerateNewStars() {
        // Only destory shortestPath Line Renderer if stars have been generated before in the current session
        if(hasEverGeneratedStars == true) {
            Destroy(GameObject.FindGameObjectWithTag("ShortestPathLR"));
        }
        currentStarsPositions.Clear();
        allStarsGenerated = false;
        hasEverGeneratedStars = true;
        // Reset the bools for the Origin and Destination stars being selected
        PickOriginDestination.hasPickedOriginStar = false;
        PickOriginDestination.hasPickedDestinationStar = false;
        ResetProperties();
        // Removes the stars from the List 
        starManagerScript.starPositionsList.RemoveRange(0, starManagerScript.starPositionsList.Count); 
        // Removes Line Renderer
        lineRenderersScript.lineRenderer.positionCount = 0;
        // Changes the line renderers 'positionCount' to the 'number of stars' selected using the slider in the UI
        lineRenderersScript.lineRenderer.positionCount = (int)numberOfStarsSlider.value;
        StarManager.numberOfStarConnectionsList.Clear();
        shortestRouteText.text = null;
        BasicCameraMovement.camRotation = Vector3.zero;
        StartCoroutine(SpawnStar());
    }
}