using UnityEngine;

public class InfoBoxUIController : MonoBehaviour
{
    public RectTransform entriesParent;

    public FloatInfoBoxEntryUIController entryPrefab;

    private FloatInfoBoxEntryUIController creditsEntry;

    private FloatInfoBoxEntryUIController demandEntry;

    private FloatInfoBoxEntryUIController outputEntry;

    public void UpdateValues(float credits, float demand, float output)
    {
        creditsEntry.EntryValue = credits;
        demandEntry.EntryValue = demand;
        outputEntry.EntryValue = output;
    }

    private void Start()
    {
        creditsEntry = CreateEntry("Credits", "{0:0.00} CR");
        demandEntry = CreateEntry("Demand", "{0:0.000} W");
        outputEntry = CreateEntry("Output", "{0:0.000} W");
    }

    private FloatInfoBoxEntryUIController CreateEntry(string entryName, string entryValueFormat = "{0}")
    {
        var entry = Instantiate(entryPrefab, entriesParent);
        entry.Initialize(entryName, entryValueFormat);
        return entry;
    }
}