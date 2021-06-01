// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 01.06.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.Utilities
{
    public static class PivotUtilities
    {
        public static Vector2 GetPivotByIndex(int indexPivot)
        {
            return indexPivot switch
            {
                0 => new Vector2(0, 1),
                1 => new Vector2(0.5f, 1),
                2 => new Vector2(1, 1),
                3 => new Vector2(0, 0.5f),
                4 => new Vector2(0.5f, 0.5f),
                5 => new Vector2(1, 0.5f),
                6 => new Vector2(0, 0),
                7 => new Vector2(0.5f, 0),
                8 => new Vector2(1, 0),
                _ => new Vector2(0.5f, 0.5f)
            };
        }
    }
}