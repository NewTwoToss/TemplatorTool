// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 06.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Drawers.Base;
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

        public override void DrawNode(int index, int level)
        {
            DrawNodeBackground();

            const string ICON_NAME = "d_VerticalLayoutGroup Icon";
            var icon = new GUIContent(EditorGUIUtility.IconContent(ICON_NAME)).image;
            DrawNodeTitle(icon, index, level);
        }

        public override void DrawInspector()
        {
            Spacing = Mathf
                .Clamp(EditorGUILayout.IntField("Spacing", Spacing), 0, MAX_NUMBER_VALUE);
        }
    }
}