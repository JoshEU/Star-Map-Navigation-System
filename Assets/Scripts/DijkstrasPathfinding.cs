using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This Class carries out Dijkstras Pathfinding Algorithm in order to find the shortest path from the chosen 'Origin' Star to the chosen 'Destination' Star.
public class DijkstrasPathfinding : MonoBehaviour {    
    // Script References:
    [SerializeField]
    private StarManager starManagerScript;
    [SerializeField]
    private LineRenderers lineRenderersScript;
    [SerializeField]
    private NavigatePath navigatepathScript;
    [SerializeField]
    private PathfindingConstraints pathfindingConstraintsScript;
    [SerializeField]
    private AudioManager audioManagerScript;
    // Pathfinding Variables: 
    public GameObject currentNode = null;
    public GameObject nextStarToVisit;
    public GameObject shortestDistanceStar;
    public int totalStarsVisited = 0;
    public List<GameObject> unvisitedStars = new List<GameObject>();
    public List<GameObject> visitedStars = new List<GameObject>();
    public List<int> shortestPathDistances = new List<int>();
    public bool foundShortestPath = false;
    public bool destinationStarIsReachable = true;
    public bool isStarJumpIsTooFarAway = false;
    // Scene Object References:
    [SerializeField]
    private TextMeshProUGUI shortestPathTMP;
    [SerializeField]
    private TextMeshProUGUI shortestPathConstraintsTMP;
    [SerializeField]
    private TextMeshProUGUI shortestPathFlyingTMP;
    [SerializeField]
    private Button flyShipBtnObj;
    [SerializeField]
    private Button pathfindingBtnObj;
    [SerializeField]
    private LineRenderer shortestPathLineRenderer;

