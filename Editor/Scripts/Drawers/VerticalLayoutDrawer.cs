// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 06.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Core;
using Plugins.Templator.Editor.Scripts.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers
{
    public class VerticalLayoutDrawer : BaseDrawer, IPropertiesVerticalLayout
    {
        public override string Type => "Vertical Layout";
        
        public override float InspectorHeight => 80.0f;

#region [INSPECTOR]

        public int Spacing { get; private set; }

#endregion


        public VerticalLayoutDrawer(Rect rect, DTemplatorCore core) : base(rect, core)
        {
            Spacing = 0;
        }

        public VerticalLayoutDrawer(Rect rect,
            DTemplatorCore core,
            IPropertiesVerticalLayout drawer)
            : base(rect, core)
        {
            Spacing = drawer.Spacing;
        }

        public override void DrawNode()
        {
            DrawNodeBackground();

            const string ICON_NAME = "d_VerticalLayoutGroup Icon";
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