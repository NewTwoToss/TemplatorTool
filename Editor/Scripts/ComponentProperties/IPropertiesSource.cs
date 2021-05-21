// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.ComponentProperties
{
    public interface IPropertiesSource
    {
        RectTransform Source { get; }
    }
}