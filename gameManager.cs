using UnityEngine;
using UnityEngine.SceneManagement; // 1. REQUIRED for scene management

public class GameManager : MonoBehaviour
{
    // --- Singleton Instance ---
    public static GameManager Instance { get; private set; }

    // --- Settings Storage ---
    [Header("Toggle Settings (Booleans)")]
    [Tooltip("State of the Music Toggle (e.g., Is music enabled?)")]
    public bool MusicToggle = true;

    [Tooltip("State of the SFX Toggle (e.g., Are sound effects enabled?)")]
    public bool SFXToggle = true;

    // --- MISSING DISABILITY TOGGLE SETTINGS ---
    [Tooltip("State of the Wheelchair Toggle (e.g., Affects movement logic)")]
    public bool WheelchairToggle = false;

    [Tooltip("State of the Dyslexia Toggle (e.g., Affects text rendering)")]
    public bool DyslexiaToggle = false;
    // -----------------------------------------

    [Header("Slider Settings (Floats)")]
    [Range(0f, 1f)]
    [Tooltip("Current master volume level (0.0 to 1.0)")]
    public float MasterVolume = 0.75f;

    [Range(0.1f, 5.0f)]
    [Tooltip("Current mouse/camera sensitivity (e.g., 1.0 is default)")]
    public float Sensitivity = 1.0f;

    // --- AUTISM SLIDER SETTING ---
    [Range(0f, 1f)]
    [Tooltip("Level of autism-friendly filtering/simplicity (0.0 = Off, 1.0 = Max filtering/simplicity)")]
    public float AutismFilterLevel = 0.0f;
    // -----------------------------

    // --- MISSING BLINDNESS SLIDER SETTINGS ---
    [Range(0f, 1f)]
    [Tooltip("Level of darkness/fog (0.0 = Clear, 1.0 = Max Fog)")]
    public float DarknessLevel = 0.0f;

    [Range(0f, 300f)]
    [Tooltip("Intensity of the blur effect (0.0 = Clear, 300.0 = Max Blur)")]
    public float BlurIntensity = 0.0f;
    // -----------------------------------------

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Implements the Singleton pattern and ensures persistence.
    /// </summary>
    private void Awake()
    {
        // 1. Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If another instance exists, destroy this new one
            Destroy(this.gameObject);
            Debug.LogWarning("GameManager detected a duplicate and destroyed itself.");
            return;
        }

        // 2. Set this instance as the Singleton
        Instance = this;

        // 3. Make this object persist across scene changes
        DontDestroyOnLoad(this.gameObject);

        Debug.Log("GameManager initialized and set to persist across scenes.");

        // Optional: Load initial values from PlayerPrefs or a save file here if needed
        // LoadSettings();
    }

    // --- Public Methods to Update Settings from UI ---

    /// <summary>
    /// Updates the music toggle state.
    /// </summary>
    /// <param name="isOn">The new boolean value from a Toggle UI element.</param>
    public void SetMusicToggle(bool isOn)
    {
        MusicToggle = isOn;
        Debug.Log($"Music Toggle set to: {isOn}");
    }

    /// <summary>
    /// Updates the SFX toggle state.
    /// </summary>
    /// <param name="isOn">The new boolean value from a Toggle UI element.</param>
    public void SetSFXToggle(bool isOn)
    {
        SFXToggle = isOn;
        Debug.Log($"SFX Toggle set to: {isOn}");
    }

    // --- NEW SETTER METHODS FOR MISSING TOGGLES ---
    public void SetWheelchairToggle(bool isOn)
    {
        WheelchairToggle = isOn;
        Debug.Log($"Wheelchair Toggle set to: {isOn}");
    }
    
    public void SetDyslexiaToggle(bool isOn)
    {
        DyslexiaToggle = isOn;
        Debug.Log($"Dyslexia Toggle set to: {isOn}");
    }
    // ---------------------------------------------

    /// <summary>
    /// Updates the master volume level.
    /// </summary>
    /// <param name="volume">The new float value from a Slider UI element.</param>
    public void SetMasterVolume(float volume)
    {
        MasterVolume = volume;
        Debug.Log($"Master Volume set to: {volume:F2}");
    }

    /// <summary>
    /// Updates the player sensitivity setting.
    /// </summary>
    /// <param name="sens">The new float value from a Slider UI element.</param>
    public void SetSensitivity(float sens)
    {
        Sensitivity = sens;
        Debug.Log($"Sensitivity set to: {sens:F2}");
    }

    /// <summary>
    /// Updates the autism-friendly filtering level.
    /// </summary>
    /// <param name="level">The new float value from a Slider UI element (0.0 to 1.0).</param>
    public void SetAutismFilterLevel(float level)
    {
        AutismFilterLevel = level;
        Debug.Log($"Autism Filter Level set to: {level:F2}");
    }

    // --- NEW SETTER METHODS FOR MISSING SLIDERS ---
    public void SetDarknessLevel(float level)
    {
        DarknessLevel = level;
        Debug.Log($"Darkness Level set to: {level:F2}");
    }

    public void SetBlurIntensity(float intensity)
    {
        BlurIntensity = intensity;
        Debug.Log($"Blur Intensity set to: {intensity:F2}");
    }
    // ----------------------------------------------
    
    // --- NEW FUNCTION TO LOAD SCENES ---
    
    /// <summary>
    /// Loads a scene by its name. This function is typically connected to a UI Button.
    /// </summary>
    /// <param name="sceneName">The exact name of the scene to load (must be in Build Settings).</param>
    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("LoadSceneByName called with an empty scene name!");
            return;
        }

        Debug.Log($"Attempting to load scene: {sceneName}");
        
        // This will load the scene asynchronously but immediately.
        // It's crucial the scene is added to your Build Settings (File > Build Settings).
        SceneManager.LoadScene(sceneName);
    }
}