using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float Health { get; set; }
    float Shield { get; set; }

    void TakeDamage(float amount);
    void Die();
}
