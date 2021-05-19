// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
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
    public class ButtonNode : BaseNodeComponent
    {
        private ButtonDrawer _drawer;
        private readonly ButtonCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public ButtonNode(Rect rect, DTossCreator data) : base(data)
        {
            _drawer = new ButtonDrawer(rect, data);
            _creator = new ButtonCreator();
        }

        private ButtonNode(Rect rect, DTossCreator data, IPropertiesButton drawer) 
            : base(data)
        {
            _drawer = new ButtonDrawer(rect, data, drawer);
            _creator = new ButtonCreator();
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

        private void DrawerForClone(ButtonDrawer drawer) => _drawer = drawer;

        public override void MyClone(BaseNodeComponent cloneParent)
        {
            var cloneNode = new ButtonNode(GetCloneRect(), data, _drawer);
            //cloneNode.DrawerForClone(_drawer);

            cloneParent.nodes.Add(cloneNode);

            if (nodes.Count == 0) return;

            foreach (var node in nodes)
            {
                node.MyClone(cloneNode);
            }
        }
    }
}