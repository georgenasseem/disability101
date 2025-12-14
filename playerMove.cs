using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class playerMove : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public Volume vol; // The Global Volume component reference
    public LayerMask groundMask;
    public Transform feet;

    // --- Restored Local Variables (Synced from GameManager) ---
    [Header("Disability Settings (Synced)")]
    public bool isDyslexic;
    public bool inWheelchair;
    public AudioSource aud;

    [Range(0f, 1f)]
    public float blindnessFog;
    [Range(0f, 300f)]
    public float blindnessBlur;
    
    // --- NEW AUTISM SLIDER SETTING (Synced) ---
    [Range(0f, 1f)]
    [Tooltip("Level of autism-friendly filtering/simplicity.")]
    public float autismFilterLevel;
    // --- End Restored Variables ---

    [HideInInspector]
    public bool isTalking = false;

    float groundDistance = 0.4f;
    const float gravity = -9.81f;
    bool isGrounded;
    
    // --- Post-Processing Effect References ---
    DepthOfField DOF;
    ChromaticAberration ChromAb; // NEW REFERENCE
    LensDistortion LensD;       // NEW REFERENCE
    // -----------------------------------------

    Vector3 velocity;

    void Start()
    {
        // Safety Check: Ensure the persistent GameManager exists.
        if (GameManager.Instance == null)
        {
            Debug.LogError("FATAL ERROR: GameManager instance is missing. Ensure the GameManager script is running in your first scene and set to DontDestroyOnLoad.");
            enabled = false;
            return;
        }

        // Initialize Post-Processing Effects
        if(vol.profile.TryGet<DepthOfField>(out DOF)) { Debug.Log("DOF settings found!"); }
        else { Debug.Log("DOF settings not found!"); }

        // --- NEW INITIALIZATION FOR CHROMATIC ABERRATION ---
        if(vol.profile.TryGet<ChromaticAberration>(out ChromAb)) { Debug.Log("ChromaticAberration settings found!"); }
        else { Debug.Log("ChromaticAberration settings not found! Ensure it's in your Global Volume Profile."); }

        // --- NEW INITIALIZATION FOR LENS DISTORTION ---
        if(vol.profile.TryGet<LensDistortion>(out LensD)) { Debug.Log("LensDistortion settings found!"); }
        else { Debug.Log("LensDistortion settings not found! Ensure it's in your Global Volume Profile."); }
    }

    // Update is called once per frame
    void Update()
    {
        // --- 1. Sync Visual Settings from GameManager to Local Variables ---

        // Sync local variables with the persistent GameManager values
        inWheelchair = GameManager.Instance.WheelchairToggle;
        isDyslexic = GameManager.Instance.DyslexiaToggle;
        blindnessFog = GameManager.Instance.DarknessLevel;
        blindnessBlur = GameManager.Instance.BlurIntensity;
        
        // --- NEW SYNC FOR AUTISM SLIDER ---
        autismFilterLevel = GameManager.Instance.AutismFilterLevel;

        // --- 2. Apply Visual Settings using Local Variables ---
        
        // Set Darkness Level (using local variable blindnessFog)
        RenderSettings.fogDensity = blindnessFog;

        // Set Blur Intensity (using local variable blindnessBlur)
        if(DOF != null)
        {
            DOF.focalLength.value = blindnessBlur;
        }
        
        // --- NEW APPLICATION: CONTROL CHROMATIC ABERRATION AND LENS DISTORTION ---
        // A higher AutismFilterLevel (closer to 1.0) means more filtering/simplicity,
        // so we set the intensity/distortion closer to 0 (off).
        float simplicityFactor = 1.0f - autismFilterLevel; // 1.0 = Max Effect, 0.0 = Min Effect

        if(ChromAb != null)
        {
            // Chromatic Aberration intensity is typically from 0 to 1.
            // When autismFilterLevel is 1.0, simplicityFactor is 0.0, turning the effect OFF.
            ChromAb.intensity.value = autismFilterLevel;
        }

        aud.volume = autismFilterLevel;

        if(LensD != null)
        {
            // Lens Distortion intensity is typically from -1 to 1. We'll use 0.5 as max distortion.
            // When autismFilterLevel is 1.0, simplicityFactor is 0.0, turning the effect OFF.
            LensD.intensity.value = autismFilterLevel * 0.6f; 
        }
        // ----------------------------------------------------------------------
        
        // --- 3. Standard Movement and Gravity ---

        isGrounded = Physics.CheckSphere(feet.position, groundDistance, groundMask);

        // NON WHEELCHAIR LOGIC
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Check Wheelchair setting using local variable inWheelchair
        if(!inWheelchair){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
        }

        // Gravity Stuff
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}