// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Drawers
{
    public class ImageDrawer : BaseDrawer, IPropertiesImage
    {
        public override string Type => "Image";

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Sprite SourceImage { get; private set; }

        public Color Color { get; private set; }

        public bool RaycastTarget { get; private set; }
        
        public bool Maskable { get; private set; }
        
        public bool SetNativeSize { get; private set; }

#endregion

        public ImageDrawer(Rect rect, DTossCreator data) : base(rect, data)
        {
            Name = "ImgName";
            Width = data.DefaultValues.Image.Width;
            Height = data.DefaultValues.Image.Height;
            Color = Color.white;
            nodeBackgroundColor = data.DefaultValues.Image.NodeColor;
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

            GUISpaceSmall();

            Height = Mathf.Clamp(EditorGUILayout.IntField("Height", Height), 2, MAX_NUMBER_VALUE);

            GUISpaceSmall();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Source Image");
            SourceImage = (Sprite) EditorGUILayout.ObjectField(SourceImage, typeof(Sprite), true);
            GUILayout.EndHorizontal();

            GUISpaceSmall();

            Color = EditorGUILayout.ColorField("Color", Color);

            GUISpaceSmall();

            RaycastTarget = EditorGUILayout.Toggle("Raycast Target", RaycastTarget);
            
            GUISpaceSmall();

            Maskable = EditorGUILayout.Toggle("Maskable", Maskable);
            
            GUISpaceSmall();

            SetNativeSize = EditorGUILayout.Toggle("Set Native Size", SetNativeSize);
        }
    }
}