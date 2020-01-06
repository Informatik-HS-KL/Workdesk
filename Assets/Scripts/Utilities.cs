using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities
{
   public static object[] convertListToArray(List<object> list)
    {
        object[] array;
        array = list.ToArray();
        return array;
    }

    public static string getSceneAsString()
    {
        return SceneManager.GetActiveScene().name;
    }
}
