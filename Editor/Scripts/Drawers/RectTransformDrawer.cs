// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Drawers
{
    public class RectTransformDrawer : BaseDrawer, IPropertiesRectTransform
    {
        public override string Type => "Rect Transform";

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public RectTransformDrawer(Rect rect, DTestScriptable data) : base(rect, data)
        {
            Name = "RtName";
            Width = 220;
            Height = 100;
            nodeBackgroundColor = new Color(0.7f, 0.7f, 1.0f);
        }

        public override void DrawNode(int index, int level)
        {
            DrawNodeBackground();

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_RectTransform Icon")).image;
            DrawNodeTitle(icon, index, level);
            
            var labelText = $"{Name} [{Width}x{Height}]";
            DrawBody(labelText);
        }

        public override void DrawInspector()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name");
            Name = GUILayout.TextField(Name, MAX_TEXT_FIELD_LENGTH);
            GUILayout.EndHorizontal();

            GUISpaceBig();

            Width = Mathf.Clamp(EditorGUILayout.IntField("Width", Width), 2, MAX_NUMBER_VALUE);

            GUISpaceSmall();
            
            Height = Mathf.Clamp(EditorGUILayout.IntField("Height", Height), 2, MAX_NUMBER_VALUE);
        }
    }
}