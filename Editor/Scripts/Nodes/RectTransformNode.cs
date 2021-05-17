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
        private readonly RectTransformDrawer _drawer;
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

        public override void MyCloneTwo(BaseNodeComponent cloneParent)
        {
            var oldRect = Drawer.Rect;
            var parent = new RectTransformNode(new Rect(oldRect.x, oldRect.y, 200, 60), data);
            
            cloneParent.nodes.Add(parent);

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
            {
                node.MyCloneTwo(parent);
            }
        }
    }
}