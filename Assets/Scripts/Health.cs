using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 5;
    public int GetHealth()
    {
        return health;
    }
    public void RemoveHealth()
    {
        health--;
    }
}
