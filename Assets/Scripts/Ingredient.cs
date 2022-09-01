using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] Color color;

    public Color Color => color;
    public bool IsAddedToMixer { get; set; } = false;
}
