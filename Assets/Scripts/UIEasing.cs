using System.Collections;
using UnityEngine;

// This Class contains all the logic for carrying out the UI Easing functions.
// These appear when navigating the UI etc.
public class UIEasing : MonoBehaviour {
    [SerializeField]
    private GameObject featuresPanelObj;
    [SerializeField]
    private GameObject constraintsPanelObj;
    [SerializeField]
    private GameObject flyingPanelObj;
    [SerializeField]
    private GameObject starDescriptionObj;
    [SerializeField]
    private GameObject pathfindingPanelFeaturesObj;
    [SerializeField]
    private GameObject pathfindingPanelConstraintsObj;
    [SerializeField]
    public GameObject pathfindingPanelFlyingObj;
    [SerializeField]
    private CanvasGroup featuresPanelGroup;
    [SerializeField]
    private CanvasGroup constraintsPanelGroup;
    [SerializeField]
    private CanvasGroup flyingPanelGroup;
    [SerializeField]
    private CanvasGroup starDescriptionGroup;
    [SerializeField]
    private CanvasGroup pathfindingPanelFeaturesGroup;
    [SerializeField]
    private CanvasGroup pathfindingPanelConstraintsGroup;
    [SerializeField]
    private CanvasGroup pathfindingPanelFlyingGroup;

