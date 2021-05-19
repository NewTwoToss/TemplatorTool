// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Drawers
{
    public class GridLayoutDrawer : BaseDrawer, IPropertiesGridLayout
    {
        public override string Type => "Grid Layout";

#region [INSPECTOR]

        public Vector2 Spacing { get; private set; }

#endregion

        public GridLayoutDrawer(Rect rect, DTossCreator data) : base(rect, data)
        {
            Spacing = Vector2.zero;
        }

        public GridLayoutDrawer(Rect rect, DTossCreator data, IPropertiesGridLayout drawer)
            : base(rect, data)
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