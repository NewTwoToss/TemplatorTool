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
    public class GridLayoutDrawer : BaseDrawer, IPropertiesGridLayout
    {
        public override string Type => "Grid Layout";
        
        public override float InspectorHeight => 100.0f;

#region [INSPECTOR]

        public Vector2 Spacing { get; private set; }

#endregion

        public GridLayoutDrawer(Rect rect, DTemplatorCore core) : base(rect, core)
        {
            Spacing = Vector2.zero;
        }

        public GridLayoutDrawer(Rect rect, DTemplatorCore core, IPropertiesGridLayout drawer)
            : base(rect, core)
        {
            Spacing = drawer.Spacing;
        }

        public override void DrawNode()
        {
            DrawNodeBackground();

            const string ICON_NAME = "d_GridLayoutGroup Icon";
            var icon = new GUIContent(EditorGUIUtility.IconContent(ICON_NAME)).image;
            DrawNodeTitle(icon);
        }

        public override void DrawInspector()
        {
            Spacing = EditorGUILayout.Vector2Field("Spacing", Spacing);
        }
    }
}