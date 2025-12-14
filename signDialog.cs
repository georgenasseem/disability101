using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class signDialog : MonoBehaviour
{
    public playerMove playerGuy;
    public string regularText;
    public string dyslexicText;
    private TextMeshPro tmpPro;



    void Start()
    {
        tmpPro = GetComponent<TextMeshPro>();
    }
    void Update()
    {
        if(playerGuy.isDyslexic){tmpPro.text = dyslexicText;} else {tmpPro.text = regularText;}
    }
}
