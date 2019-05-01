﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnappingGrid))]
public class Shop : MonoBehaviour
{
    [SerializeField] Module[] modulePrefabs = null;
    [SerializeField] SnappingGrid stand;
    [SerializeField] int slotCount;

    int availableTier = 1;

    void Reset() => stand = GetComponent<SnappingGrid>();

    void Start() => Populate();

    void Populate()
    {
        for (int i = 0; i < slotCount; i++)
            AddArticle(i);
    }

    void ProcessTransaction(Module article, int freedSlot)
    {
        availableTier = Mathf.Max(availableTier, article.Tier + 1);
        AddArticle(freedSlot);
    }

    void AddArticle(int slotId)
    {
        int index = Random.Range(0, modulePrefabs.Length);
        int tier = Random.Range(1, 1 + 1);
        Module article = Instantiate(modulePrefabs[index], stand.GetCellCenter(slotId, 0), Quaternion.identity);
        article.Tierify(tier);
        article.OnBought += (a) => ProcessTransaction(a, slotId);
        stand.TrySnap(article);
    }
}