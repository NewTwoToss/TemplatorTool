// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 12.05.2021
// =================================================================================================

using System;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Core
{
    [Serializable]
    public class DrawValues
    {
#region [INSPECTOR]

        [SerializeField]
        private Vector2Int _sourceNodePosition;
        
        [SerializeField]
        private Vector2Int _nodeSize;
        
        [SerializeField]
        private Vector2Int _decoratorSize;
        
        [Space, SerializeField, Range(40, 100)]
        public int _nodeShiftVertical = 80;

        [SerializeField, Range(40, 100)]
        public int _nodeShiftHorizontal = 40;

        [SerializeField, Range(10, 100)]
        public int _decoratorShiftHorizontal = 20;

#endregion

#region [INSPECTOR GETTERS]

        public Vector2Int SourceNodePosition => _sourceNodePosition;
        
        public Vector2Int NodeSize => _nodeSize;

        public Vector2Int DecoratorSize => _decoratorSize;
        
        public int NodeShiftVertical => _nodeShiftVertical;

        public int NodeShiftHorizontal => _nodeShiftHorizontal;

        public int DecoratorShiftHorizontal => _decoratorShiftHorizontal;
        
#endregion

        public Rect SourceNodeRect => new Rect(_sourceNodePosition, _nodeSize);
    }
}