using UnityEngine;
using System;
using Random = System.Random;

[Serializable]
public class TileVariant
{
    [SerializeField] GameObject[] m_variants = new GameObject[0];

    public GameObject GetRandomTile()
    {
        Random random = SharedLevelData.Instance.Rand;
        if (m_variants.Length == 0) { return null; }
        int randomIndex = random.Next(0, m_variants.Length);
        return m_variants[randomIndex];
    }
}
