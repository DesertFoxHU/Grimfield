using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Marks a building to be able to toggle it's active status
/// </summary>
public interface IDisable
{
    public void SetActive(bool IsActive);

    public bool IsActive();
}
