using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Road
{
    public float roadLength;
    public Collectableobject[] collectableObjectsOnRoad;

    public Road(float roadLength,Collectableobject[] collectableobjects)
    {
        this.roadLength = roadLength;
        this.collectableObjectsOnRoad = collectableobjects;
    }
}
