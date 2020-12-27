using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IMouseDetection
{
    [Header("UI Compunents")]
    private Material material;
    private Color originalColor;
    public Color enterColor;

    private BuildManager buildManager;

    public string identifier;

    [HideInInspector]
    private bool isBuilt;
    public Transform buildPosition;

    private void Start()
    {
        buildManager = BuildManager.GetInstance();
        material = GetComponent<MeshRenderer>().material;
        originalColor = material.color;
        isBuilt = false;
    }

    public void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //Debug.Log("Button");
        if (!isBuilt)
        {
            buildManager.SetBuild(this, identifier);
        }
    }

    public void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() || isBuilt)
        {
            return;
        }
        material.color = enterColor;
    }
    public void OnMouseExit()
    {
        material.color = originalColor;
    }

    public void SetBuilt(bool isBuilt)
    {
        this.isBuilt = isBuilt;
    }

    public bool GetBuilt()
    {
        return isBuilt;
    }
}
