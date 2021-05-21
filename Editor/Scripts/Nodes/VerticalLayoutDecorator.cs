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

        public VerticalLayoutDecorator(Rect rect, TemplatorCore data) : base(data)
        {
            _drawer = new VerticalLayoutDrawer(rect, data);
            _creator = new VerticalLayoutCreator();
        }

        private VerticalLayoutDecorator(Rect rect,
            TemplatorCore data,
            IPropertiesVerticalLayout drawer) : base(data)
        {
            _drawer = new VerticalLayoutDrawer(rect, data, drawer);
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
            var cloneNode = new VerticalLayoutDecorator(GetCloneRect(), data, _drawer);

            cloneParent.decorators.Add(cloneNode);
        }
    }
}