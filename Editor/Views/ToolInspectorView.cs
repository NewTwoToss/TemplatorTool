// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 01.05.2021
// =================================================================================================

using Dash;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Views
{
    public class ToolInspectorView : BaseView
    {
        //private Vector2 _scrollPosition;

        public override void DrawGUI(Rect pRect)
        {
            if (!Data.IsSelection) return;

            DrawNodeInspector(pRect);
        }

        private void DrawNodeInspector(Rect pRect)
        {
            if (!Data.IsSelection) return;

            var currentNode = Data.SelectedNode;
            var rect = new Rect(pRect.width - 300, 30, 280, 300);

            DrawBoxGUI(rect, "Properties", TextAnchor.UpperRight);

            GUI.Label(new Rect(rect.x + 5, rect.y, 100, 100),
                currentNode.Drawer.Type,
                DashEditorCore.Skin.GetStyle("NodePropertiesTitle"));

            GUILayout.BeginArea(
                new Rect(rect.x + 5, rect.y + 40, rect.width - 10, rect.height - 35));

            //_scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, false);

            currentNode.Drawer.DrawInspector();

            //GUILayout.EndScrollView();
            GUILayout.EndArea();

            UseEvent(rect);
        }

        private void UseEvent(Rect pRect)
        {
            if (pRect.Contains(Event.current.mousePosition) &&
                Event.current.type == EventType.MouseDown)
            {
                Event.current.type = EventType.Used;
            }
        }
    }
}