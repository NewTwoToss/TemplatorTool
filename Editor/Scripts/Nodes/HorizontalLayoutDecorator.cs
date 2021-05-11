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
    public class HorizontalLayoutDecorator : BaseNodeComponent
    {
        private readonly HorizontalLayoutDrawer _drawer;
        private readonly HorizontalLayoutCreator _creator;
        
        public override BaseDrawer Drawer => _drawer;

        public HorizontalLayoutDecorator(Rect rect, DTestScriptable data) : base(data)
        {
            _drawer = new HorizontalLayoutDrawer(rect, data);
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
    }
}