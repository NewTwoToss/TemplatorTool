// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
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
    public class HorizontalLayoutDecorator : BaseNodeComponent
    {
        private readonly HorizontalLayoutDrawer _drawer;
        private readonly HorizontalLayoutCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public HorizontalLayoutDecorator(Rect rect, DTemplatorCore core) : base(core)
        {
            _drawer = new HorizontalLayoutDrawer(rect, core);
            _creator = new HorizontalLayoutCreator();
        }

        private HorizontalLayoutDecorator(Rect rect,
            DTemplatorCore core,
            IPropertiesHorizontalLayout drawer)
            : base(core)
        {
            _drawer = new HorizontalLayoutDrawer(rect, core, drawer);
            _creator = new HorizontalLayoutCreator();
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
            var cloneNode = new HorizontalLayoutDecorator(GetCloneRect(), core, _drawer);

            cloneParent.decorators.Add(cloneNode);
        }
    }
}