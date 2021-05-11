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
    public class TextDrawer : BaseDrawer, IPropertiesText
    {
        public override string Type => "Text (TMP)";

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public string Text { get; private set; }

#endregion

        public TextDrawer(Rect rect, DTestScriptable data) : base(rect, data)
        {
            Name = "TxtName";
            Width = 100;
            Height = 40;
            Text = "New Text";
            nodeBackgroundColor = new Color(0.7f, 1.0f, 1.0f);
        }

        public override void DrawNode(int index, int level)
        {
            DrawNodeBackground();

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_Button Icon")).image;
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

            GUISpaceSmall();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Text");
            Text = GUILayout.TextField(Text, MAX_TEXT_FIELD_LENGTH);
            GUILayout.EndHorizontal();
        }
    }
}