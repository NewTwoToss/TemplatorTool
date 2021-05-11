// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.ComponentProperties;
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

        public Color Color { get; private set; }

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

            /*var texture = GetSolidColorTexture(Color);

            GUI.DrawTexture(new Rect(rect.x + 2, rect.y + 20, 16, 16),
                texture);*/
        }

        private Texture2D GetSolidColorTexture(Color color)
        {
            var texture = new Texture2D(16, 16);
            for (var i = 0; i < 16; i++)
            {
                for (var j = 0; j < 16; j++)
                {
                    texture.SetPixel(i, j, Color);
                }
            }

            texture.Apply();

            return texture;
        }

        public override void DrawInspector()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            Name = GUILayout.TextField(Name, 25);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            Width = Mathf.Clamp(EditorGUILayout.IntField("Width:", Width), 2, MAX_NUMBER_VALUE);

            GUILayout.Space(4);

            Height = Mathf.Clamp(EditorGUILayout.IntField("Height:", Height), 2, MAX_NUMBER_VALUE);

            GUILayout.Space(4);

            Color = EditorGUILayout.ColorField("Color:", Color);
        }
    }
}