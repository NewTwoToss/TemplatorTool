// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using Plugins.Templator.Editor.Scripts.ComponentProperties;
using Plugins.Templator.Editor.Scripts.Core;
using Plugins.Templator.Editor.Scripts.Creators;
using Plugins.Templator.Editor.Scripts.Drawers;
using Plugins.Templator.Editor.Scripts.Drawers.Base;
using Plugins.Templator.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Nodes
{
    public class ButtonNode : BaseNodeComponent
    {
        private readonly ButtonDrawer _drawer;
        private readonly ButtonCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public ButtonNode(Rect rect, DTemplatorCore core) : base(core)
        {
            _drawer = new ButtonDrawer(rect, core);
            _creator = new ButtonCreator();
        }

        private ButtonNode(Rect rect, DTemplatorCore core, IPropertiesButton drawer) 
            : base(core)
        {
            _drawer = new ButtonDrawer(rect, core, drawer);
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
            var product = _creator.Product;

            CreateDecorators(product);
            CreateGameUINodes(product);
        }

        public override void MyClone(BaseNodeComponent cloneParent)
        {
            var cloneNode = new ButtonNode(GetCloneRect(), core, _drawer);

            cloneParent.Nodes.Add(cloneNode);

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