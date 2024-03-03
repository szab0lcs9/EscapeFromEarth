using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ammo : ScriptableObject
{
    const float MASS_MULTIPLIER = 4f;

    [Range(1, 10)]
    public float velocity = 1f;

    [Range(0.01f, 10f)]
    public  float mass = 0.1f;
    public float Damage { get => mass * MASS_MULTIPLIER * velocity * velocity; }
}
