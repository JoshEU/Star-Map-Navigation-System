using UnityEngine;

// This Class will make a star glow when you hover over it with the mouse cursor - essentially when you look at the star with the camera
public class HoverOverStar : MonoBehaviour {
    private GameObject hoveredStarObj;
    private Color hoveredStarColor;
    private bool isAnotherStarGlowing;
    // Shoots a ray that checks if the user is hovering over a star.
    // If TRUE: It will glow
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            // Set GameObject variable to the star GameObject that the Ray hit
            hoveredStarObj = hit.transform.gameObject;
            Debug.DrawLine(ray.origin, hit.point, Color.red, 5.0f);
            // Check if the object being hovered over is a star and that no other stars in the scene are currently being hovered over
            if (hit.collider.gameObject.tag == "Star" && isAnotherStarGlowing == false) {
                // Get the stars current color (The one that is being hovered over)
                hoveredStarColor = hoveredStarObj.GetComponent<MeshRenderer>().material.color;
                // Enable the light component (glow effect)
                hit.collider.gameObject.GetComponent<Light>().enabled = true;
                // Make the light color the same as the stars color
                hit.collider.gameObject.GetComponent<Light>().color = hoveredStarColor;
                isAnotherStarGlowing = true;
            }
        } 
        else if (ClickStar.isConfirmationUIEnabled) {
            // Don't disable a light component if if a star is already glowing
        } else { 
            // Disable the light component (glow effect)
            gameObject.GetComponent<Light>().enabled = false;
            // Allow a new star to glow when it's hovered over
            isAnotherStarGlowing = false;
        }
    }
}