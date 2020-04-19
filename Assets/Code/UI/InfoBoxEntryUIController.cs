using System;
using TMPro;
using UnityEngine;

public class InfoBoxEntryUIController<T> : MonoBehaviour where T : IEquatable<T>
{
    public TextMeshProUGUI _entryNameText;

    public TextMeshProUGUI _entryValueText;

    private T _value;

    public string EntryName
    {
        get => _entryNameText.text;
        set => _entryNameText.text = value;
    }

    public string EntryValueFormat { get; set; }

    public T EntryValue
    {
        get => _value;
        set
        {
            if (!_value.Equals(value))
            {
                _value = value;
                UpdateValueText();
            }
        }
    }

    public void Initialize(string entryName, string entryValueFormat, T entryValue = default)
    {
        EntryName = entryName;
        EntryValueFormat = entryValueFormat;
        EntryValue = entryValue;
        UpdateValueText();
    }

    private void UpdateValueText()
    {
        _entryValueText.text = string.Format(EntryValueFormat, _value);
    }
}