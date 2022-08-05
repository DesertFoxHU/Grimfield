using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IProducer
{
    public ResourceType Type { get; }
    public Dictionary<int, double> ProduceLevel { get; }
}
