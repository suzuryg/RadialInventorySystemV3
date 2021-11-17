using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;

namespace YagihataItems.RadialInventorySystemV3
{
    [CustomEditor(typeof(RISSettings))]
    public class RISSettingsEditor : Editor
    {
        private ReorderableList propGroupsReorderableList;

        void OnEnable()
        {
            var settings = target as RISSettings;
            var groups = settings.Groups;
            propGroupsReorderableList = new ReorderableList(groups, typeof(PropGroup))
            {
                drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, $"{"PropGroups"}: {groups.Count}", EditorStyles.boldLabel);
                },
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    if (groups.Count <= index)
                        return;
                    GUI.Label(rect, groups[index].GroupName);
                },

                drawFooterCallback = rect => { },
                footerHeight = 0f,
                elementHeightCallback = index =>
                {
                    if (groups.Count <= index)
                        return 0;
                    return EditorGUIUtility.singleLineHeight;
                }
            };
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var settings = target as RISSettings;

            var avatarRoot = settings.AvatarRoot;
            settings.AvatarRoot = EditorGUILayout.ObjectField("Avatar Root", settings.AvatarRoot, typeof(VRCAvatarDescriptor), true) as VRCAvatarDescriptor;
            if (settings.AvatarRoot != avatarRoot)
                EditorUtility.SetDirty(settings);

            var writeDefaults = settings.WriteDefaults;
            settings.WriteDefaults = EditorGUILayout.Toggle("Write Defaults", settings.WriteDefaults);
            if (settings.WriteDefaults != writeDefaults)
                EditorUtility.SetDirty(settings);

            var folderID = settings.FolderID;
            settings.FolderID = EditorGUILayout.TextField("Folder ID", settings.FolderID);
            if (settings.FolderID != folderID)
                EditorUtility.SetDirty(settings);

            var listHash = settings.Groups.GetHashCode();
            var listProp = serializedObject.FindProperty("Groups");
            propGroupsReorderableList.DoLayoutList();
            if (settings.Groups.GetHashCode() != listHash)
                EditorUtility.SetDirty(settings);

            var menuMode = settings.MenuMode;
            settings.MenuMode = (RISV3.RISMode)EditorGUILayout.EnumPopup("Menu Mode", settings.MenuMode);
            if (settings.MenuMode != menuMode)
                EditorUtility.SetDirty(settings);

            var applyEnabled = settings.ApplyEnabled;
            settings.ApplyEnabled = EditorGUILayout.Toggle("Apply Enabled", settings.ApplyEnabled);
            if (settings.ApplyEnabled != applyEnabled)
                EditorUtility.SetDirty(settings);

            var useCommonMenu = settings.UseCommonMenu;
            settings.UseCommonMenu = EditorGUILayout.Toggle("UseCommonMenu", settings.UseCommonMenu);
            if (settings.UseCommonMenu != useCommonMenu)
                EditorUtility.SetDirty(settings);

            var commonFXLayer = settings.CommonFXLayer;
            settings.CommonFXLayer = EditorGUILayout.ObjectField("Controller", settings.CommonFXLayer, typeof(RuntimeAnimatorController), true) as RuntimeAnimatorController;
            if (settings.CommonFXLayer != commonFXLayer)
                EditorUtility.SetDirty(settings);

            var commonMenu = settings.CommonMenu;
            settings.CommonMenu = EditorGUILayout.ObjectField("Expression Menu", settings.CommonMenu, typeof(VRCExpressionsMenu), true) as VRCExpressionsMenu;
            if (settings.CommonMenu != commonMenu)
                EditorUtility.SetDirty(settings);

            var commonParameters = settings.CommonParameters;
            settings.CommonParameters = EditorGUILayout.ObjectField("Expression Parameters", settings.CommonParameters, typeof(VRCExpressionParameters), true) as VRCExpressionParameters;
            if (settings.CommonParameters != commonParameters)
                EditorUtility.SetDirty(settings);

            serializedObject.ApplyModifiedProperties();
        }
    }
}