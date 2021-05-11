// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Creators;
using Plugins.GameUIBuilder.Editor.Drawers;
using Plugins.GameUIBuilder.Editor.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Nodes
{
    public class ButtonNode : BaseNodeComponent
    {
        private readonly ButtonDrawer _drawer;
        private readonly ButtonCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public ButtonNode(Rect rect, DTestScriptable data) : base(data)
        {
            _drawer = new ButtonDrawer(rect, data);
            _creator = new ButtonCreator();
        }

        public override void CheckPositionYAndShift(float shiftLimitY)
        {
            if (_drawer.Rect.y > shiftLimitY)
            {
                _drawer.ShiftY();
            }

            base.CheckPositionYAndShift(shiftLimitY);
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
    }
}