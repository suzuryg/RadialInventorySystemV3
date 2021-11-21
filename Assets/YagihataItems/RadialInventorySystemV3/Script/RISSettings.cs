using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
using YagihataItems.YagiUtils;

namespace YagihataItems.RadialInventorySystemV3
{
    public class RISSettings : MonoBehaviour, IEditorExtSettings
    {
        [SerializeField] public VRCAvatarDescriptor AvatarRoot { get { return risVariables.AvatarRoot; } set { risVariables.AvatarRoot = value; } }
        [SerializeField] public bool WriteDefaults { get { return risVariables.WriteDefaults; } set { risVariables.WriteDefaults = value; } }
        [SerializeField] public bool OptimizeParams { get { return risVariables.OptimizeParams; } set { risVariables.OptimizeParams = value; } }
        [SerializeField] public string FolderID { get { return risVariables.FolderID; } set { risVariables.FolderID = value; } }
        [SerializeField] public List<PropGroup> Groups { get { return risVariables.Groups; } set { risVariables.Groups = value; } }

        [SerializeField] /*[HideInInspector]*/ public RISVariables risVariables = new RISVariables();
        [SerializeField] public RISV3.RISMode MenuMode { get { return risVariables.MenuMode; } set { risVariables.MenuMode = value; } }
        [SerializeField] public bool ApplyEnabled { get { return risVariables.ApplyEnabled; } set { risVariables.ApplyEnabled = value; } }
        [SerializeField] public int[] AdvancedGroupMode { get { return risVariables.AdvancedGroupMode; } set { risVariables.AdvancedGroupMode = value; } }
        [SerializeField] public bool UseCommonMenu { get { return risVariables.UseCommonMenu; } set { risVariables.UseCommonMenu = value; } }
        [SerializeField] public RuntimeAnimatorController CommonFXLayer { get { return risVariables.CommonFXLayer; } set { risVariables.CommonFXLayer = value; } }
        [SerializeField] public VRCExpressionsMenu CommonMenu { get { return risVariables.CommonMenu; } set { risVariables.CommonMenu = value; } }
        [SerializeField] public VRCExpressionParameters CommonParameters { get { return risVariables.CommonParameters; } set { risVariables.CommonParameters = value; } }
        public void SetVariables(IEditorExtVariables variables)
        {
            if (!(variables is RISVariables))
                return;
            var risVariables = variables as RISVariables;
            this.AvatarRoot = risVariables.AvatarRoot;
            this.WriteDefaults = risVariables.WriteDefaults;
            this.OptimizeParams = risVariables.OptimizeParams;
            this.FolderID = risVariables.FolderID;
            this.Groups = risVariables.Groups;
            this.MenuMode = risVariables.MenuMode;
            this.ApplyEnabled = risVariables.ApplyEnabled;
            this.AdvancedGroupMode = risVariables.AdvancedGroupMode;
            this.UseCommonMenu = risVariables.UseCommonMenu;
            this.CommonFXLayer = risVariables.CommonFXLayer;
            this.CommonMenu = risVariables.CommonMenu;
            this.CommonParameters = risVariables.CommonParameters;
        }
        public IEditorExtVariables GetVariables()
        {
            return new RISVariables()
            {
                AvatarRoot = this.AvatarRoot,
                WriteDefaults = this.WriteDefaults,
                OptimizeParams = this.OptimizeParams,
                FolderID = this.FolderID,
                Groups = this.Groups,
                MenuMode = this.MenuMode,
                ApplyEnabled = this.ApplyEnabled,
                AdvancedGroupMode = this.AdvancedGroupMode,
                UseCommonMenu = this.UseCommonMenu,
                CommonFXLayer = this.CommonFXLayer,
                CommonMenu = this.CommonMenu,
                CommonParameters = this.CommonParameters
            };
        }
    }
    [System.Serializable]
    public class RISVariables : IEditorExtVariables, ICloneable
    {
        [SerializeField] public VRCAvatarDescriptor AvatarRoot;
        [SerializeField] public bool WriteDefaults = false;
        [SerializeField] public bool OptimizeParams = true;
        [SerializeField] public string FolderID = "";
        [SerializeField] public List<PropGroup> Groups = new List<PropGroup>();
        [SerializeField] public RISV3.RISMode MenuMode = RISV3.RISMode.Simple;
        [SerializeField] public bool ApplyEnabled = true;
        [SerializeField] public int[] AdvancedGroupMode = new int[Enum.GetNames(typeof(RISV3.PropGroup)).Length];
        [SerializeField] public bool UseCommonMenu = false;
        [SerializeField] public RuntimeAnimatorController CommonFXLayer;
        [SerializeField] public VRCExpressionsMenu CommonMenu;
        [SerializeField] public VRCExpressionParameters CommonParameters;

        public object Clone()
        {
            var obj = new RISVariables();
            obj.AvatarRoot = this.AvatarRoot;
            obj.WriteDefaults = this.WriteDefaults;
            obj.OptimizeParams = this.OptimizeParams;
            obj.FolderID = this.FolderID;
            obj.Groups.AddRange(this.Groups.Select(n => (PropGroup)n.Clone()));
            obj.MenuMode = this.MenuMode;
            obj.ApplyEnabled = this.ApplyEnabled;
            obj.AdvancedGroupMode = new int[this.AdvancedGroupMode.Length];
            Array.Copy(this.AdvancedGroupMode, obj.AdvancedGroupMode, this.AdvancedGroupMode.Length);
            obj.UseCommonMenu = this.UseCommonMenu;
            obj.CommonFXLayer = this.CommonFXLayer;
            obj.CommonMenu = this.CommonMenu;
            obj.CommonParameters = this.CommonParameters;
            return obj;
        }
    }
}