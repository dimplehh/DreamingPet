using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ToyDatabase : ScriptableObject
{
    public Toy[] toy;

    public Toy GetToy(int index)
    {
        return toy[index];
    }
}
