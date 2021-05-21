// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 27.04.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.Nodes;
using Plugins.Templator.Editor.Scripts.Nodes.Base;
using Plugins.Templator.Editor.Scripts.Views.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Views
{
    public class ToolView : BaseView
    {
        private const int MAX_LEVEL_CREATE_COMPONENTS = 5;

        private const string TEXT_MOVE_UP = "Move Up";
        private const string TEXT_MOVE_DOWN = "Move Down";
        private const string TEXT_ADD_RT = "Add RectTransform";
        private const string TEXT_ADD_IMG = "Add Image";

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

            _scrollPosition = GUI.BeginScrollView(new Rect(0, 22, pRect.width, pRect.height - 22),
                _scrollPosition,
                new Rect(0, 22, pRect.width - 20, viewRectY));

            Data.SourceNode.Draw();
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
                Data.Skin.GetStyle("NodeSelected"));
            GUI.color = Color.white;
        }

        public override void ProcessEvent(Event pEvent, Rect pRect)
        {
            if (!pRect.Contains(pEvent.mousePosition)) return;

            ProcessLeftClick(pEvent);
            ProcessRightClick(pEvent);
        }

        private void ProcessLeftClick(Event pEvent)
        {
            if (pEvent.button != 0 || pEvent.type != EventType.MouseDown) return;

            Data.IsSelection = false;
            Data.IsRepaint = false;

            var nodeContain = Data
                .SourceNode
                .SelectionControl(pEvent.mousePosition, _scrollPosition);

            if (!nodeContain) return;

            Data.IsSelection = true;
            Data.IsRepaint = true;
        }

        private void ProcessRightClick(Event pEvent)
        {
            if (pEvent.button != 1 || pEvent.type != EventType.MouseDown) return;

            var nodeContain = Data
                .SourceNode
                .Contains(pEvent.mousePosition, _scrollPosition);

            if (!nodeContain) return;

            ContextMenu();
        }

        private void ContextMenu()
        {
            var menu = new GenericMenu();
            var currentNode = Data.CurrentNode;
            var currentNodeIndex = currentNode.Index;
            var isDecorator = currentNode.IsDecorator();

            if (!isDecorator)
            {
                if (currentNode.Level != 0)
                {
                    if (Data.SourceNode.IsFirst(currentNodeIndex))
                    {
                        menu.AddDisabledItem(new GUIContent(TEXT_MOVE_UP));
                    }
                    else
                    {
                        menu.AddItem(new GUIContent(TEXT_MOVE_UP), false, MoveNodeUp);
                    }

                    if (Data.SourceNode.IsLast(currentNodeIndex))
                    {
                        menu.AddDisabledItem(new GUIContent(TEXT_MOVE_DOWN));
                    }
                    else
                    {
                        menu.AddItem(new GUIContent(TEXT_MOVE_DOWN),
                            false, MoveNodeDown);
                    }

                    menu.AddSeparator(string.Empty);
                }

                if (currentNode.Level < MAX_LEVEL_CREATE_COMPONENTS)
                {
                    menu.AddItem(new GUIContent(TEXT_ADD_RT),
                        false, AddRectTransform);
                    menu.AddItem(new GUIContent(TEXT_ADD_IMG),
                        false, AddImage);
                    menu.AddItem(new GUIContent("Add Button"),
                        false, AddButton);
                    menu.AddItem(new GUIContent("Add Text"),
                        false, AddText);
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_RT));
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_IMG));
                    menu.AddDisabledItem(new GUIContent("Add Button"));
                    menu.AddDisabledItem(new GUIContent("Add Text"));
                }

                menu.AddSeparator(string.Empty);

                menu.AddItem(new GUIContent("Add Vertical Layout"),
                    false, AddVerticalLayout);
                menu.AddItem(new GUIContent("Add Horizontal Layout"),
                    false, AddHorizontalLayout);
                menu.AddItem(new GUIContent("Add Grid Layout"),
                    false, AddGridLayout);
            }

            if (currentNode.CanBeDeleted())
            {
                if (!isDecorator)
                {
                    menu.AddSeparator(string.Empty);
                }

                menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode);
            }

            menu.ShowAsContext();
        }

#region [CONTEXT MENU METHODS]

        private void MoveNodeUp()
        {
            var indexCurrentNode = Data.CurrentNode.Index;
            Data.SourceNode.MoveNode(indexCurrentNode, true);
            Data.IsRepaint = true;
        }

        private void MoveNodeDown()
        {
            var indexCurrentNode = Data.CurrentNode.Index;
            Data.SourceNode.MoveNode(indexCurrentNode, false);
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
            var newNodeRect = Data.CurrentNode.GetRectForNewNode();
            var newNode = new TextNode(newNodeRect, Data);
            AddNewNode(newNode, newNodeRect);
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
            var newDecoratorRect = Data.CurrentNode.GetRectForNewDecorator();
            var newDecorator = new HorizontalLayoutDecorator(newDecoratorRect, Data);
            AddNewDecorator(newDecorator);
        }

        private void AddGridLayout()
        {
            var newDecoratorRect = Data.CurrentNode.GetRectForNewDecorator();
            var newDecorator = new GridLayoutDecorator(newDecoratorRect, Data);
            AddNewDecorator(newDecorator);
        }

        private void AddNewDecorator(BaseNodeComponent newDecorator)
        {
            Data.CurrentNode.AddDecorator(newDecorator);
            Data.IsRepaint = true;
        }

        private void DeleteNode()
        {
            var indexDelete = Data.CurrentNode.Index;
            Data.SourceNode.Delete(indexDelete);

            if (!Data.IsSelection) return;

            if (Data.SelectedNode.Index == indexDelete)
            {
                Data.ResetSelection();
            }
        }

#endregion
    }
}