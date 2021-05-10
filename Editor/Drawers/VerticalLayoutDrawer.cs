// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 06.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.ComponentProperties;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Drawers
{
    public class VerticalLayoutDrawer : BaseDrawer, IPropertiesVerticalLayout
    {
#region [INSPECTOR]

        public int Spacing { get; private set; }

#endregion

        public override string Type => "Vertical Layout";

        public VerticalLayoutDrawer(Rect rect, DTestScriptable data) : base(rect, data)
        {
            Spacing = 0;
        }

        public override void DrawNode()
        {
            GUI.color = nodeBackgroundColor;
            GUI.Box(rect, string.Empty, data._skin.GetStyle("NodeBodyBg"));
            GUI.color = Color.white;

            var icon = new GUIContent(
                EditorGUIUtility.IconContent("d_VerticalLayoutGroup Icon")).image;
            DrawNodeTitle(icon);
        }

        public override void DrawInspector()
        {
            Spacing = Mathf.Clamp(EditorGUILayout.IntField("Spacing:", Spacing), 0, 10_000);
        }
    }
}