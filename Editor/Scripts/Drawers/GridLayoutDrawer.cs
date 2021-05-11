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
#region [INSPECTOR]

        public Vector2 Spacing { get; private set; }

#endregion

        public override string Type => "Grid Layout";

        public GridLayoutDrawer(Rect rect, DTestScriptable data) : base(rect, data)
        {
            Spacing = Vector2.zero;
        }

        public override void DrawNode(int index, int level)
        {
            DrawNodeBackground();

            const string ICON_NAME = "d_GridLayoutGroup Icon";
            var icon = new GUIContent(EditorGUIUtility.IconContent(ICON_NAME)).image;
            DrawNodeTitle(icon, index, level);
        }

        public override void DrawInspector()
        {
            Spacing = EditorGUILayout.Vector2Field("Spacing", Spacing);
        }
    }
}