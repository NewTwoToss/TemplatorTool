// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 06.05.2021
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
    public class VerticalLayoutDecorator : BaseNodeComponent
    {
        private readonly VerticalLayoutDrawer _drawer;
        private readonly VerticalLayoutCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public VerticalLayoutDecorator(Rect rect, DTemplatorCore core) : base(core)
        {
            _drawer = new VerticalLayoutDrawer(rect, core);
            _creator = new VerticalLayoutCreator();
        }

        private VerticalLayoutDecorator(Rect rect,
            DTemplatorCore core,
            IPropertiesVerticalLayout drawer) : base(core)
        {
            _drawer = new VerticalLayoutDrawer(rect, core, drawer);
            _creator = new VerticalLayoutCreator();
        }

        public override void SetParent(RectTransform parent)
        {
            _creator.Parent = parent;
        }

        public override void Create()
        {
            _creator.Properties = _drawer;
            _creator.CreateUI();
        }

        public override bool IsDecorator()
        {
            return true;
        }

        public override void MyClone(BaseNodeComponent cloneParent)
        {
            var cloneNode = new VerticalLayoutDecorator(GetCloneRect(), core, _drawer);

            cloneParent.Decorators.Add(cloneNode);
        }
    }
}