﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreBoxUIController : MonoBehaviour
{
    public RectTransform entriesParent;

    public StoreBoxEntryUIController entryPrefab;

    public event Action<ReactorPartDef> partSelected;

    public void Initialize(IEnumerable<ReactorPartDef> parts)
    {
        foreach (var part in parts.OrderBy(p => p.price))
        {
            var entry = Instantiate(entryPrefab, entriesParent);
            entry.Initialize(part);
            entry.selected += OnPartSelected;
        }
    }

    private void OnPartSelected(ReactorPartDef part)
    {
        partSelected?.Invoke(part);
    }
}