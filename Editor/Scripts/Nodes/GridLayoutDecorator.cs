// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.Creators;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Nodes
{
    public class GridLayoutDecorator : BaseNodeComponent
    {
        private readonly GridLayoutDrawer _drawer;
        private readonly GridLayoutCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public GridLayoutDecorator(Rect rect, DTestScriptable data) : base(data)
        {
            _drawer = new GridLayoutDrawer(rect, data);
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
    }
}