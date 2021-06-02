// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
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
    public class ImageDrawer : BaseDrawer, IPropertiesImage
    {
        public override string Type => "Image";

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
            Color = Color.white;
            nodeBackgroundColor = core.DefaultValues.Image.NodeColor;
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
            Name = GUILayout.TextField(Name, 25);
            GUILayout.EndHorizontal();

            GUISpaceBig();

            Width = Mathf.Clamp(EditorGUILayout.IntField("Width", Width), 2, MAX_NUMBER_VALUE);
            Height = Mathf.Clamp(EditorGUILayout.IntField("Height", Height), 2, MAX_NUMBER_VALUE);

            GUISeparator();
            
            GUILayout.BeginHorizontal();
            IndexAnchor = AnchorsSelectorDrawer.Draw();
            IndexPivot = PivotSelectorDrawer.Draw();
            GUILayout.EndHorizontal();

            GUISeparator();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Source Image");
            SourceImage = (Sprite) EditorGUILayout.ObjectField(SourceImage, typeof(Sprite), true);
            GUILayout.EndHorizontal();
            
            Color = EditorGUILayout.ColorField("Color", Color);

            GUISpaceSmall();

            RaycastTarget = EditorGUILayout.Toggle("Raycast Target", RaycastTarget);
            Maskable = EditorGUILayout.Toggle("Maskable", Maskable);
            SetNativeSize = EditorGUILayout.Toggle("Set Native Size", SetNativeSize);
        }
    }
}