// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 06.05.2021
// =================================================================================================

using Plugins.GameUIBuilder.Editor.Scripts.Creators;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers;
using Plugins.GameUIBuilder.Editor.Scripts.Drawers.Base;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes.Base;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Nodes
{
    public class VerticalLayoutDecorator : BaseNodeComponent
    {
        private readonly VerticalLayoutDrawer _drawer;
        private readonly VerticalLayoutCreator _creator;
        
        public override BaseDrawer Drawer => _drawer;

        public VerticalLayoutDecorator(Rect rect, DTossCreator data) : base(data)
        {
            _drawer = new VerticalLayoutDrawer(rect, data);
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
    }
}