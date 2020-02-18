using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipBoardController : MonoBehaviour
{
    public GameObject clipboardHolder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void populateDropdownList(Dropdown dropdown, List<GameObject> valueList)
    {
        List<string> stringValueList = new List<string>();
        foreach (GameObject gameObject in valueList)
        {
            stringValueList.Add(gameObject.name);
        }
        dropdown.AddOptions(stringValueList);
    }

    public void disable()
    {
        clipboardHolder.SetActive(false);
    }

    public void enable()
    {
        clipboardHolder.SetActive(true);
    }
}
