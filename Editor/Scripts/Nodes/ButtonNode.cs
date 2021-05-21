// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using System;
using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Core;
using Plugins.Templator.Editor.Scripts.Creators;
using Plugins.Templator.Editor.Scripts.Drawers;
using Plugins.Templator.Editor.Scripts.Drawers.Base;
using Plugins.Templator.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Nodes
{
    [Serializable]
    public class ButtonNode : BaseNodeComponent
    {
        private ButtonDrawer _drawer;
        private readonly ButtonCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public ButtonNode(Rect rect, TemplatorCore data) : base(data)
        {
            _drawer = new ButtonDrawer(rect, data);
            _creator = new ButtonCreator();
        }

        private ButtonNode(Rect rect, TemplatorCore data, IPropertiesButton drawer) 
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

        public override void MyClone(BaseNodeComponent cloneParent)
        {
            var cloneNode = new ButtonNode(GetCloneRect(), data, _drawer);

            cloneParent.nodes.Add(cloneNode);

            foreach (var node in nodes)
            {
                node.MyClone(cloneNode);
            }
            
            foreach (var decorator in decorators)
            {
                decorator.MyClone(cloneNode);
            }
        }
    }
}