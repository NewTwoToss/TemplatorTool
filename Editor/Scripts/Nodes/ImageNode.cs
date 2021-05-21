// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
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
    public class ImageNode : BaseNodeComponent
    {
        private ImageDrawer _drawer;
        private readonly ImageCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public ImageNode(Rect rect, DTemplatorCore core) : base(core)
        {
            _drawer = new ImageDrawer(rect, core);
            _creator = new ImageCreator();
        }
        
        private ImageNode(Rect rect, DTemplatorCore core, IPropertiesImage drawer) 
            : base(core)
        {
            _drawer = new ImageDrawer(rect, core, drawer);
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
            var cloneNode = new ImageNode(GetCloneRect(), core, _drawer);

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