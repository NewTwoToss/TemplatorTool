// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 27.04.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Nodes;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Views
{
    public class ToolView : BaseView
    {
        private bool _initialized;
        private Texture _backgroundTexture;

        public override void DrawGUI(Rect pRect)
        {
            if (!_initialized)
            {
                _backgroundTexture = Resources.Load<Texture>("Textures/graph_background");
                _initialized = true;
                Debug.Log("ToolView :: DrawGUI()");
            }

            DrawBackground(pRect);
            Data.SourceNode.DrawGUI();
            DrawOutline();
        }

        private void DrawBackground(Rect pRect)
        {
            var width = pRect.width;
            var height = pRect.height;
            var widthTexture = _backgroundTexture.width;
            var heightTexture = _backgroundTexture.height;
            var rectPosition = new Rect(0, 0, width, height);

            GUI.color = new Color(0.0f, 0.0f, 0.0f, 0.4f);
            GUI.DrawTextureWithTexCoords(rectPosition,
                _backgroundTexture,
                new Rect(0, 0, width / widthTexture, height / heightTexture),
                true);
            GUI.color = Color.white;
        }

        private void DrawOutline()
        {
            if (!Data.IsSelection) return;

            var rect = Data.SelectedNode.Drawer.Rect;

            GUI.color = Color.green;
            GUI.Box(new Rect(rect.x - 2, rect.y - 2, rect.width + 4, rect.height + 4),
                string.Empty,
                Data._skin.GetStyle("NodeSelected"));
            GUI.color = Color.white;
        }

        public override void ProcessEvent(Event pEvent, Rect pRect)
        {
            if (!pRect.Contains(pEvent.mousePosition)) return;

            ProcessLeftClick(pEvent, pRect);
            ProcessRightClick(pEvent, pRect);
        }

        private void ProcessLeftClick(Event pEvent, Rect pRect)
        {
            if (pEvent.button != 0 || pEvent.type != EventType.MouseDown) return;

            Debug.Log("Left Click");

            Data.IsSelection = false;
            Data.IsRepaint = false;

            var nodeContain = Data.SourceNode.SelectionControl(pEvent.mousePosition);

            if (!nodeContain) return;

            Data.IsSelection = true;
            Data.IsRepaint = true;
        }

        private void ProcessRightClick(Event pEvent, Rect pRect)
        {
            if (pEvent.button != 1 || pEvent.type != EventType.MouseDown) return;

            Debug.Log("Right Click");

            var nodeContain = Data.SourceNode.Contains(pEvent.mousePosition);

            if (!nodeContain) return;

            ContextMenu();
        }

        private void ContextMenu()
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode);
            menu.AddSeparator(string.Empty);
            menu.AddItem(new GUIContent("Add RectTransform"), false, AddRectTransform);
            menu.AddItem(new GUIContent("Add Image"), false, AddImage);
            menu.AddItem(new GUIContent("Add Button"), false, AddButton);
            menu.AddSeparator(string.Empty);
            /*menu.AddItem(new GUIContent("Decorators/Vertical Layout"),
                false,
                AddVerticalLayout);*/
            
            menu.AddItem(new GUIContent("Vertical Layout"), false, AddVerticalLayout);
            
            menu.AddItem(new GUIContent("Decorators/Horizontal Layout"),
                false,
                AddHorizontalLayout);
            menu.ShowAsContext();
        }

        private void DeleteNode()
        {
            Debug.Log("[TOOL] Click");
        }

        private void AddRectTransform()
        {
            var newNodeRect = Data.CurrentNode.GetRectForNewNode();
            var newNode = new RectTransformNode(newNodeRect, Data);

            var limitShiftY = newNodeRect.y - 2;
            Data.SourceNode.CheckPositionYAndShift(limitShiftY);
            Data.CurrentNode.Add(newNode);
            Data.IsRepaint = true;
        }

        private void AddImage()
        {
            var newNodeRect = Data.CurrentNode.GetRectForNewNode();
            var newNode = new ImageNode(newNodeRect, Data);

            var limitShiftY = newNodeRect.y - 2;
            Data.SourceNode.CheckPositionYAndShift(limitShiftY);
            Data.CurrentNode.Add(newNode);
            Data.IsRepaint = true;
        }

        private void AddButton()
        {
            var newNodeRect = Data.CurrentNode.GetRectForNewNode();
            var newNode = new ButtonNode(newNodeRect, Data);

            var limitShiftY = newNodeRect.y - 2;
            Data.SourceNode.CheckPositionYAndShift(limitShiftY);
            Data.CurrentNode.Add(newNode);
            Data.IsRepaint = true;
        }

        private void AddVerticalLayout()
        {
            var newNodeRect = Data.CurrentNode.GetRectForNewDecorator();
            var newNode = new VerticalLayoutDecorator(newNodeRect, Data);

            Data.CurrentNode.AddDecorator(newNode);

            Data.IsRepaint = true;
        }

        private void AddHorizontalLayout()
        {
            Debug.Log("[TOOL] Click");
        }
    }
}