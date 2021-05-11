// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 27.04.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.Nodes;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using Plugins.GameUIBuilder.Editor.Scripts.Views.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Views
{
    public class ToolView : BaseView
    {
        private const int MAX_LEVEL_CREATE_COMPONENTS = 5;

        private bool _initialized;
        private Texture _backgroundTexture;
        private Vector2 _scrollPosition;

        public override void DrawGUI(Rect pRect)
        {
            if (!_initialized)
            {
                _backgroundTexture = Resources.Load<Texture>("Textures/graph_background");
                _scrollPosition = Vector2.zero;
                _initialized = true;
            }

            DrawBackground(pRect);

            var viewRectY = Data.SourceNode.GetLastChildRectY() + 200;

            _scrollPosition = GUI.BeginScrollView(new Rect(0, 0, pRect.width, pRect.height),
                _scrollPosition,
                new Rect(0, 0, pRect.width - 20, viewRectY));

            Data.SourceNode.DrawGUI();
            DrawOutline();

            GUI.EndScrollView();
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

            //Debug.Log("Left Click");

            Data.IsSelection = false;
            Data.IsRepaint = false;

            var nodeContain = Data
                .SourceNode
                .SelectionControl(pEvent.mousePosition, _scrollPosition);

            if (!nodeContain) return;

            Data.IsSelection = true;
            Data.IsRepaint = true;
        }

        private void ProcessRightClick(Event pEvent, Rect pRect)
        {
            if (pEvent.button != 1 || pEvent.type != EventType.MouseDown) return;

            //Debug.Log("Right Click");

            var nodeContain = Data
                .SourceNode
                .Contains(pEvent.mousePosition, _scrollPosition);

            if (!nodeContain) return;

            ContextMenu();
        }

        private void ContextMenu()
        {
            var menu = new GenericMenu();

            if (Data.CurrentNode.CanBeDeleted())
            {
                menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode);
                menu.AddSeparator(string.Empty);
            }

            if (!Data.CurrentNode.IsDecorator())
            {
                if (Data.CurrentNode.Level < MAX_LEVEL_CREATE_COMPONENTS)
                {
                    menu.AddItem(new GUIContent("Add RectTransform"),
                        false, AddRectTransform);
                    menu.AddItem(new GUIContent("Add Image"),
                        false, AddImage);
                    menu.AddItem(new GUIContent("Add Button"),
                        false, AddButton);
                    menu.AddItem(new GUIContent("Add Text"),
                        false, AddButton);
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent("Add RectTransform"));
                    menu.AddDisabledItem(new GUIContent("Add Image"));
                    menu.AddDisabledItem(new GUIContent("Add Button"));
                    menu.AddDisabledItem(new GUIContent("Add Text"));
                }

                menu.AddSeparator(string.Empty);

                menu.AddItem(new GUIContent("Add Grid Layout"),
                    false, AddVerticalLayout);
                menu.AddItem(new GUIContent("Add Vertical Layout"),
                    false, AddVerticalLayout);
                menu.AddItem(new GUIContent("Add Horizontal Layout"),
                    false, AddHorizontalLayout);
            }

            menu.ShowAsContext();
        }

        private void DeleteNode()
        {
            var indexDelete = Data.CurrentNode.Index;
            Data.SourceNode.Delete(indexDelete);
        }

        private void AddRectTransform()
        {
            var newNodeRect = Data.CurrentNode.GetRectForNewNode();
            var newNode = new RectTransformNode(newNodeRect, Data);
            AddNewNode(newNode, newNodeRect);
        }

        private void AddImage()
        {
            var newNodeRect = Data.CurrentNode.GetRectForNewNode();
            var newNode = new ImageNode(newNodeRect, Data);
            AddNewNode(newNode, newNodeRect);
        }

        private void AddButton()
        {
            var newNodeRect = Data.CurrentNode.GetRectForNewNode();
            var newNode = new ButtonNode(newNodeRect, Data);
            AddNewNode(newNode, newNodeRect);
        }

        private void AddText()
        {
        }

        private void AddNewNode(BaseNodeComponent newNode, Rect newNodeRect)
        {
            var limitShiftY = newNodeRect.y - 2;
            Data.SourceNode.CheckPositionYAndShiftDown(limitShiftY);
            Data.CurrentNode.AddNode(newNode);
            Data.IsRepaint = true;
        }

        private void AddVerticalLayout()
        {
            var newDecoratorRect = Data.CurrentNode.GetRectForNewDecorator();
            var newDecorator = new VerticalLayoutDecorator(newDecoratorRect, Data);

            AddNewDecorator(newDecorator);
        }

        private void AddHorizontalLayout()
        {
            Debug.Log("[TOOL] Click");
        }

        private void AddNewDecorator(BaseNodeComponent newDecorator)
        {
            Data.CurrentNode.AddDecorator(newDecorator);
            Data.IsRepaint = true;
        }
    }
}