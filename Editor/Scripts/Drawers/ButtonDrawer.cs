// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
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
    public class ButtonDrawer : BaseDrawer, IPropertiesButton
    {
        public override string Type => "Button";

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

#endregion

        public ButtonDrawer(Rect rect, DTemplatorCore core) : base(rect, core)
        {
            Name = "BtnName";
            Width = core.DefaultValues.Button.Width;
            Height = core.DefaultValues.Button.Height;
            nodeBackgroundColor = core.DefaultValues.Button.NodeColor;
        }

        public ButtonDrawer(Rect rect, DTemplatorCore core, IPropertiesButton drawer)
            : base(rect, core)
        {
            Name = drawer.Name;
            Width = drawer.Width;
            Height = drawer.Height;
            nodeBackgroundColor = core.DefaultValues.Button.NodeColor;
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