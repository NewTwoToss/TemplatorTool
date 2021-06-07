// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 05.06.2021
// =================================================================================================

using System;
using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Core
{
    [Serializable]
    public class ToolIcons
    {
        [SerializeField]
        private Texture2D[] _anchorSelector;

        [SerializeField]
        private Texture2D[] _pivotSelector;

#region [GETTERS]

        public Texture2D[] AnchorSelector => _anchorSelector;

        public Texture2D[] PivotSelector => _pivotSelector;

#endregion
    }
}