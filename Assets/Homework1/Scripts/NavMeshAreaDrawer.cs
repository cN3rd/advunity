#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Homework1
{
    // Originally from https://discussions.unity.com/t/creating-an-enum-for-navmesh-areas/725103/7
    // Imported it just to make UX slightly better
    [CustomPropertyDrawer(typeof(NavMeshAreaAttribute))]
    public class NavMeshAreaDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            // fetch and build data for the field
            string[] navMeshAreaNames = UnityEngine.AI.NavMesh.GetAreaNames();
            int currentArea = property.intValue;
            
            // find the array index that corresponds to the currentArea 
            // so the dropdown can display the correct default selection.
            int selectedDropdownItem = -1;
            for (int i = 0; i < navMeshAreaNames.Length; i++)
            {
                if (UnityEngine.AI.NavMesh.GetAreaFromName(navMeshAreaNames[i]) == currentArea)
                {
                    selectedDropdownItem = i;
                    break;
                }
            }

            // build field
            var popupField = new PopupField<string>(
                label: property.displayName,
                choices: new List<string>(navMeshAreaNames),
                defaultValue: selectedDropdownItem >= 0 ? navMeshAreaNames[selectedDropdownItem] : string.Empty
            );
            popupField.RegisterValueChangedCallback(changeEvent =>
            {
                int newAreaValue = UnityEngine.AI.NavMesh.GetAreaFromName(changeEvent.newValue);
                property.intValue = newAreaValue;
                property.serializedObject.ApplyModifiedProperties();
            });

            // fix field indentation using magic attribute
            // used by most other unity fields (including OnGUI drawers)
            // (credit: https://github.com/shloon/Event-Horizon/blob/6e685b4e9d3311dc98b40698784bfd59ac50e1eb/Packages/il.runiarl.eventhorizon/Editor/UI/FrameRatePropertyDrawer.cs#L16C1-L16C58)
            // ((an alt of cN3rd))
            popupField.AddToClassList("unity-base-field__aligned");
            
            root.Add(popupField);
            return root;
        }
    }

}
#endif
