using UnityEngine;
using System;
using Random = System.Random;

[ExecuteAlways]
[DisallowMultipleComponent]
public class SharedLevelData : MonoBehaviour
{
    public static SharedLevelData Instance { get; private set; }
    [SerializeField] private int _scale = 1;
    [SerializeField] private int _seed = Environment.TickCount;
    Random _random;
    public int Scale => _scale;
    public Random Rand => _random;

    [ContextMenu("Generate New Seed")]
    public void GenerateSeed()
    {
        _seed = Environment.TickCount;
        _random = new Random(_seed);
    }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            enabled = false;
            Debug.LogWarning("Duplicate SharedLevelData instance detected. Disabling the new one.", this);
        }
        Debug.Log(Instance.GetInstanceID());
        _random = new Random(_seed);
    }

    public void ResetRandom()
    {
        _random = new Random(_seed);
    }
}
