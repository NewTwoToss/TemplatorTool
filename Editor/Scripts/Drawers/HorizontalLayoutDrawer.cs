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
    public class HorizontalLayoutDrawer : BaseDrawer, IPropertiesHorizontalLayout
    {
#region [INSPECTOR]

        public int Spacing { get; private set; }

#endregion

        public override string Type => "Horizontal Layout";

        public HorizontalLayoutDrawer(Rect rect, DTossCreator data) : base(rect, data)
        {
            Spacing = 0;
        }

        public override void DrawNode(int index, int level)
        {
            DrawNodeBackground();

            const string ICON_NAME = "d_HorizontalLayoutGroup Icon";
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