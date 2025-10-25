using UnityEngine;
using System;
using Random = System.Random;

[ExecuteAlways]
[DisallowMultipleComponent]
public class SharedLevelData : MonoBehaviour
{
    public static SharedLevelData Instance { get; private set; }
    [SerializeField] private int m_scale = 1;
    [SerializeField] private int m_seed = Environment.TickCount;
    Random m_random;
    public int Scale => m_scale;
    public Random Rand => m_random;

    [ContextMenu("Generate New Seed")]
    public void GenerateNewSeed()
    {
        m_seed = Environment.TickCount;
        m_random = new Random(m_seed);
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
        m_random = new Random(m_seed);
    }

    public void ResetRandom()
    {
        m_random = new Random(m_seed);
    }
}
