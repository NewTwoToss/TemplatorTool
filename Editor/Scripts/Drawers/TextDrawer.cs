// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Core;
using Plugins.Templator.Editor.Scripts.Drawers.Base;
using Plugins.Templator.Editor.Scripts.Drawers.Selectors;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers
{
    public class TextDrawer : BaseDrawer, IPropertiesText
    {
        public override string Type => "Text (TMP)";

        public override float InspectorHeight => 320.0f;

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int IndexAnchor { get; private set; }

        public int IndexPivot { get; private set; }

        public string Text { get; private set; }

        public Color Color { get; private set; }

#endregion

        public TextDrawer(Rect rect, DTemplatorCore core) : base(rect, core)
        {
            Name = "TxtName";
            Width = core.DefaultValues.Text.Width;
            Height = core.DefaultValues.Text.Height;
            Text = "New Text";
            Color = Color.white;
            nodeBackgroundColor = core.DefaultValues.Text.NodeColor;
        }

        public TextDrawer(Rect rect, DTemplatorCore core, IPropertiesText drawer)
            : base(rect, core)
        {
            Name = drawer.Name;
            Width = drawer.Width;
            Height = drawer.Height;
            IndexAnchor = drawer.IndexAnchor;
            IndexPivot = drawer.IndexPivot;
            Text = drawer.Text;
            Color = drawer.Color;
            nodeBackgroundColor = core.DefaultValues.Text.NodeColor;
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
            Name = GUILayout.TextField(Name, MAX_TEXT_FIELD_LENGTH, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            GUISpaceBig();

            Width = Mathf.Clamp(EditorGUILayout.IntField("Width", Width), 2, MAX_NUMBER_VALUE);
            Height = Mathf.Clamp(EditorGUILayout.IntField("Height", Height), 2, MAX_NUMBER_VALUE);

            GUISeparator();

            GUILayout.BeginHorizontal();
            {
                IndexAnchor = AnchorsSelectorDrawer.Draw();
                IndexPivot = PivotSelectorDrawer.Draw();
            }
            GUILayout.EndHorizontal();

            GUISeparator();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Text");
                Text = GUILayout.TextField(Text, MAX_TEXT_AREA_LENGTH, GUILayout.Width(200));
            }
            GUILayout.EndHorizontal();

            GUISpaceSmall();

            Color = EditorGUILayout.ColorField("Color", Color);
        }
    }
}