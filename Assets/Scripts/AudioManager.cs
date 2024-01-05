using UnityEngine;

// This Class contains all audio sources used in the project in which scripts can call from to Play audio clips at particular intervals
// It also allows all audio that is currently playing to Pause and Re-continue playing when the user pauses the project
public class AudioManager : MonoBehaviour {
    private AudioSource[] allAudioSources;
    [SerializeField]
    private AudioSource ambientSpaceAudioSource;
    [SerializeField]
    private AudioSource rocketBoostAudioSource;
    [SerializeField]
    private AudioSource buttonClickUIAudioSource;
    [SerializeField]
    private AudioSource confirmUIAudioSource;
    [SerializeField]
    private AudioSource backUIAudioSource;
    [SerializeField]
    private AudioSource closeUIAudioSource;
    [SerializeField]
    private AudioSource pathfindingSuccessAudioSource;
    [SerializeField]
    private AudioSource pathfindingErrorAudioSource;
    [SerializeField]
    private AudioSource reachedDestinationAudioSource;
    [SerializeField]
    private AudioSource selfDestructSequenceAudioSource;
    [SerializeField]
    private AudioSource selfDestructCountdownAudioSource;
    [SerializeField]
    private AudioSource explosionAudioSource;
    [SerializeField]
    private AudioSource npcExplosionAudioSource;
    [SerializeField]
    private AudioSource starClickAudioSource;
    [SerializeField]
    private AudioSource resetValuesAudioSource;

    // Retrieves all Audio Sources present in the game and Pauses them.
    public void PauseAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audio in allAudioSources) {
            audio.Pause();
        }
    }
    // Un-Pauses any audio sources that were previously playing prior to the game being paused.
    public void UnPauseAudio() {
        foreach (AudioSource audio in allAudioSources) {
            audio.UnPause();
        }
    }
    public void AmbientSpaceAudio() {
        ambientSpaceAudioSource.Play();
    }
    public void RocketBoostAudio() {
        rocketBoostAudioSource.Play();
    }
    public void StopRocketBoostAudio() {
        rocketBoostAudioSource.Stop();
    }
    public void ButtonClickUIAudio() {
        buttonClickUIAudioSource.Play();
    }
    public void ConfirmUIAudio() {
        confirmUIAudioSource.Play();
    }
    public void BackUIAudio() {
        backUIAudioSource.Play();
    }
    public void CloseUIAudio() {
        closeUIAudioSource.Play();
    }
    public void PathfindingSuccessAudio() {
        pathfindingSuccessAudioSource.Play();
    }
    public void PathfindingErrorAudio() {
        pathfindingErrorAudioSource.Play();
    }
    public void ReachedDestinationAudio() {
        reachedDestinationAudioSource.Play();
    }
    public void SelfDestructSequenceAudio() {
        selfDestructSequenceAudioSource.Play();
    }
    public void SelfDestructCountdownAudio() {
        selfDestructCountdownAudioSource.Play();
    }
    public void ExplosionAudio() {
        explosionAudioSource.Play();
    }
    public void NPCExplosionAudio() {
        npcExplosionAudioSource.Play();
    }
    public void StarClickAudio() {
        starClickAudioSource.Play();
    }
    public void ResetValuesAudio() {
        resetValuesAudioSource.Play();
    }
}