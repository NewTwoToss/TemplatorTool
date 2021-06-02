// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 27.04.2021
// =================================================================================================

using System.Collections.Generic;
using Plugins.Templator.Editor.Scripts.Nodes;
using Plugins.Templator.Editor.Scripts.Nodes.Base;
using Plugins.Templator.Editor.Scripts.Views.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Views
{
    public class MainView : BaseView
    {
#region [CONSTANS]

        private const int MAX_LEVEL_CREATE_NODES = 5;
        private const int MAX_NODE_CHILDREN_COUNT = 12;

        private const string TEXT_MOVE_UP = "Move Up";
        private const string TEXT_MOVE_DOWN = "Move Down";
        private const string TEXT_ADD_RT = "Add RectTransform";
        private const string TEXT_ADD_IMG = "Add Image";
        private const string TEXT_ADD_BTN = "Add Button";
        private const string TEXT_ADD_TXT = "Add Text";
        private const string TEXT_ADD_VER_LAYOUT = "Add Vertical Layout";
        private const string TEXT_ADD_HOR_LAYOUT = "Add Horizontal Layout";
        private const string TEXT_ADD_GRID_LAYOUT = "Add Grid Layout";

#endregion

#region [PRIVATE]

        private bool _initialized;
        private Texture _backgroundTexture;
        private Color _separatorColor;
        private Vector2 _scrollPosition;

#endregion

        public override void DrawGUI(Rect pRect)
        {
            if (!_initialized)
            {
                _backgroundTexture = Resources.Load<Texture>("Textures/graph_background");
                _separatorColor = new Color(0.8f, 0.6f, 0.0f, 0.4f);
                _scrollPosition = Vector2.zero;
                _initialized = true;
            }

            DrawBackground(pRect);

            var sourceNode = Core.SourceNode;
            var viewRectY = sourceNode.GetLastChildRectY() + 200;

            _scrollPosition = GUI.BeginScrollView(new Rect(0, 22, pRect.width, pRect.height - 22),
                _scrollPosition,
                new Rect(0, 22, pRect.width - 20, viewRectY));

            ShortcutsHandler();
            DrawSeparators(sourceNode.Nodes, pRect);
            sourceNode.Draw();
            DrawOutline();

            GUI.EndScrollView();
        }

        private void ShortcutsHandler()
        {
            if (!Event.current.control || Event.current.type != EventType.KeyDown)
                return;

            switch (Event.current.keyCode)
            {
                case KeyCode.Keypad0:
                    AddRectTransformViaShortcut();
                    break;
                case KeyCode.Keypad1:
                    AddImageViaShortcut();
                    break;
                case KeyCode.Keypad2:
                    AddTextViaShortcut();
                    break;
                case KeyCode.Keypad3:
                    AddButtonViaShortcut();
                    break;
            }
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

        private void DrawSeparators(IReadOnlyList<BaseNodeComponent> nodes, Rect pRect)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                if (i % 2 != 0) continue;

                var separatorRectX = nodes[i].Drawer.Rect.x - 20;
                var separatorRectY = nodes[i].Drawer.Rect.y - 8;
                var separatorRectWidth = pRect.width - 2 * separatorRectX;
                var separatorRectHeight = nodes[i].GetLastChildRectY() - separatorRectY;
                separatorRectHeight += Core.DrawValues.NodeSize.y + 4;
                var separatorRect = new Rect(separatorRectX,
                    separatorRectY,
                    separatorRectWidth,
                    separatorRectHeight);
                GUI.color = _separatorColor;
                GUI.Box(separatorRect, string.Empty, Core.Skin.GetStyle("GraphBox"));
                GUI.color = Color.white;
            }
        }

        private void DrawOutline()
        {
            if (!Core.IsSelection) return;

            var rect = Core.SelectedNode.Drawer.Rect;

            GUI.color = Color.green;
            GUI.Box(new Rect(rect.x - 2, rect.y - 2, rect.width + 4, rect.height + 4),
                string.Empty,
                Core.Skin.GetStyle("NodeSelected"));
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

            Core.IsSelection = false;
            Core.IsRepaint = false;

            var nodeContain = Core
                .SourceNode
                .SelectionControl(pEvent.mousePosition, _scrollPosition);

            if (!nodeContain) return;

            Core.IsSelection = true;
            Core.IsRepaint = true;
        }

        private void ProcessRightClick(Event pEvent)
        {
            if (pEvent.button != 1 || pEvent.type != EventType.MouseDown) return;

            var nodeContain = Core
                .SourceNode
                .Contains(pEvent.mousePosition, _scrollPosition);

            if (!nodeContain) return;

            ContextMenu();
        }

        private void ContextMenu()
        {
            var menu = new GenericMenu();
            var currentNode = Core.CurrentNode;
            var currentNodeIndex = currentNode.Index;
            var isDecorator = currentNode.IsDecorator();

            if (!isDecorator)
            {
                if (currentNode.Level != 0)
                {
                    if (Core.SourceNode.IsFirst(currentNodeIndex))
                    {
                        menu.AddDisabledItem(new GUIContent(TEXT_MOVE_UP));
                    }
                    else
                    {
                        menu.AddItem(new GUIContent(TEXT_MOVE_UP), false, MoveNodeUp);
                    }

                    if (Core.SourceNode.IsLast(currentNodeIndex))
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

                var isCreateLevel = currentNode.Level < MAX_LEVEL_CREATE_NODES;
                var isChildCount = currentNode.Nodes.Count < MAX_NODE_CHILDREN_COUNT;

                if (isCreateLevel && isChildCount)
                {
                    menu.AddItem(new GUIContent(TEXT_ADD_RT), false, AddRectTransformViaMenu);
                    menu.AddItem(new GUIContent(TEXT_ADD_IMG), false, AddImageViaMenu);
                    menu.AddItem(new GUIContent(TEXT_ADD_BTN), false, AddButtonViaMenu);
                    menu.AddItem(new GUIContent(TEXT_ADD_TXT), false, AddTextViaMenu);
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_RT));
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_IMG));
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_BTN));
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_TXT));
                }

                menu.AddSeparator(string.Empty);

                if (currentNode.Decorators.Count == 0)
                {
                    menu.AddItem(new GUIContent(TEXT_ADD_VER_LAYOUT), false, AddVerticalLayout);
                    menu.AddItem(new GUIContent(TEXT_ADD_HOR_LAYOUT), false, AddHorizontalLayout);
                    menu.AddItem(new GUIContent(TEXT_ADD_GRID_LAYOUT), false, AddGridLayout);
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_VER_LAYOUT));
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_HOR_LAYOUT));
                    menu.AddDisabledItem(new GUIContent(TEXT_ADD_GRID_LAYOUT));
                }
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
            var indexCurrentNode = Core.CurrentNode.Index;
            Core.SourceNode.MoveNode(indexCurrentNode, true);
            Core.IsRepaint = true;
        }

        private void MoveNodeDown()
        {
            var indexCurrentNode = Core.CurrentNode.Index;
            Core.SourceNode.MoveNode(indexCurrentNode, false);
        }

        private void AddRectTransformViaMenu()
        {
            var newNode = CreateRectTransformNode();
            AddNewNode(newNode);
        }

        private void AddRectTransformViaShortcut()
        {
            if (!Core.IsSelection) return;

            var newNode = CreateRectTransformNode();
            AddNewNode(newNode, true);
        }

        private void AddImageViaShortcut()
        {
            if (!Core.IsSelection) return;

            var newNode = CreateImageNode();
            AddNewNode(newNode, true);
        }

        private void AddTextViaShortcut()
        {
            if (!Core.IsSelection) return;

            var newNode = CreateTextNode();
            AddNewNode(newNode, true);
        }

        private void AddButtonViaShortcut()
        {
            if (!Core.IsSelection) return;

            var newNode = CreateButtonNode();
            AddNewNode(newNode, true);
        }

        private RectTransformNode CreateRectTransformNode()
        {
            var newNodeRect = Core.SelectedNode.GetRectForNewNode();
            return new RectTransformNode(newNodeRect, Core);
        }

        private ImageNode CreateImageNode()
        {
            var newNodeRect = Core.SelectedNode.GetRectForNewNode();
            return new ImageNode(newNodeRect, Core);
        }

        private TextNode CreateTextNode()
        {
            var newNodeRect = Core.SelectedNode.GetRectForNewNode();
            return new TextNode(newNodeRect, Core);
        }

        private ButtonNode CreateButtonNode()
        {
            var newNodeRect = Core.SelectedNode.GetRectForNewNode();
            return new ButtonNode(newNodeRect, Core);
        }

        private void AddImageViaMenu()
        {
            var newNode = CreateImageNode();
            AddNewNode(newNode);
        }

        private void AddButtonViaMenu()
        {
            var newNode = CreateButtonNode();
            AddNewNode(newNode);
        }

        private void AddTextViaMenu()
        {
            var newNode = CreateTextNode();
            AddNewNode(newNode);
        }

        private void AddNewNode(BaseNodeComponent newNode, bool isShortcut = false)
        {
            var limitShiftY = newNode.Drawer.Rect.y - 2;
            Core.UndoRedo.RegisterSnapshot();
            Core.SourceNode.CheckPositionYAndShiftDown(limitShiftY);

            if (isShortcut)
            {
                Core.SelectedNode.AddNode(newNode);
            }
            else
            {
                Core.CurrentNode.AddNode(newNode);
            }

            Core.IsRepaint = true;
        }

        private void AddVerticalLayout()
        {
            var newDecoratorRect = Core.CurrentNode.GetRectForNewDecorator();
            var newDecorator = new VerticalLayoutDecorator(newDecoratorRect, Core);
            AddNewDecorator(newDecorator);
        }

        private void AddHorizontalLayout()
        {
            var newDecoratorRect = Core.CurrentNode.GetRectForNewDecorator();
            var newDecorator = new HorizontalLayoutDecorator(newDecoratorRect, Core);
            AddNewDecorator(newDecorator);
        }

        private void AddGridLayout()
        {
            var newDecoratorRect = Core.CurrentNode.GetRectForNewDecorator();
            var newDecorator = new GridLayoutDecorator(newDecoratorRect, Core);
            AddNewDecorator(newDecorator);
        }

        private void AddNewDecorator(BaseNodeComponent newDecorator)
        {
            Core.UndoRedo.RegisterSnapshot();
            Core.CurrentNode.AddDecorator(newDecorator);
            Core.IsRepaint = true;
        }

        private void DeleteNode()
        {
            var indexDelete = Core.CurrentNode.Index;
            Core.SourceNode.Delete(indexDelete);

            if (!Core.IsSelection) return;

            if (Core.SelectedNode.Index == indexDelete)
            {
                Core.ResetSelection();
            }
        }

#endregion
    }
}