// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
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
    public class TextNode : BaseNodeComponent
    {
        private TextDrawer _drawer;
        private readonly TextCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public TextNode(Rect rect, TemplatorCore data) : base(data)
        {
            _drawer = new TextDrawer(rect, data);
            _creator = new TextCreator();
        }
        
        private TextNode(Rect rect, TemplatorCore data, IPropertiesText drawer) 
            : base(data)
        {
            _drawer = new TextDrawer(rect, data, drawer);
            _creator = new TextCreator();
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
            var cloneNode = new TextNode(GetCloneRect(), data, _drawer);

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