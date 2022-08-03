using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blueprinting : MonoBehaviour
{
    public GameObject blueprint;
    [HideInInspector] public BuildingDefinition current = null;

    public bool IsBlueprinting() { return current != null; }

    public void Cancel() { current = null; }

    public void Show()
    {
        blueprint.SetActive(true);
    }

    public void Hide()
    {
        blueprint.SetActive(false);
    }

    public void SetPosition(Vector3 vector3)
    {

    }

    public void ChangeBlueprint(BuildingDefinition definition)
    {
        this.current = definition;
    }
}
