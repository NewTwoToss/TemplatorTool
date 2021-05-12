// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 12.05.2021
// =================================================================================================

using System;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Core
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

#endregion

#region [INSPECTOR GETTERS]

        public Vector2Int SourceNodePosition => _sourceNodePosition;
        
        public Vector2Int NodeSize => _nodeSize;

        public Vector2Int DecoratorSize => _decoratorSize;
        
#endregion

        public Rect SourceNodeRect => new Rect(_sourceNodePosition, _nodeSize);

    }
}