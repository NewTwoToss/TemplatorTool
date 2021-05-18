// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using System;
using Plugins.GameUIBuilder.Editor.Scripts.Creators;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Nodes
{
    [Serializable]
    public class RectTransformNode : BaseNodeComponent
    {
        private RectTransformDrawer _drawer;
        private readonly RectTransformCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public RectTransformNode(Rect rect, DTossCreator data) : base(data)
        {
            _drawer = new RectTransformDrawer(rect, data);
            _creator = new RectTransformCreator();
        }

        public override void SetParent(RectTransform parent)
        {
            _creator.Parent = parent;
        }

        public override void Create()
        {
            _creator.Properties = _drawer;
            _creator.CreateUI();
            var getProduct = _creator.Product;

            CreateDecorators(getProduct);
            CreateGameUINodes(getProduct);
        }

        private void DrawerForClone(RectTransformDrawer drawer) => _drawer = drawer;

        public override void MyClone(BaseNodeComponent cloneParent)
        {
            var currentRect = Drawer.Rect;
            //var cloneRectPosition = new Vector2(currentRect.x, currentRect.y);
            //var cloneRectSize = new Vector2(currentRect.width, currentRect.height);
            //var cloneRect = new Rect(cloneRectPosition, cloneRectSize);
            //var cloneNode = new RectTransformNode(cloneRect, data);
            var cloneNode = new RectTransformNode(new Rect(currentRect.x, currentRect.y, 200, 60), data);
            cloneNode.DrawerForClone(_drawer);

            cloneParent.nodes.Add(cloneNode);

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
            {
                node.MyClone(cloneNode);
            }
        }
    }
}