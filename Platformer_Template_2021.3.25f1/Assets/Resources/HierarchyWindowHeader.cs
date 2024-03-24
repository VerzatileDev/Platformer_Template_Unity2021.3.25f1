using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class HierarchySectionHeader
{
    public static bool isEnabled = true;
    public static string headerPrefix = "//";
    public static Color headerColor = Color.black;

    static HierarchySectionHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;

        // Subscribe to the settings changed event
        CustomHierarchySettings settings = Resources.Load<CustomHierarchySettings>("CustomHierarchySettings");
        if (settings != null)
        {
            settings.OnSettingsChanged += UpdateHierarchySettings;
            // Initialize the header color and enabled state from the settings
            headerColor = settings.headerColor;
            isEnabled = settings.isEnabled;
        }
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null && isEnabled && !string.IsNullOrEmpty(HierarchySectionHeader.headerPrefix) &&
            gameObject.name.StartsWith(HierarchySectionHeader.headerPrefix, System.StringComparison.Ordinal))
        {
            EditorGUI.DrawRect(selectionRect, HierarchySectionHeader.headerColor);
            EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace(HierarchySectionHeader.headerPrefix, "").ToUpperInvariant());
        }
    }

    // Method to update hierarchy settings when the asset is changed
    static void UpdateHierarchySettings()
    {
        CustomHierarchySettings settings = Resources.Load<CustomHierarchySettings>("CustomHierarchySettings");
        if (settings != null)
        {
            headerPrefix = settings.headerPrefix;
            headerColor = settings.headerColor;
            isEnabled = settings.isEnabled;
            EditorApplication.RepaintHierarchyWindow();
        }
    }
}