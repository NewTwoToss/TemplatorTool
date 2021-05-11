// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.Creators;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Nodes
{
    public class RectTransformNode : BaseNodeComponent
    {
        private readonly RectTransformDrawer _drawer;
        private readonly RectTransformCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public RectTransformNode(Rect rect, DTestScriptable data) : base(data)
        {
            _drawer = new RectTransformDrawer(rect, data);
            _creator = new RectTransformCreator();
        }
        
        public override void CheckPositionYAndShiftUp(float shiftLimitY, int countDeleteNodes)
        {
            if (_drawer.Rect.y > shiftLimitY)
            {
                _drawer.ShiftUp(countDeleteNodes);
            }

            base.CheckPositionYAndShiftUp(shiftLimitY, countDeleteNodes);
        }

        public override void CheckPositionYAndShiftDown(float shiftLimitY)
        {
            if (_drawer.Rect.y > shiftLimitY)
            {
                _drawer.ShiftDown();
            }

            base.CheckPositionYAndShiftDown(shiftLimitY);
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