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
        public override string Type => "Horizontal Layout";

#region [INSPECTOR]

        public int Spacing { get; private set; }

#endregion


        public HorizontalLayoutDrawer(Rect rect, DTossCreator data) : base(rect, data)
        {
            Spacing = 0;
        }
        
        public HorizontalLayoutDrawer(Rect rect, 
            DTossCreator data, 
            IPropertiesHorizontalLayout drawer) 
            : base(rect, data)
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