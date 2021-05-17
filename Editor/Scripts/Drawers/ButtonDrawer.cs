// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using System;
using Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Drawers
{
    [Serializable]
    public class ButtonDrawer : BaseDrawer, IPropertiesButton
    {
        public override string Type => "Button";

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

#endregion

        public ButtonDrawer(Rect rect, DTossCreator data) : base(rect, data)
        {
            Name = "BtnName";
            Width = data.DefaultValues.Button.Width;
            Height = data.DefaultValues.Button.Height;
            nodeBackgroundColor = data.DefaultValues.Button.NodeColor;
        }

        public override void DrawNode()
        {
            DrawNodeBackground();

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_Button Icon")).image;
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
        }
    }
}