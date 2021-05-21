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
    public class RectTransformNode : BaseNodeComponent
    {
        private RectTransformDrawer _drawer;
        private readonly RectTransformCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public RectTransformNode(Rect rect, DTemplatorCore core) : base(core)
        {
            _drawer = new RectTransformDrawer(rect, core);
            _creator = new RectTransformCreator();
        }
        
        private RectTransformNode(Rect rect, DTemplatorCore core, IPropertiesRectTransform drawer) 
            : base(core)
        {
            _drawer = new RectTransformDrawer(rect, core, drawer);
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

        public override void MyClone(BaseNodeComponent cloneParent)
        {
            var cloneNode = new RectTransformNode(GetCloneRect(), core, _drawer);

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