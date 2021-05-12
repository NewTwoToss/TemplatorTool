// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.Creators;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Nodes
{
    public class ImageNode : BaseNodeComponent
    {
        private readonly ImageDrawer _drawer;
        private readonly ImageCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public ImageNode(Rect rect, DTossCreator data) : base(data)
        {
            _drawer = new ImageDrawer(rect, data);
            _creator = new ImageCreator();
        }

        /*public override void CheckPositionYAndShiftUp(float shiftLimitY, int countDeleteNodes)
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
        }*/

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