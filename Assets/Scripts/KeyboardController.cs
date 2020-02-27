using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userText;


    public void updateTextUI(string letter)
    {
        string currentText = userText.text;
        StringBuilder builder = new StringBuilder(currentText);
        builder.Append(letter);
        userText.text = builder.ToString();
    }
}
