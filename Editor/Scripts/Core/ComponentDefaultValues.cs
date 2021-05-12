// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 12.05.2021
// =================================================================================================

using System;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Core
{
    [Serializable]
    public class ComponentDefaultValues
    {
#region [INSPECTOR]

        [SerializeField, Range(10, 1_000)]
        private int _width = 100;

        [SerializeField, Range(10, 1_000)]
        private int _height = 100;

        [SerializeField]
        private Color _nodeColor;

#endregion

#region [GETTERS]

        public int Width => _width;

        public int Height => _height;

        public Color NodeColor => _nodeColor;

#endregion
    }
}