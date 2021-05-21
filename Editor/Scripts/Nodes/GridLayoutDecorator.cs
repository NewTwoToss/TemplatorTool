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
    public class GridLayoutDecorator : BaseNodeComponent
    {
        private readonly GridLayoutDrawer _drawer;
        private readonly GridLayoutCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public GridLayoutDecorator(Rect rect, DTemplatorCore core) : base(core)
        {
            _drawer = new GridLayoutDrawer(rect, core);
            _creator = new GridLayoutCreator();
        }

        private GridLayoutDecorator(Rect rect,
            DTemplatorCore core,
            IPropertiesGridLayout drawer) : base(core)
        {
            _drawer = new GridLayoutDrawer(rect, core, drawer);
            _creator = new GridLayoutCreator();
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
            var cloneNode = new GridLayoutDecorator(GetCloneRect(), core, _drawer);

            cloneParent.decorators.Add(cloneNode);
        }
    }
}