// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 12.05.2021
// =================================================================================================

using System.Collections.Generic;

namespace Plugins.GameUIBuilder.Editor.Scripts.Core
{
    public class ComponentCounter
    {
        private readonly List<bool> _isSerialNumber;

        public ComponentCounter()
        {
            _isSerialNumber = new List<bool>();
        }

        public int GetCount()
        {
            for (var i = 0; i < _isSerialNumber.Count; i++)
            {
                if (_isSerialNumber[i]) continue;

                _isSerialNumber[i] = true;
                return i + 1;
            }

            _isSerialNumber.Add(true);

            return _isSerialNumber.Count;
        }

        public void DecreaseCountRectTransform(int number)
        {
            if (_isSerialNumber.Count < number) return;

            _isSerialNumber[number - 1] = false;
        }
    }
}