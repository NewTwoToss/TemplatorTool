// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Creators;
using Plugins.GameUIBuilder.Editor.Drawers;
using Plugins.GameUIBuilder.Editor.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Nodes
{
    public class ImageNode : BaseNodeComponent
    {
        private readonly ImageDrawer _drawer;
        private readonly ImageCreator _creator;

        public override BaseDrawer Drawer => _drawer;

        public ImageNode(Rect rect, DTestScriptable data) : base(data)
        {
            _drawer = new ImageDrawer(rect, data);
            _creator = new ImageCreator();
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