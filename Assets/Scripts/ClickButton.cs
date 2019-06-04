using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abgewandelte Klasse zum Darstellen eines Drückbaren Buttons.
/// Grundlegend war das Skript "ResetButton" im VIU Beispiel "Collider Event".
/// </summary>
public class ClickButton : MonoBehaviour
    , IColliderEventPressUpHandler
    , IColliderEventPressEnterHandler
    , IColliderEventPressExitHandler
{
    public Transform buttonObject;
    public Vector3 buttonDownDisplacement;
    public bool clickable;

    [SerializeField]
    private ColliderButtonEventData.InputButton m_activeButton = ColliderButtonEventData.InputButton.Trigger;
    private RigidPose[] resetPoses;
    private HashSet<ColliderButtonEventData> pressingEvents = new HashSet<ColliderButtonEventData>();

    private GameObject buttonBase;
    private GameObject button;

    /// <summary>
    /// Der genutzte Button am ViveController. Einstellbar in der Szene in Unity.
    /// </summary>
    public ColliderButtonEventData.InputButton activeButton
    {
        get
        {
            return m_activeButton;
        }
        set
        {
            m_activeButton = value;
            // set all child MaterialChanger heighlightButton to value;
            var changers = ListPool<MaterialChanger>.Get();
            GetComponentsInChildren(changers);
            for (int i = changers.Count - 1; i >= 0; --i) { changers[i].heighlightButton = value; }
            ListPool<MaterialChanger>.Release(changers);
        }
    }

    // Wird zur Initialisierung genutzt.
    private void Start()
    {
        getButtonGameObjects();
        switchCollider(clickable);        
    }

    /// <summary>
    /// Liest die zwei GameObjects des Knopfes zur späteren Verwendung aus.
    /// </summary>
    private void getButtonGameObjects()
    {
        if (this.transform.GetChild(0).name.Equals("Base") || this.transform.GetChild(0).name.Equals("base"))
        {
            buttonBase = this.transform.GetChild(0).gameObject;
            button = this.transform.GetChild(1).gameObject;
        }
        else
        {
            buttonBase = this.transform.GetChild(1).gameObject;
            button = this.transform.GetChild(0).gameObject;
        }
    }

    /// <summary>
    /// Methode stellt die Collider ein, sodass die gewünschte Funktion des Knopfes gegeben ist.
    /// </summary>
    /// <param name="clickable"></param>
    private void switchCollider(bool clickable)
    {
        if (clickable)
        {
            buttonBase.GetComponent<CapsuleCollider>().enabled = true;
            button.GetComponent<MeshCollider>().enabled = false;
        }
        else
        {
            buttonBase.GetComponent<CapsuleCollider>().enabled = false;
            button.GetComponent<MeshCollider>().enabled = true;
        }
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        activeButton = m_activeButton;
    }
#endif

    /// <summary>
    /// Methode wird aufgerufen, wenn der Button gedrückt wird.
    /// </summary>
    /// <param name="eventData">Enthält Infos über das ausgelöste Event. Beispielsweise den gedrückten Knopf am Controller.</param>
    public void OnColliderEventPressUp(ColliderButtonEventData eventData)
    {
        //Nur der Trigger löst die gewünschte Aktion aus.
        if (eventData.button.ToString().Equals("Trigger"))
        {
            loadData();
        }
    }

    /// <summary>
    /// Methode die Aufgerufen wird, wenn wir mit dem Controller den Button berühren.
    /// Sofern clickable auf false gesetzt wurde.
    /// </summary>
    /// <param name="collider">Das Objekt, das mit dem Collider in Berührung kommt.</param>
    /// <param name="name">Name des berührten Objektes.</param>
    public void myTriggerEnter(Collider collider, string name)
    {
        Debug.Log("Name: " + name);
    }

    /// <summary>
    /// Methode um die Ausgewählten Daten auf der Hauptanzeige darzustellen.
    /// </summary>
    private void loadData()
    {
        Debug.Log("Load Data " + this.name);
    }

    /// <summary>
    /// Wird aufgerufen wenn der Button gedrückt wird.
    /// Zusätzlich wird der Button in seine "gedrückte" Position gesetzt.
    /// </summary>
    /// <param name="eventData">Enthält Infos über das ausgelöste Event.</param>
    public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        if (eventData.button == m_activeButton && pressingEvents.Add(eventData) && pressingEvents.Count == 1)
        {
            buttonObject.localPosition += buttonDownDisplacement;
        }
    }

    /// <summary>
    /// Wird aufgerufen wenn der Button losgelassen wird.
    /// Zusätzlich wird der Button wieder an die ursprüngliche Position gesetzt.
    /// </summary>
    /// <param name="eventData">Enthält Infos über das ausgelöste Event.</param>
    public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {
        if (pressingEvents.Remove(eventData) && pressingEvents.Count == 0)
        {
            buttonObject.localPosition -= buttonDownDisplacement;
        }
    }
}
