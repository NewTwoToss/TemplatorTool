// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Drawers
{
    public class ImageDrawer : BaseDrawer, IPropertiesImage
    {
#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Sprite SourceImage { get; private set; }

        public Color Color { get; private set; }

        public bool SetNativeSize { get; private set; }
        
#endregion

        public override string Type => "Image";

        public ImageDrawer(Rect rect, DTestScriptable data) : base(rect, data)
        {
            Name = "ImgName";
            Width = 100;
            Height = 40;
            Color = Color.white;
            nodeBackgroundColor = new Color(0.8f, 0.5f, 0.5f);
        }

        public override void DrawNode()
        {
            DrawNodeBackground();

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_Image Icon")).image;
            DrawNodeTitle(icon);

            DrawBody();
        }

        private void DrawBody()
        {
            var labelText = $"{Name} [{Width}x{Height}]";
            var rectPosition = new Vector2(rect.x + 10, rect.y + 20);
            var rectSize = new Vector2(180, 40);
            GUI.Label(new Rect(rectPosition, rectSize),
                labelText,
                data._skin.GetStyle("NodeText"));
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

            SetNativeSize = EditorGUILayout.Toggle("Set Native Size", SetNativeSize);
        }
    }
}