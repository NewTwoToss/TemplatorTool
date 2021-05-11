// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 06.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Creators;
using Plugins.GameUIBuilder.Editor.Drawers;
using Plugins.GameUIBuilder.Editor.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Nodes
{
    public class VerticalLayoutDecorator : BaseNodeComponent
    {
        private readonly VerticalLayoutDrawer _drawer;
        private readonly VerticalLayoutCreator _creator;
        
        public override BaseDrawer Drawer => _drawer;

        public VerticalLayoutDecorator(Rect rect, DTestScriptable data) : base(data)
        {
            _drawer = new VerticalLayoutDrawer(rect, data);
            _creator = new VerticalLayoutCreator();
        }

        public override void CheckPositionYAndShiftDown(float shiftLimitY)
        {
            if (_drawer.Rect.y > shiftLimitY)
            {
                _drawer.ShiftY();
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
        }
        
        public override bool IsDecorator()
        {
            return true;
        }
    }
}