using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class npcDialog : MonoBehaviour
{
    // Update is called once per frame
    public LayerMask playerMask;
    public playerMove playerGuy;
    public bool drawVisualizer;
    public string dyslexicDialog;
    public string regularDialog;
    public float waitTime = 5f;
    string finalText;
    private TextMeshProUGUI tmpPro;
    float playerDistance = 4.36f;
    bool isNear;

    void Awake()
{
    GameObject textObject = GameObject.FindGameObjectWithTag("TextDisplay");
    
    if (textObject != null)
    {
        tmpPro = textObject.GetComponent<TextMeshProUGUI>();
    }
    else
    {
        Debug.LogError("GameObject with Tag 'TextDisplay' not found!");
    }
}
    void Update()
    {
        if(!playerGuy.isTalking){tmpPro.text = "";}
        isNear = Physics.CheckSphere(transform.position, playerDistance, playerMask);
        if(isNear && Input.GetKeyDown("space"))
        {
            playerGuy.isTalking = true;
            if(playerGuy.isDyslexic){tmpPro.text = dyslexicDialog;} else{tmpPro.text = regularDialog;}
            StartCoroutine(turnFalseWithDelay(waitTime));
        }
    }

    IEnumerator turnFalseWithDelay(float wait){yield return new WaitForSeconds(wait); playerGuy.isTalking = false;}
     private void OnDrawGizmos()
    {
        // Set the color for the Gizmo (e.g., green for easy visibility)
        Gizmos.color = Color.green;


        // Draw a small solid sphere at the NPC's position.
        // This helps visualize the exact center point of the GameObject in the Scene view.
        if(drawVisualizer){Gizmos.DrawWireSphere(transform.position, playerDistance);}
    }
}
