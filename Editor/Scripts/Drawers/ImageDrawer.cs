// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Core;
using Plugins.Templator.Editor.Scripts.Drawers.Base;
using Plugins.Templator.Editor.Scripts.Drawers.Selectors;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Drawers
{
    public class ImageDrawer : BaseDrawer, IPropertiesImage
    {
        public override string Type => "Image";

        public override float InspectorHeight => 400.0f;

        private readonly AnchorsSelectorDrawer _anchorsSelectorDrawer;
        private readonly PivotSelectorDrawer _pivotSelectorDrawer;

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int IndexAnchor { get; private set; }

        public int IndexPivot { get; private set; }

        public Sprite SourceImage { get; private set; }

        public Color Color { get; private set; }

        public bool RaycastTarget { get; private set; }

        public bool Maskable { get; private set; }

        public bool SetNativeSize { get; private set; }

#endregion

        public ImageDrawer(Rect rect, DTemplatorCore core) : base(rect, core)
        {
            Name = "ImgName";
            Width = core.DefaultValues.Image.Width;
            Height = core.DefaultValues.Image.Height;
            IndexAnchor = 4;
            IndexPivot = 4;
            Color = Color.white;
            RaycastTarget = false;
            Maskable = false;
            SetNativeSize = false;
            nodeBackgroundColor = core.DefaultValues.Image.NodeColor;
            _anchorsSelectorDrawer = new AnchorsSelectorDrawer(core);
            _pivotSelectorDrawer = new PivotSelectorDrawer(core);
        }

        public ImageDrawer(Rect rect, DTemplatorCore core, IPropertiesImage drawer)
            : base(rect, core)
        {
            Name = drawer.Name;
            Width = drawer.Width;
            Height = drawer.Height;
            IndexAnchor = drawer.IndexAnchor;
            IndexPivot = drawer.IndexPivot;
            SourceImage = drawer.SourceImage;
            Color = drawer.Color;
            RaycastTarget = drawer.RaycastTarget;
            Maskable = drawer.Maskable;
            SetNativeSize = drawer.SetNativeSize;
            nodeBackgroundColor = core.DefaultValues.Image.NodeColor;
            _anchorsSelectorDrawer = new AnchorsSelectorDrawer(core);
            _pivotSelectorDrawer = new PivotSelectorDrawer(core);
        }

        public override void DrawNode()
        {
            DrawNodeBackground();

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_Image Icon")).image;
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
                IndexAnchor = _anchorsSelectorDrawer.Draw(IndexAnchor);
                IndexPivot = _pivotSelectorDrawer.Draw(IndexPivot);
            }
            GUILayout.EndHorizontal();

            GUISeparator();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Source Image");
                SourceImage = (Sprite) EditorGUILayout
                    .ObjectField(SourceImage, typeof(Sprite), true);
            }
            GUILayout.EndHorizontal();

            GUISpaceSmall();

            Color = EditorGUILayout.ColorField("Color", Color);

            GUISpaceBig();

            RaycastTarget = EditorGUILayout.Toggle("Raycast Target", RaycastTarget);
            Maskable = EditorGUILayout.Toggle("Maskable", Maskable);
            SetNativeSize = EditorGUILayout.Toggle("Set Native Size", SetNativeSize);
        }
    }
}