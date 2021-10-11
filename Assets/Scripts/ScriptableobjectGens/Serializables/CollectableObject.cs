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
}
