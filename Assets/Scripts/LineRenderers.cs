using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This Class Generates the physical connections between stars using line renderers
public class LineRenderers : MonoBehaviour {
	[SerializeField]
	private StarManager starManagerScript;
	[SerializeField]
	private DijkstrasPathfinding dijkstrasPathfindingScript;
	[SerializeField]
	public LineRenderer lineRenderer;
	[SerializeField]
	private LineRenderer lineRendererPrefab;
	[SerializeField]
	private Slider numOfStarsToGenerateSlider;

	void Start() {
		lineRenderer = GetComponent<LineRenderer>();
		// Defines line renderers starting and ending color 
		lineRenderer.startColor = Color.blue;
		lineRenderer.endColor = Color.cyan;
	}
	private void GetStarPositions() {
		for (int i = 0; i < starManagerScript.starObjectsArray.Length; i++) {
			// Add each stars position to the 'starPositionsList'
			starManagerScript.starPositionsList.Add(starManagerScript.starObjectsArray[i].transform.position);
		}
	}
	// This Function retrieves each star and calls its function to store its neighbour positions inside a list
	private void GetEachStar() {
		// Stores each stars Neighbour Positions
		GameObject[] starScripts = GameObject.FindGameObjectsWithTag("Star");
		foreach (GameObject starObj in starScripts) {
			starObj.GetComponent<Star>().StoreNeighbourPositions();
		}
	}
	public void GenerateLines() {
		// Loop through each star object in the 'starObjectsArray'
		// Every iteration, add the star objects position to 'starPositionsList'
		// Set the position of the line renderer to be the next elements position
		// This will draw lines between each star that is generated
		if (GenerateStars.drawLines) {
			// Firstly, get all of the stars positions
			GetStarPositions();
			// Executes until the starPositionList is greater than or equal to the number of starObjects currently in the scene.
			if (starManagerScript.starPositionsList.Count <= starManagerScript.starObjectsArray.Length) {
				for (int i = 0; i < starManagerScript.starObjectsArray.Length; i++) {
					// Create a local list for this stars current neighbour positions
					List<Vector3> currentStarNeighbourPositions = new List<Vector3>();
					// Draw lines between the current star in correlation to how many connections it has been assigned to have
					for (int j = 0; j < StarManager.numberOfStarConnectionsList[i]; ++j) {
						// Instantiate a Line Renderer to link the current Star to a neighbour star
						LineRenderer newLR = Instantiate(lineRendererPrefab, lineRenderer.transform.position, Quaternion.identity);
						// Make the Line Renderer a child of the current object we are setting connections for
						newLR.transform.SetParent(starManagerScript.starObjectsArray[i].transform);
						// Assign Max positions to 2: A start & end position
						newLR.positionCount = 2;
						// Set Starting Position to the Current Star in the iteration
						newLR.SetPosition(0, starManagerScript.starPositionsList[i]);
						// Set the Ending Position to a random star position in the scene (that has not already been taken by this star)
						newLR.SetPosition(1, starManagerScript.starPositionsList[Random.Range(0, starManagerScript.starPositionsList.Count)]);
						// If the Line Renderers End Position is the same as this stars position OR this star already has a neighbour connection at this position,
						// then it will loop until it is a unqiue star that hasn't already got a connection to this star
						while (newLR.GetPosition(1) == starManagerScript.starPositionsList[i] || currentStarNeighbourPositions.Contains(newLR.GetPosition(1))) {
							newLR.SetPosition(1, starManagerScript.starPositionsList[Random.Range(0, starManagerScript.starPositionsList.Count)]);
						}
						// Add current stars neighbour position to avoid duplicate neighbours for this particular star
						currentStarNeighbourPositions.Add(newLR.GetPosition(1));
					}
				}
			}
			GenerateStars.drawLines = false;
			GetEachStar();
		} 
	}
}