using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ITransformator
{
    public Dictionary<ResourceType, double> From { get; set; }
    public Dictionary<ResourceType, double> To { get; set; }
}
