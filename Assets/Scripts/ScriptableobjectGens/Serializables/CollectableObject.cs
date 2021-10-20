using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Collectableobject
{
    public enum ObjectType { Cube, Sphere, Cylinder };
    public ObjectType objectType;
    public float size;
    //public bool isExplosion;
    public Vector3 spawnPoint;

    public Collectableobject(ObjectType type,float size)
    {
        this.objectType = type;
        this.size = size;
        this.spawnPoint = Vector3.zero;
    }
}
