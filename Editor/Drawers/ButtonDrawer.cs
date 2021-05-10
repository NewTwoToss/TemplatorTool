// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.ComponentProperties;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Drawers
{
    public class ButtonDrawer : BaseDrawer, IPropertiesButton
    {
        public override string Type => "Button";

#region [INSPECTOR]

        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

#endregion

        public ButtonDrawer(Rect rect, DTestScriptable data) : base(rect, data)
        {
            Name = "BtnName";
            Width = 100;
            Height = 40;
            nodeBackgroundColor = new Color(0.7f, 1.0f, 1.0f);
        }

        public override void DrawNode()
        {
            GUI.color = nodeBackgroundColor;
            GUI.Box(rect, string.Empty, data._skin.GetStyle("NodeBodyBg"));
            GUI.color = Color.white;

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_Button Icon")).image;
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
            GUILayout.Label("Name:");
            Name = GUILayout.TextField(Name, 25);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            Width = Mathf.Clamp(EditorGUILayout.IntField("Width:", Width), 2, 10_000);

            GUILayout.Space(4);
            
            Height = Mathf.Clamp(EditorGUILayout.IntField("Height:", Height), 2, 10_000);
        }
    }
}