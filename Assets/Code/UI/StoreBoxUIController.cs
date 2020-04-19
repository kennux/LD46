using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreBoxUIController : MonoBehaviour
{
    public RectTransform entriesParent;

    public StoreBoxEntryUIController entryPrefab;

    public Action<ReactorPartDef> partSelected;

    public void Initialize(IEnumerable<ReactorPartDef> parts)
    {
        foreach (var part in parts)
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