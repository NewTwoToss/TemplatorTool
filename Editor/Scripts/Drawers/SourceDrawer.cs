// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 01.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Drawers
{
    public class SourceDrawer : BaseDrawer, IPropertiesSource
    {
        private readonly Color _errorColor;

#region [INSPECTOR]

        public RectTransform Source { get; private set; }

#endregion

        public override string Type => "Source (Main Parent)";

        public SourceDrawer(Rect rect, DTestScriptable data) : base(rect, data)
        {
            Source = null;
            nodeBackgroundColor = new Color(0.8f, 0.6f, 0.0f);
            _errorColor = new Color(1.0f, 0.16f, 0.16f);
        }

        public override void DrawNode(int index, int level)
        {
            DrawNodeBackground();

            var icon = new GUIContent(EditorGUIUtility.IconContent("d_PlayButton On")).image;
            DrawNodeTitle(icon, index, level);

            DrawBody("Ref.: NULL!");
        }

        protected override void DrawBody(string labelText)
        {
            var styleNodeText = data.Skin.GetStyle("NodeText");
            var labelPosition = new Vector2(rect.x + 10, rect.y + 20);
            var labelSize = new Vector2(180, 40);

            if (Source is null)
            {
                GUI.color = _errorColor;
                GUI.Label(new Rect(labelPosition, labelSize),
                    labelText,
                    styleNodeText);
            }
            else
            {
                GUI.Label(new Rect(labelPosition, labelSize),
                    $"Ref.: {Source.name}",
                    styleNodeText);
            }

            GUI.color = Color.white;
        }

        public override void DrawInspector()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Parent");
            Source = (RectTransform) EditorGUILayout
                .ObjectField(Source, typeof(RectTransform), true);
            GUILayout.EndHorizontal();
        }
    }
}