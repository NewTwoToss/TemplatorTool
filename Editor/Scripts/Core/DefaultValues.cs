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

        [SerializeField]
        private ComponentDefaultValues _rectTransform;
        
        [SerializeField]
        private ComponentDefaultValues _image;

        [SerializeField]
        private ComponentDefaultValues _text;
        
        [SerializeField]
        private ComponentDefaultValues _button;

#endregion

#region [GETTERS]



#endregion

        public ComponentDefaultValues RectTransform => _rectTransform;

        public ComponentDefaultValues Image => _image;

        public ComponentDefaultValues Text => _text;

        public ComponentDefaultValues Button => _button;
    }
}