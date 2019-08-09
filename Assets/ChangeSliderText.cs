using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSliderText : MonoBehaviour {

    private Text xSize;
    private Text zSize;
    private Slider xSizeSlider;
    private Slider zSizeSlider;

    private void Awake()
    {
        xSize = GameObject.FindGameObjectWithTag("XSize").GetComponent<Text>();
        zSize = GameObject.FindGameObjectWithTag("ZSize").GetComponent<Text>();
        xSizeSlider = GameObject.FindGameObjectWithTag("XSizeSlider").GetComponent<Slider>();
        zSizeSlider = GameObject.FindGameObjectWithTag("ZSizeSlider").GetComponent<Slider>();
    }

    // Use this for initialization
    void Start () {
		
	}

    public void setXSize()
    {
        xSize.text = xSizeSlider.value.ToString();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Maze>().setXSize();
    }

    public void setZSize()
    {
        zSize.text = zSizeSlider.value.ToString();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Maze>().setZSize();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
