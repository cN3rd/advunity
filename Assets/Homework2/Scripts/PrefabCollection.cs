using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public enum PrefabTag
{
    Children, Teachers, LesserMark
}

[Serializable]
public class PrefabItem
{
    public GameObject prefab;
    public float chance = 1;
    public PrefabTag tag;
}

[CreateAssetMenu(fileName = "PrefabCollection", menuName = "Scriptable Objects/PrefabCollection")]
public class PrefabCollection : ScriptableObject
{
    // public interface
    [SerializeField] private List<PrefabItem> prefabs = new();

    // values to precalculate (IMO this is better to do this in a separate class)
    private readonly Dictionary<PrefabTag, PrefabSubCollection> _prefabSubCollections = new();
    private readonly Random _random = new();
    private PrefabSubCollection _defaultSubcollection;
    public List<PrefabItem> Prefabs => prefabs;

    // unity events triggers for precalculating things
    private void Awake() => PrecalculateInternals();
#if UNITY_EDITOR
    private void OnValidate() => PrecalculateInternals();
#endif

    /// <summary>
    ///     Precalculates subcollections and total chances across the board
    ///     For "fetch random element" operations
    /// </summary>
    private void PrecalculateInternals()
    {
        float totalChance = 0f;
        _prefabSubCollections.Clear();

        // build a subcollection for each tag so we'd be able to select random prefabs from it
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

    /// <summary>
    ///     Get a random prefab from the collection
    /// </summary>
    /// <returns>The prefab (as a game object)</returns>
    public GameObject GetRandomPrefab() => GetRandomPrefab(_defaultSubcollection);

    /// <summary>
    ///     Get a random prefab carrying a certain tag from the collection
    /// </summary>
    /// <param name="tag">Tag required by the prefab</param>
    /// <returns>The prefab (as a game object)</returns>
    public GameObject GetRandomPrefab(PrefabTag tag) => GetRandomPrefab(_prefabSubCollections[tag]);

    /// <summary>
    ///     Get a random prefab from a subcollection
    /// </summary>
    /// <param name="subCollection">the relevant subcollection</param>
    /// <returns>The prefab (as a game object)</returns>
    private GameObject GetRandomPrefab(PrefabSubCollection subCollection)
    {
        if (subCollection.Prefabs == null || subCollection.Prefabs.Count == 0) return null;

        // A variation of this: https://stackoverflow.com/a/4463622
        float randomValue = (float)_random.NextDouble() * subCollection.TotalChance;
        float currentTotal = 0f;
        foreach (PrefabItem prefabItem in subCollection.Prefabs)
        {
            currentTotal += prefabItem.chance;
            if (randomValue <= currentTotal) { return prefabItem.prefab; }
        }

        // ^1 is the syntax for the "last element of array"
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges#systemindex
        return prefabs[^1].prefab;
    }

    // helper definition for internal calculations
    private record PrefabSubCollection(List<PrefabItem> Prefabs, float TotalChance);
}
