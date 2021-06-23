using TMPro;
using UnityEngine;

public class IntToStringText : MonoBehaviour
{
    [Header("Setup")]
    public string prefix = "";
    public string suffix = "";
    public TextMeshProUGUI textToConvert;

    public void SetText(int number)
    {
        textToConvert.text = prefix + number + suffix;
    }
}