    // This Function is called whenever the Pathfinding Button is pressed - it clears the previous list of Un-visited and Visited Stars
    private void ResetPathfinding() {
        // Remove any data from previous pathfinding lists by clearing them
        unvisitedStars.Clear();
        visitedStars.Clear();
        shortestPathDistances.Clear();
        totalStarsVisited = 0;
        foundShortestPath = false;
        destinationStarIsReachable = true;
        shortestPathTMP.text = null;
        shortestPathConstraintsTMP.text = null;
        shortestPathFlyingTMP.text = null;
        isStarJumpIsTooFarAway = false;
        navigatepathScript.shipFlyRoute.Clear();
    }
    private void AssignToUnvisitedList() {
        // Iterate through each star object and assign it to the Un-visited Stars list
        for (int i = 0; i < starManagerScript.starObjectsArray.Length; i++) {
            unvisitedStars.Add(starManagerScript.starObjectsArray[i]);
        }
    }
    // This Function is called once at then beginning of Pathfinding - it visits the origin star at the beginning of the Pathfinding
    // Also can be called a few times once the Destination Star has been found but not all stars have been visited yet
    private void VisitOriginStar() {
        // Retrieve all stars and store them in an array of type: GameObjects
        GameObject[] starObjectArray = GameObject.FindGameObjectsWithTag("Star");
        // Assigns the start node a distance of 0 from the start
        foreach (GameObject star in starObjectArray) {
            // Check which star is the 'Origin' Star
            if (star == PickOriginDestination.originObj) {
                // Assigns currentNode as the 'Origin' Star
                currentNode = PickOriginDestination.originObj;
                // Assigns the origin node a distance of 0, and sets it to be the current node
                PickOriginDestination.originObj.GetComponent<Star>().shortestDistanceToStart = 0;
                // Removes the current node from the Un-visited Stars List
                unvisitedStars.Remove(currentNode);
                // Add current node to the Visited Stars List
                visitedStars.Add(currentNode);
                // Add Origin Star distance to a list of the starDistances - adding "shortestDistanceToStart" as it will always be 0 for the origin star
                shortestPathDistances.Add(currentNode.GetComponent<Star>().shortestDistanceToStart);
                // Increase stars visited by 1;
                totalStarsVisited += 1;
            }
        }
        // Once every neighbour has been visited, add the Origin Star to shortest Path Text
        shortestPathTMP.text = shortestPathTMP.text + currentNode.name + " + ";
        shortestPathConstraintsTMP.text = shortestPathConstraintsTMP.text + currentNode.name + " + ";
        shortestPathFlyingTMP.text = shortestPathFlyingTMP.text + currentNode.name + " + ";
        // Add Origin Position to ships fly route
        navigatepathScript.shipFlyRoute.Add(currentNode.transform.position);
    }
    // This Function is called once - it will visit all the neighbours of the origin star
    private void VisitOriginNeighbouringStars() {
        // Store neighbour distances
        List<int> neighbouringDistances = new List<int>();
        // Store smallestDistance between neighbours
        int smallestDistance = int.MaxValue;

        // Retrieve the current nodes neighbouring positions
        foreach (Vector3 neighbouringPosition in currentNode.GetComponent<Star>().neighbouringStarsPositions) {
            if (foundShortestPath == false) {
                // Increase stars visited by 1;
                totalStarsVisited += 1;

                // Create an array of neighbourPositions and initialise its size to the correct amount of neighbours
                List<Vector3> currentStarsNeighbouringPositions = new List<Vector3>();
                // Add each neighbouring position for the current node to a new list of Vector 3's
                currentStarsNeighbouringPositions.Add(neighbouringPosition);
                // Randomise the neighbouring star to visit
                int randIndex = Random.Range(0, currentStarsNeighbouringPositions.Count);
                // Check which neighbouring star gameObject is at the random indexed position
                // Iterate through so that you visit all neighbouring stars
                Collider[] neighbouringStarCollider = Physics.OverlapSphere(currentStarsNeighbouringPositions[randIndex], 0.75f);
                // Set current node to be the chosen neighbouring star to visit
                currentNode = neighbouringStarCollider[0].gameObject;

                // Removes the current node from the Un-visited Stars List
                unvisitedStars.Remove(currentNode);
                // Add current node to the Visited Stars List
                visitedStars.Add(currentNode);

                // Add to the neighbouringDistances List
                neighbouringDistances.Add(currentNode.GetComponent<Star>().starDistance);

                // Assigns shortest distance - as they are origin star neighbours - just set it to their starDistance
                currentNode.GetComponent<Star>().shortestDistanceToStart = currentNode.GetComponent<Star>().starDistance;

                // Checks if the current neighbour star has a distance lower than the other neighbouring stars
                if (currentNode.GetComponent<Star>().starDistance < smallestDistance) {
                    smallestDistance = currentNode.GetComponent<Star>().starDistance;
                    nextStarToVisit = currentNode;
                    shortestDistanceStar = currentNode;
				}
                // Check if the currentNode is the destination Node
                CheckDestinationStarFound();
            }
        }
        // Checking if the the shortestDistanceStar's distance is within range of the MaxJumpDistance value in the PathfindingConstraints UI
        if (shortestDistanceStar.GetComponent<Star>().starDistance > pathfindingConstraintsScript.maxJumpDistance) {
            isStarJumpIsTooFarAway = true;
        }
        // Check if the currentNode contains the destination node as its neighbour
        else if (currentNode.GetComponent<Star>().name == PickOriginDestination.destinationObj.name) {
            foundShortestPath = true;
            nextStarToVisit = null;
            shortestDistanceStar = currentNode;
            // Add Destination text to the shortestPath Text & set shortestPathDistance as the Destination stars distance
            shortestPathDistances.Add(currentNode.GetComponent<Star>().shortestDistanceToStart);
            shortestPathTMP.text = shortestPathTMP.text + shortestDistanceStar.name;
            shortestPathConstraintsTMP.text = shortestPathConstraintsTMP.text + shortestDistanceStar.name;
            shortestPathFlyingTMP.text = shortestPathFlyingTMP.text + shortestDistanceStar.name;
            // Check if the currentNode is the destination Node
            CheckDestinationStarFound();
		} else {
            shortestPathDistances.Add(smallestDistance);
            shortestPathTMP.text = shortestPathTMP.text + shortestDistanceStar.name + " + ";
            shortestPathConstraintsTMP.text = shortestPathConstraintsTMP.text + shortestDistanceStar.name + " + ";
            shortestPathFlyingTMP.text = shortestPathFlyingTMP.text + shortestDistanceStar.name + " + ";
        }
        // Add nextStarToVisir Position to ships fly route
        navigatepathScript.shipFlyRoute.Add(shortestDistanceStar.transform.position);
    }
    // This Function will be called multiple times - it will visit all the neighbouring stars of a current node after the origins neigbouring stars have been visited
    private void VisitOtherNeighbouringStars() {
        // Store neighbour distances
        List<int> neighbouringDistances = new List<int>();
        // Store smallestDistance between neighbours
        int smallestDistance = int.MaxValue;
        currentNode = nextStarToVisit;

        // Retrieve the current nodes neighbouring positions
        foreach (Vector3 neighbouringPosition in currentNode.GetComponent<Star>().neighbouringStarsPositions) {
			// Makes sure to only check neighbouring stars that have NOT already been visited - executed only if the current node doesn't contain the destination node as its neighbour
			 if (foundShortestPath == false) {
                // Increase stars visited by 1;
                totalStarsVisited += 1;

                // Create an array of neighbourPositions and initialise its size to the correct amount of neighbours
                List<Vector3> currentStarsNeighbouringPositions = new List<Vector3>();
                // Add each neighbouring position for the current node to a new list of Vector 3's
                currentStarsNeighbouringPositions.Add(neighbouringPosition);
                // Randomise the neighbouring star to visit
                int randIndex = Random.Range(0, currentStarsNeighbouringPositions.Count);
                // Check which neighbouring star gameObject is at the random indexed position
                // Iterate through so that you visit all neighbouring stars
                Collider[] neighbouringStarCollider = Physics.OverlapSphere(currentStarsNeighbouringPositions[randIndex], 0.75f);
                // Set current node to be the chosen neighbouring star to visit
                currentNode = neighbouringStarCollider[0].gameObject; 

                // Removes the current node from the Un-visited Stars List
                unvisitedStars.Remove(currentNode);
                if (visitedStars.Contains(currentNode) == false) {
                    // Add current node to the Visited Stars List if it has not already been visited
                    visitedStars.Add(currentNode);
                }

                // Add to the neighbouringDistances List
                neighbouringDistances.Add(currentNode.GetComponent<Star>().starDistance);              

                // Set shortestDistanceToStart to 0
                currentNode.GetComponent<Star>().shortestDistanceToStart = 0;

                // Assign current nodes shortestDistanceToStart
                for (int i = 0; i < shortestPathDistances.Count; i++) {
                    currentNode.GetComponent<Star>().shortestDistanceToStart = currentNode.GetComponent<Star>().shortestDistanceToStart += shortestPathDistances[i];
                }
                // Increment current nodes shortestDistanceToStart by their starDistance as well
                currentNode.GetComponent<Star>().shortestDistanceToStart += currentNode.GetComponent<Star>().starDistance;

                // Check if the current neighbour star has a distance lower than the other neighbouring stars
                if (currentNode.GetComponent<Star>().starDistance < smallestDistance) {
                    smallestDistance = currentNode.GetComponent<Star>().starDistance;
                    nextStarToVisit = currentNode;
                    shortestDistanceStar = currentNode;
                } 
                // Check if the currentNode is the destination Node
                CheckDestinationStarFound();
            } 
        }
        // Checking if the the shortestDistanceStar's distance is within range of the MaxJumpDistance value in the PathfindingConstraints UI
        if (shortestDistanceStar.GetComponent<Star>().starDistance > pathfindingConstraintsScript.maxJumpDistance) {
            isStarJumpIsTooFarAway = true;
        }
        // Check if the currentNode contains the destination node as its neighbour
        else if(currentNode.GetComponent<Star>().name == PickOriginDestination.destinationObj.name) {
            foundShortestPath = true;
            nextStarToVisit = null;
            shortestDistanceStar = currentNode;
            // Add Destination text to the shortestPath Text & set shortestPathDistance as the Destination stars distance
            shortestPathDistances.Add(smallestDistance);
            shortestPathTMP.text = shortestPathTMP.text + shortestDistanceStar.name;
            shortestPathConstraintsTMP.text = shortestPathConstraintsTMP.text + shortestDistanceStar.name;
            shortestPathFlyingTMP.text = shortestPathFlyingTMP.text + shortestDistanceStar.name;
            // Check if the currentNode is the destination Node
            CheckDestinationStarFound();
		} else {
            shortestPathDistances.Add(smallestDistance);
            shortestPathTMP.text = shortestPathTMP.text + shortestDistanceStar.name + " + ";
            shortestPathConstraintsTMP.text = shortestPathConstraintsTMP.text + shortestDistanceStar.name + " + ";
            shortestPathFlyingTMP.text = shortestPathFlyingTMP.text + shortestDistanceStar.name + " + ";
        }
        // Add nextStarToVisit Position to ships fly route
        navigatepathScript.shipFlyRoute.Add(shortestDistanceStar.transform.position);
    }
    // This Function will check if the destination star has been found after visiting the origin stars neighbour stars 
    private void CheckDestinationStarFound() {
        // Check if you have visited/reached the Destination Star
        if (currentNode.name == PickOriginDestination.destinationObj.name && currentNode.GetComponent<Star>().starDistance <= pathfindingConstraintsScript.maxJumpDistance) {
            foundShortestPath = true;
        } 
        else if (currentNode.name != PickOriginDestination.destinationObj.name) {
            foundShortestPath = false;
        }
    }
    // Called when the Pathfinding button is clicked - assigned in the inspector
    public void DestroyShortestPathLR() {
        Destroy(GameObject.FindGameObjectWithTag("ShortestPathLR"));
    }
    public void PathfindingButton() {
        // Resets values and lists after every 'Pathfinding Button click'
        ResetPathfinding();

        // Iterate until the shortestPath has been found
        if (foundShortestPath == false && UIManager.canDoPathfinding) {
            // Assigns all stars in scene to unvisited list
            AssignToUnvisitedList();

            // Visits the Origin Star
            VisitOriginStar();

            // Visits the Origin Stars neighbouring stars
            VisitOriginNeighbouringStars();

            // Iterate and call the VisitOtherNeighbouringStars() function in correlation to how many stars have been generated - this will ensure that all stars have been checked and will determine whether or not there is a possible route to the Destination Star
            for (int i = 0; i <= starManagerScript.starObjectsArray.Length; i++) {
                // Checks if the shortest path has still not been found on each iteration
                if (foundShortestPath == false) {
                    // Visits the other Neighbouring stars - called once the origins neighbouring star with the lowest distance has been found
                    VisitOtherNeighbouringStars();
                    // Checks if the loop is on its final iteration 
                    // If so, then it will decide that the Destination Star cannot be reached as it would have already been found by now if there was a possible route
                    if (i == starManagerScript.starObjectsArray.Length - 1 && foundShortestPath == false) {
                        destinationStarIsReachable = false;
                    }
                }
            }
            // If the Destination star cannot be reached and the starJump is within range, then print "ERROR: DESTINATION STAR CANNOT BE REACHED!"
            // Otherwise if the starJump was out of range, then print "ERROR: STAR JUMP IS OUT OF RANGE! I SUGGEST YOU INCREASE THE MAX JUMP DISTANCE!"
            if (destinationStarIsReachable == false && isStarJumpIsTooFarAway == false) {
                shortestPathTMP.text = "ERROR: DESTINATION STAR CANNOT BE REACHED!";
                shortestPathConstraintsTMP.text = "ERROR: DESTINATION STAR CANNOT BE REACHED!";
                shortestPathFlyingTMP.text = "ERROR: DESTINATION STAR CANNOT BE REACHED!";
                flyShipBtnObj.interactable = false;
                pathfindingBtnObj.interactable = true;
                visitedStars.Clear();
                navigatepathScript.shipFlyRoute.Clear();
                // Plays Pathfinding Error SFX
                audioManagerScript.PathfindingErrorAudio();
            }
            else if(isStarJumpIsTooFarAway == true) {
                shortestPathTMP.text = "ERROR: STAR JUMP IS OUT OF RANGE!\nI SUGGEST YOU INCREASE THE MAX JUMP DISTANCE!";
                shortestPathConstraintsTMP.text = "ERROR: STAR JUMP IS OUT OF RANGE!\nI SUGGEST YOU INCREASE THE MAX JUMP DISTANCE!";
                shortestPathFlyingTMP.text = "ERROR: STAR JUMP IS OUT OF RANGE!\nI SUGGEST YOU INCREASE THE MAX JUMP DISTANCE!";
                flyShipBtnObj.interactable = false;
                foundShortestPath = false;
                // Plays Pathfinding Error SFX
                audioManagerScript.PathfindingErrorAudio();
            } 
        } 
    }  
    // Displays a Line Renderer that maps out the fastest route manually for the user in world space
    private void ShowPathfindingRouteLR() {
        LineRenderer pathfindingLR = Instantiate(shortestPathLineRenderer);
        // Set number of vertices to the number of stars that are required to be visited by the Nav Agen when pathfinding
        pathfindingLR.positionCount = shortestPathDistances.Count;
        // Add Origin Position to the shortestPath LineRenderer
        pathfindingLR.SetPositions(navigatepathScript.shipFlyRoute.ToArray());
        // Changes the Spaceship Nav Agents Rotation so it's already looking at its first star to travel to on route to the destination
        navigatepathScript.gameObject.transform.LookAt(navigatepathScript.shipFlyRoute[1], Vector3.back);
        // Plays Pathfinding Success SFX
        audioManagerScript.PathfindingSuccessAudio();
    }
    private void Update() {
		if (foundShortestPath) {
			ShowPathfindingRouteLR();
			foundShortestPath = false;
		}
	}
}