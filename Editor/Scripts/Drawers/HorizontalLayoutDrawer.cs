// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Core;
using Plugins.Templator.Editor.Scripts.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers
{
    public class HorizontalLayoutDrawer : BaseDrawer, IPropertiesHorizontalLayout
    {
        public override string Type => "Horizontal Layout";
        
        public override float InspectorHeight => 80.0f;

#region [INSPECTOR]

        public int Spacing { get; private set; }

#endregion


        public HorizontalLayoutDrawer(Rect rect, DTemplatorCore core) : base(rect, core)
        {
            Spacing = 0;
        }
        
        public HorizontalLayoutDrawer(Rect rect, 
            DTemplatorCore core, 
            IPropertiesHorizontalLayout drawer) 
            : base(rect, core)
        {
            Spacing = drawer.Spacing;
        }

        public override void DrawNode()
        {
            DrawNodeBackground();

            const string ICON_NAME = "d_HorizontalLayoutGroup Icon";
            var icon = new GUIContent(EditorGUIUtility.IconContent(ICON_NAME)).image;
            DrawNodeTitle(icon);
        }

        public override void DrawInspector()
        {
            Spacing = Mathf
                .Clamp(EditorGUILayout.IntField("Spacing", Spacing), 0, MAX_NUMBER_VALUE);
        }
    }
}