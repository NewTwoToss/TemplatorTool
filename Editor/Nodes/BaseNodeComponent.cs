// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 30.04.2021
// =================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.GameUIBuilder.Editor.Drawers;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Nodes
{
    [Serializable]
    public abstract class BaseNodeComponent
    {
        protected readonly DTestScriptable data;
        protected readonly List<BaseNodeComponent> decorators;
        protected readonly List<BaseNodeComponent> nodes;

        public abstract BaseDrawer Drawer { get; }

        public BaseNodeComponent AddNewNode
        {
            set => nodes.Add(value);
        }

        protected BaseNodeComponent(DTestScriptable data)
        {
            this.data = data;
            decorators = new List<BaseNodeComponent>();
            nodes = new List<BaseNodeComponent>();
        }

        public void Add(BaseNodeComponent node) => nodes.Add(node);

        public void AddDecorator(BaseNodeComponent decorator) => decorators.Add(decorator);

        public void DrawGUI()
        {
            Drawer.DrawNode();

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                {
                    decorator.DrawGUI();
                }
            }

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
            {
                node.DrawGUI();
            }
        }

        public bool SelectionControl(Vector2 mousePosition)
        {
            if (Drawer.Rect.Contains(mousePosition))
            {
                data.SelectedNode = this;
                return true;
            }

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                {
                    if (decorator.SelectionControl(mousePosition))
                    {
                        data.SelectedNode = decorator;
                        return true;
                    }
                }
            }

            return nodes.Count != 0 && nodes.Any(node => node.SelectionControl(mousePosition));
        }

        public bool Contains(Vector2 mousePosition)
        {
            if (Drawer.Rect.Contains(mousePosition))
            {
                data.CurrentNode = this;
                return true;
            }

            return nodes.Count != 0 && nodes.Any(node => node.Contains(mousePosition));
        }

        public virtual void SetParent(RectTransform parent)
        {
        }

        public abstract void Create();

        protected void CreateDecorators(RectTransform currentObject)
        {
            if (decorators.Count == 0) return;

            foreach (var decorator in decorators)
            {
                decorator.SetParent(currentObject);
                decorator.Create();
            }
        }

        protected void CreateGameUINodes(RectTransform parentObject)
        {
            if (nodes.Count == 0) return;

            foreach (var node in nodes)
            {
                node.SetParent(parentObject);
                node.Create();
            }
        }

        public Rect GetRectForNewNode()
        {
            var baseRect = Drawer.Rect;

            if (nodes.Count == 0)
            {
                var vector = new Vector2(baseRect.x + data._nodeShiftHorizontal,
                    baseRect.y + data._nodeShiftVertical);
                var rect1 = new Rect(vector, baseRect.size);
                return rect1;
            }

            var lastNode = nodes[nodes.Count - 1];
            var lastNodeRect = lastNode.Drawer.Rect;
            var test = GetLastChildRectY() + data._nodeShiftVertical;
            var rect2 = new Rect(new Vector2(lastNodeRect.x, test), baseRect.size);

            return rect2;
        }

        public Rect GetRectForNewDecorator()
        {
            var baseRect = Drawer.Rect;

            if (decorators.Count == 0)
            {
                var vector = new Vector2(baseRect.x + baseRect.width + data._decoratorShiftHorizontal,
                    baseRect.y);
                var rect1 = new Rect(vector, new Vector2(baseRect.width - 40, baseRect.height - 10));
                return rect1;
            }

            return Drawer.Rect;
        }

        public virtual void CheckPositionYAndShift(float shiftLimitY)
        {
            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                    decorator.CheckPositionYAndShift(shiftLimitY);
            }

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
                node.CheckPositionYAndShift(shiftLimitY);
        }

        public float GetLastChildRectY()
        {
            if (nodes.Count == 0) return Drawer.Rect.y;

            var lastNode = nodes[nodes.Count - 1];

            return lastNode.GetLastChildRectY();
        }
    }
}