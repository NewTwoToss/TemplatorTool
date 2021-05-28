// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 01.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.Views.Base;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Views
{
    public class InspectorView : BaseView
    {
        public override void DrawGUI(Rect pRect)
        {
            if (!Core.IsSelection) return;

            DrawNodeInspector(pRect);
        }

        private void DrawNodeInspector(Rect pRect)
        {
            if (!Core.IsSelection) return;

            var currentNode = Core.SelectedNode;
            var rect = new Rect(pRect.width - 300, 30, 280, 300);

            DrawBoxGUI(rect, "Properties", TextAnchor.UpperRight);

            GUI.Label(new Rect(rect.x + 5, rect.y, 100, 100),
                currentNode.Drawer.Type,
                Core.Skin.GetStyle("NodePropertiesTitle"));

            GUILayout.BeginArea(new Rect(rect.x + 5,
                rect.y + 40,
                rect.width - 10,
                rect.height - 35));

            currentNode.Drawer.DrawInspector();

            GUILayout.EndArea();

            UseEvent(rect);
        }

        private void UseEvent(Rect pRect)
        {
            var rectContains = pRect.Contains(Event.current.mousePosition);
            var isMouseDown = Event.current.type == EventType.MouseDown;

            if (rectContains && isMouseDown)
            {
                Event.current.type = EventType.Used;
            }
        }
    }
}