using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyBoard : MonoBehaviour
{

    private Key[] row1;
    private Key[] row2;
    private Key[] row3;
    private Key[] row4;

    bool shift = false;

    // Start is called before the first frame update
    void Start()
    {
        fillRowArrays();
        fillrow1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fill rowarrays from keyboard with Layout
    private void fillRowArrays()
    {
        Layout layout = new Layout();
        row1 = layout.getRow1();
        row2 = layout.getRow2();
        row3 = layout.getRow3();
        row4 = layout.getRow4();
    }

    private void fillrow1()
    {
        Transform Canvas = this.gameObject.transform.Find("Canvas");
        Transform Numbers = Canvas.Find("numbers");
        Transform ReturnEnter = Canvas.Find("returnenter");
        Transform Letters = Canvas.Find("LetterPad");

        Transform numberrow1 = Numbers.Find("Row1");
        for(int i = 0; i <= numberrow1.childCount-1; i++)
        {
            Transform text = numberrow1.GetChild(i).Find("Text (TMP)");
            TextMeshProUGUI  tmprotext = text.GetComponent<TextMeshProUGUI>();
            tmprotext.text = row1[i].KeyValue;
        }

        Transform Lettersrow1 = Letters.Find("Row1");
        for (int j = 0; j <= Lettersrow1.childCount-1; j++)
        {
            Transform text = Lettersrow1.GetChild(j).Find("Text (TMP)");
            TextMeshProUGUI tmprotext = text.GetComponent<TextMeshProUGUI>();
            tmprotext.text = row1[j+3].KeyValue;
        }

        Transform ReturnEnterrow1 = ReturnEnter.Find("Row1");
        Debug.Log(ReturnEnterrow1.childCount);
        for (int j = 0; j <= ReturnEnterrow1.childCount - 1; j++)
        {
            Transform text = ReturnEnterrow1.GetChild(j).Find("Text (TMP)");
            TextMeshProUGUI tmprotext = text.GetComponent<TextMeshProUGUI>();
            tmprotext.text = row1[j +14].KeyValue;
            Debug.Log("KeyValue: " +  row1[j + 14].KeyValue);       
        }

    }
}
