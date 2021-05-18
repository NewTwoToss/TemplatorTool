// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 30.04.2021
// =================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base
{
    [Serializable]
    public abstract class BaseNodeComponent
    {
        protected readonly DTossCreator data;

        protected readonly List<BaseNodeComponent> decorators;

        //protected readonly List<BaseNodeComponent> nodes;
        public List<BaseNodeComponent> nodes;

        public int Index { get; set; }

        public int Level { get; set; }

        public abstract BaseDrawer Drawer { get; }

        protected BaseNodeComponent(DTossCreator data)
        {
            this.data = data;
            Index = data.NodeIndex;
            decorators = new List<BaseNodeComponent>();
            nodes = new List<BaseNodeComponent>();
        }

        public void AddNode(BaseNodeComponent node)
        {
            node.Level = Level + 1;
            nodes.Add(node);
        }

        public void AddDecorator(BaseNodeComponent decorator)
        {
            decorator.Level = Level + 1;
            decorators.Add(decorator);
        }

        public void Draw()
        {
            Drawer.DrawNode();

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                {
                    decorator.Draw();
                }
            }

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
            {
                node.Draw();
            }
        }

        public bool SelectionControl(Vector2 mousePosition, Vector2 offset)
        {
            if (Drawer.Rect.Contains(mousePosition + offset))
            {
                data.SelectedNode = this;
                return true;
            }

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                {
                    if (decorator.SelectionControl(mousePosition, offset))
                    {
                        data.SelectedNode = decorator;
                        return true;
                    }
                }
            }

            return nodes.Count != 0
                   && nodes.Any(node => node.SelectionControl(mousePosition, offset));
        }

        public bool Contains(Vector2 mousePosition, Vector2 offset)
        {
            if (Drawer.Rect.Contains(mousePosition + offset))
            {
                data.CurrentNode = this;
                return true;
            }

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                {
                    if (decorator.Contains(mousePosition, offset))
                    {
                        data.CurrentNode = decorator;
                        return true;
                    }
                }
            }

            return nodes.Count != 0 && nodes.Any(node => node.Contains(mousePosition, offset));
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
            var nodeShiftHorizontal = data.DrawValues.NodeShiftHorizontal;
            var nodeShiftVertical = data.DrawValues.NodeShiftVertical;

            if (nodes.Count == 0)
            {
                var vector = new Vector2(baseRect.x + nodeShiftHorizontal,
                    baseRect.y + nodeShiftVertical);
                var rect1 = new Rect(vector, baseRect.size);
                return rect1;
            }

            var lastNode = nodes[nodes.Count - 1];
            var lastNodeRect = lastNode.Drawer.Rect;
            var test = GetLastChildRectY() + nodeShiftVertical;
            var rect2 = new Rect(new Vector2(lastNodeRect.x, test), baseRect.size);

            return rect2;
        }

        public Rect GetRectForNewDecorator()
        {
            var baseRect = Drawer.Rect;
            var decoratorShiftHorizontal = data.DrawValues.DecoratorShiftHorizontal;

            if (decorators.Count == 0)
            {
                var position = new Vector2(
                    baseRect.x + baseRect.width + decoratorShiftHorizontal,
                    baseRect.y);
                var rectNewDecorator = new Rect(position, data.DrawValues.DecoratorSize);
                return rectNewDecorator;
            }

            return Drawer.Rect;
        }

        public float GetLastChildRectY()
        {
            if (nodes.Count == 0) return Drawer.Rect.y;

            var lastNode = nodes[nodes.Count - 1];

            return lastNode.GetLastChildRectY();
        }

        public virtual bool CanBeDeleted()
        {
            return true;
        }

        public virtual bool IsDecorator()
        {
            return false;
        }

        public virtual void Delete(int indexDelete)
        {
            foreach (var node in nodes)
            {
                if (node.Index == indexDelete)
                {
                    var countDeleteNodes = node.GetCountNodes();
                    var shiftLimitY = node.Drawer.Rect.y;

                    data.UndoRedo.RegisterSnapshot();
                    nodes.Remove(node);

                    data.SourceNode.CheckPositionYAndShiftUp(shiftLimitY, countDeleteNodes);
                    return;
                }

                node.Delete(indexDelete);
            }

            foreach (var decorator in decorators)
            {
                if (decorator.Index == indexDelete)
                {
                    decorators.Remove(decorator);
                    return;
                }

                decorator.Delete(indexDelete);
            }
        }

        public int GetCountNodes()
        {
            if (nodes.Count == 0) return 1;

            return 1 + nodes.Sum(node => node.GetCountNodes());
        }

        public void CheckPositionYAndShiftUp(float shiftLimitY, int countDeleteNodes)
        {
            if (Drawer.Rect.y > shiftLimitY)
            {
                ShiftUp(countDeleteNodes);
            }

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                    decorator.CheckPositionYAndShiftUp(shiftLimitY, countDeleteNodes);
            }

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
                node.CheckPositionYAndShiftUp(shiftLimitY, countDeleteNodes);
        }

        public void ShiftUpNodeWithChildren(int countNodes)
        {
            ShiftUp(countNodes);

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                    decorator.ShiftUpNodeWithChildren(countNodes);
            }

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
                node.ShiftUpNodeWithChildren(countNodes);
        }

        private void ShiftUp(int countNodes) => Drawer.ShiftUp(countNodes);

        public void CheckPositionYAndShiftDown(float shiftLimitY)
        {
            if (Drawer.Rect.y > shiftLimitY)
            {
                ShiftDown();
            }

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                    decorator.CheckPositionYAndShiftDown(shiftLimitY);
            }

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
                node.CheckPositionYAndShiftDown(shiftLimitY);
        }

        public void ShiftDownNodeWithChildren(int countNodes)
        {
            ShiftDown(countNodes);

            if (decorators.Count != 0)
            {
                foreach (var decorator in decorators)
                    decorator.ShiftDownNodeWithChildren(countNodes);
            }

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
                node.ShiftDownNodeWithChildren(countNodes);
        }

        private void ShiftDown(int countNodes = 1) => Drawer.ShiftDown(countNodes);

        public virtual void MyClone(BaseNodeComponent cloneParent)
        {
        }

        protected Rect GetCloneRect()
        {
            var currentRect = Drawer.Rect;
            var cloneRectPosition = new Vector2(currentRect.x, currentRect.y);
            var cloneRectSize = new Vector2(currentRect.width, currentRect.height);
            return new Rect(cloneRectPosition, cloneRectSize);
        }

        public void MoveNode(int indexCurrentNode, bool isMoveUp)
        {
            foreach (var node in nodes)
            {
                if (node.Index != indexCurrentNode) continue;

                var indexNodeInList = nodes.IndexOf(node);

                SwapNodes(nodes, indexNodeInList, isMoveUp);
                return;
            }

            foreach (var node in nodes)
            {
                node.MoveNode(indexCurrentNode, isMoveUp);
            }
        }

        public bool IsFirst(int indexCurrentNode)
        {
            foreach (var node in nodes)
            {
                if (node.Index != indexCurrentNode) continue;

                var indexNodeInList = nodes.IndexOf(node);

                if (indexNodeInList == 0) return true;
            }

            foreach (var node in nodes)
            {
                if (node.IsFirst(indexCurrentNode)) return true;
            }

            return false;
        }

        public bool IsLast(int indexCurrentNode)
        {
            foreach (var node in nodes)
            {
                if (node.Index != indexCurrentNode) continue;

                var indexNodeInList = nodes.IndexOf(node);
                var countNodesInList = nodes.Count;

                if (indexNodeInList == countNodesInList - 1) return true;
            }

            foreach (var node in nodes)
            {
                if (node.IsLast(indexCurrentNode)) return true;
            }

            return false;
        }

        private void SwapNodes(IList<BaseNodeComponent> pNodes, int index, bool isMoveUp)
        {
            var swapNodeIndex = index - 1;
            if (!isMoveUp)
            {
                swapNodeIndex = index + 1;
            }

            var selectedNode = pNodes[index];
            var swapNode = pNodes[swapNodeIndex];
            var selectedNodeCountNodes = selectedNode.GetCountNodes();
            var swapNodeCountNodes = swapNode.GetCountNodes();

            if (isMoveUp)
            {
                selectedNode.ShiftUpNodeWithChildren(swapNodeCountNodes);
                swapNode.ShiftDownNodeWithChildren(selectedNodeCountNodes);
            }
            else
            {
                selectedNode.ShiftDownNodeWithChildren(swapNodeCountNodes);
                swapNode.ShiftUpNodeWithChildren(selectedNodeCountNodes);
            }

            pNodes[index] = swapNode;
            pNodes[swapNodeIndex] = selectedNode;
        }
    }
}