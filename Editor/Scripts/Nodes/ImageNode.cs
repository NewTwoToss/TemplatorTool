// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using System;
using Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties;
using Plugins.GameUIBuilder.Editor.Scripts.Creators;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Nodes
{
    [Serializable]
    public class ImageNode : BaseNodeComponent
    {
        private ImageDrawer _drawer;
        private readonly ImageCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public ImageNode(Rect rect, DTossCreator data) : base(data)
        {
            _drawer = new ImageDrawer(rect, data);
            _creator = new ImageCreator();
        }
        
        private ImageNode(Rect rect, DTossCreator data, IPropertiesImage drawer) 
            : base(data)
        {
            _drawer = new ImageDrawer(rect, data, drawer);
            _creator = new ImageCreator();
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

        public override void MyClone(BaseNodeComponent cloneParent)
        {
            var cloneNode = new ImageNode(GetCloneRect(), data, _drawer);

            cloneParent.nodes.Add(cloneNode);

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
            {
                node.MyClone(cloneNode);
            }
        }
    }
}