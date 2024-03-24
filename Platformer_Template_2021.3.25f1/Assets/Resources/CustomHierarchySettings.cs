using UnityEngine;

[CreateAssetMenu(fileName = "CustomHierarchySettings", menuName = "CustomHierarchySettings")] // <- Must be correct!
public class CustomHierarchySettings : ScriptableObject
{
    //Default
    public bool isEnabled = true;
    public string headerPrefix = "//";
    public Color headerColor = Color.black;


    [Range(0f, 1f)]
    private float headerAlpha = 1f;

    // Define an event delegate
    public delegate void SettingsChangedDelegate();
    public event SettingsChangedDelegate OnSettingsChanged;

    private string previousHeaderPrefix;
    private Color previousHeaderColor;
    private float previousHeaderAlpha;
    private bool previousEnabledState;

    private void OnValidate()
    {
        if (headerPrefix != previousHeaderPrefix || headerColor != previousHeaderColor || headerAlpha != previousHeaderAlpha || isEnabled != previousEnabledState)
        {
            previousHeaderPrefix = headerPrefix;
            previousHeaderColor = headerColor;
            previousHeaderAlpha = headerAlpha;
            previousEnabledState = isEnabled;

            if (OnSettingsChanged != null)
            {
                OnSettingsChanged();
            }
        }
    }

    public void ExcludeObjectsFromBuild()
    {
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (isEnabled && obj.name.StartsWith(headerPrefix, System.StringComparison.Ordinal))
            {
                Object.DestroyImmediate(obj);
            }
        }
    }
}
