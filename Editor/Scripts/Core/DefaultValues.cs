// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 12.05.2021
// =================================================================================================

using System;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Core
{
    [Serializable]
    public class DefaultValues
    {
#region [INSPECTOR]

        [SerializeField, Range(10, 1_000)]
        private int _buttonWidth = 100;

#endregion

#region [GETTERS]

        public int ButtonWidth => _buttonWidth;

#endregion
    }
}