    public void ShowFeaturesPanel() {
        StartCoroutine(EaseFeaturesPanelIn());
    }
    public void HideFeaturesPanel() {
        StartCoroutine(EaseFeaturesPanelOut());
    }
    public void ShowConstraintsPanel() {
        StartCoroutine(EaseConstraintsPanelIn());
    }
    public void HideConstraintsPanel() {
        StartCoroutine(EaseConstraintsPanelOut());
    }
    public void ShowFlyingPanel() {
        StartCoroutine(EaseFlyingPanelIn());
    }
    public void HideFlyingPanel() {
        StartCoroutine(EaseFlyingPanelOut());
    }
    public void ShowStarDescriptionPanel() {
        StartCoroutine(EaseStarDescriptionPanelIn());
    }
    public void HideStarDescriptionPanel() {
        StartCoroutine(EaseStarDescriptionPanelOut());
    }
    public void ShowPathfindingFeaturesPanel() {
        StartCoroutine(EasePathfindingFeaturesPanelIn());
    }
    public void HidePathfindingFeaturesPanel() {
        StartCoroutine(EasePathfindingFeaturesPanelOut());
    }
    public void ShowPathfindingConstraintsPanel() {
        StartCoroutine(EasePathfindingConstraintsPanelIn());
    }
    public void HidePathfindingConstraintsPanel() {
        StartCoroutine(EasePathfindingConstraintsPanelOut());
    }
    public void ShowPathfindingFlyingPanel() {
        StartCoroutine(EasePathfindingFlyingPanelIn());
    }
    public void HidePathfindingFlyingPanel() {
        StartCoroutine(EasePathfindingFlyingPanelOut());
    }
    // Features Panel (Fades In) 
    private IEnumerator EaseFeaturesPanelIn() {
        float time = 0f;
        while (time <= 1f) {
            featuresPanelGroup.alpha = CustomEasing.Quadratic.QuadIn(time);
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        // Wait for Panel to fully fade in before allowing the user to interact with its contents
        featuresPanelGroup.interactable = true;
    }
    // Features Panel (Fades Out)
    private IEnumerator EaseFeaturesPanelOut() {
        float time = 0f;
        while (time <= 1f) {
            featuresPanelGroup.alpha = 1 - CustomEasing.Quadratic.QuadOut(time);
            time += Time.deltaTime * 2;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        featuresPanelObj.SetActive(false);
    }
    // Constraints Panel (Fades In) 
    private IEnumerator EaseConstraintsPanelIn() {
        float time = 0f;
        while (time <= 1f) {
            constraintsPanelGroup.alpha = CustomEasing.Quadratic.QuadIn(time);
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        constraintsPanelGroup.interactable = true;
    }
    // Constraints Panel (Fades Out)
    private IEnumerator EaseConstraintsPanelOut() {
        float time = 0f;
        while (time <= 1f) {
            constraintsPanelGroup.alpha = 1 - CustomEasing.Quadratic.QuadOut(time);
            time += Time.deltaTime * 2;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        constraintsPanelObj.SetActive(false);
    }
    // Flying Panel (Fades In) 
    private IEnumerator EaseFlyingPanelIn() {
        float time = 0f;
        while (time <= 1f) {
            flyingPanelGroup.alpha = CustomEasing.Quadratic.QuadIn(time);
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        flyingPanelGroup.interactable = true;
    }
    // Flying Panel (Fades Out)
    private IEnumerator EaseFlyingPanelOut() {
        float time = 0f;
        while (time <= 1f) {
            flyingPanelGroup.alpha = 1 - CustomEasing.Quadratic.QuadOut(time);
            time += Time.deltaTime * 2;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        flyingPanelObj.SetActive(false);
    }
    // StarDescription Panel (Fades In) 
    private IEnumerator EaseStarDescriptionPanelIn() {
        float time = 0f;
        while (time <= 1f) {
            starDescriptionGroup.alpha = CustomEasing.Quadratic.QuadIn(time);
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        starDescriptionGroup.interactable = true;
    }
    // StarDescription Panel (Fades Out)
    private IEnumerator EaseStarDescriptionPanelOut() {
        float time = 0f;
        while (time <= 1f) {
            starDescriptionGroup.alpha = 1 - CustomEasing.Quadratic.QuadOut(time);
            time += Time.deltaTime * 2;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        starDescriptionObj.SetActive(false);
    }
    // PathfindingFeatures Panel (Fades In) 
    private IEnumerator EasePathfindingFeaturesPanelIn() {
        if (pathfindingPanelConstraintsObj.activeSelf == true || pathfindingPanelFlyingObj.activeSelf == true) {
            pathfindingPanelFeaturesObj.SetActive(true);
            float time = 0f;
            while (time <= 1f) {
                pathfindingPanelFeaturesGroup.alpha = CustomEasing.Quadratic.QuadIn(time);
                time += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            pathfindingPanelFeaturesGroup.interactable = true;
        }
    }
    // PathfindingFeatures Panel (Fades Out)
    private IEnumerator EasePathfindingFeaturesPanelOut() {
        if (pathfindingPanelFeaturesObj.activeSelf == true) {
            float time = 0f;
            while (time <= 1f) {
                pathfindingPanelFeaturesGroup.alpha = 1 - CustomEasing.Quadratic.QuadOut(time);
                time += Time.deltaTime * 2;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            pathfindingPanelFeaturesObj.SetActive(false);
        }
    }
    // PathfindingConstraints Panel (Fades In) 
    private IEnumerator EasePathfindingConstraintsPanelIn() {
        if (pathfindingPanelFeaturesObj.activeSelf == true) {
            pathfindingPanelConstraintsObj.SetActive(true);
            float time = 0f;
            while (time <= 1f) {
                pathfindingPanelConstraintsGroup.alpha = CustomEasing.Quadratic.QuadIn(time);
                time += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            pathfindingPanelConstraintsGroup.interactable = true;;
        }
    }
    // PathfindingConstraints Panel (Fades Out)
    private IEnumerator EasePathfindingConstraintsPanelOut() {
        if (pathfindingPanelConstraintsObj.activeSelf == true) {
            float time = 0f;
            while (time <= 1f) {
                pathfindingPanelConstraintsGroup.alpha = 1 - CustomEasing.Quadratic.QuadOut(time);
                time += Time.deltaTime * 2;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            pathfindingPanelConstraintsObj.SetActive(false);
        }
    }
    // PathfindingFlying Panel (Fades In) 
    private IEnumerator EasePathfindingFlyingPanelIn() {
        if (pathfindingPanelFeaturesObj.activeSelf == true) {
            pathfindingPanelFlyingObj.SetActive(true);
            float time = 0f;
            while (time <= 1f) {
                pathfindingPanelFlyingGroup.alpha = CustomEasing.Quadratic.QuadIn(time);
                time += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            pathfindingPanelFlyingGroup.interactable = true;
        }
    }
    // PathfindingFlying Panel (Fades Out)
    private IEnumerator EasePathfindingFlyingPanelOut() {
        if (pathfindingPanelFlyingObj.activeSelf == true) {
            float time = 0f;
            while (time <= 1f) {
                pathfindingPanelFlyingGroup.alpha = 1 - CustomEasing.Quadratic.QuadOut(time);
                time += Time.deltaTime * 2;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            pathfindingPanelFlyingObj.SetActive(false);
        }
    }
}