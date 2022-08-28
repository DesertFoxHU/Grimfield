using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), ExecuteAlways()]
public class MaterialInstancing : MonoBehaviour
{
    private MaterialPropertyBlock materialBlock;
    private SpriteRenderer sRenderer;

    public Color input;
    //public string exposeField;

    private void Awake()
    {
        materialBlock = new MaterialPropertyBlock();
        sRenderer = GetComponent<SpriteRenderer>();
        ChangePropertyBlock();
    }

    [ContextMenu("Update")]
    private void ChangePropertyBlock()
    {
        sRenderer.GetPropertyBlock(materialBlock);
        materialBlock.SetColor("_Color", input);
        sRenderer.SetPropertyBlock(materialBlock);
    }
}
