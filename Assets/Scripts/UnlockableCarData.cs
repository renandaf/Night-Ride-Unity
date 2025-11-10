using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockableCarData", menuName = "Carithmetic/Unlockable Car Data")]
public class UnlockableCarData : ScriptableObject
{
    public int Id;
    public string Name;
    public GameObject Model;
    public int Price;
    public float Speed;
    public int Health;
}
