// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

using System;
using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Core;
using Plugins.Templator.Editor.Scripts.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers
{
    [Serializable]
    public class TextDrawer : BaseDrawer, IPropertiesText
    {
        public override string Type => "Text (TMP)";

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public string Text { get; private set; }

#endregion

        public TextDrawer(Rect rect, TemplatorCore data) : base(rect, data)
        {
            Name = "TxtName";
            Width = data.DefaultValues.Text.Width;
            Height = data.DefaultValues.Text.Height;
            Text = "New Text";
            nodeBackgroundColor = data.DefaultValues.Text.NodeColor;
        }
        
        public TextDrawer(Rect rect, TemplatorCore data, IPropertiesText drawer) 
            : base(rect, data)
        {
            Name = drawer.Name;
            Width = drawer.Width;
            Height = drawer.Height;
            Text = drawer.Text;
            nodeBackgroundColor = data.DefaultValues.Text.NodeColor;
        }

        public override void DrawNode()
        {
            DrawNodeBackground();

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_Text Icon")).image;
            DrawNodeTitle(icon);

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