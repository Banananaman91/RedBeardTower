using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemy;
    public int count;
    [Range(2,.1f)]
    [Tooltip("Rate = 1 / value")]
    public float rate = 2;
}
