using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PrefabTag
{
    Children, Teachers, Mark
}

[Serializable]
public class PrefabItem
{
    public GameObject prefab;
    public float chance = 1;
    public PrefabTag tag;
};

[CreateAssetMenu(fileName = "PrefabCollection", menuName = "Scriptable Objects/PrefabCollection")]
public class PrefabCollection : ScriptableObject
{
    // public interface
    [SerializeField] private List<PrefabItem> prefabs = new();
    public List<PrefabItem> Prefabs => prefabs;
    
    // helper definition for internal calculations
    private record PrefabSubCollection(List<PrefabItem> Prefabs, float TotalChance);

    // values to precalculate (IMO this is better to do this in a separate class)
    private readonly Dictionary<PrefabTag, PrefabSubCollection> _prefabSubCollections = new();
    private readonly System.Random _random = new();
    private PrefabSubCollection _defaultSubcollection;

    private void Awake() => PrecalculateInternals();
#if UNITY_EDITOR
    private void OnValidate() => PrecalculateInternals();
#endif

    /// <summary>
    ///     Utility function used for precalculating subcollections and total chance across the board
    /// </summary>
    private void PrecalculateInternals()
    {
        float totalChance = 0f;
        _prefabSubCollections.Clear();
        
        var prefabTags = Enum.GetValues(typeof(PrefabTag)).Cast<PrefabTag>().ToArray();
        foreach (PrefabTag tag in prefabTags)
        {
            var subcollection = prefabs.Where(prefabItem => prefabItem.tag == tag).ToList();
            float subcollectionTotalChance =
                subcollection.Sum(subcollectionItem => subcollectionItem.chance);

            PrefabSubCollection prefabSubcollection = new(subcollection, subcollectionTotalChance);
            _prefabSubCollections[tag] = prefabSubcollection;
            
            totalChance += subcollectionTotalChance;
        }

        _defaultSubcollection = new PrefabSubCollection(prefabs, totalChance);
    }
    
    public PrefabItem GetRandomPrefab() => GetRandomPrefab(_defaultSubcollection);
    public PrefabItem GetRandomPrefab(PrefabTag tag) => GetRandomPrefab(_prefabSubCollections[tag]);

    private PrefabItem GetRandomPrefab(PrefabSubCollection subCollection)
    {
        if (subCollection.Prefabs == null || subCollection.Prefabs.Count == 0) return null;

        // A variation of this: https://stackoverflow.com/a/4463622
        float randomValue = (float)_random.NextDouble() * subCollection.TotalChance;
        float currentTotal = 0f;
        foreach (PrefabItem prefab in subCollection.Prefabs)
        {
            currentTotal += prefab.chance;
            if (randomValue <= currentTotal) { return prefab; }
        }

        return prefabs[^1];
    }
}
