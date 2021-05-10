// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.ComponentProperties
{
    public interface IPropertiesImage
    {
        string Name { get; }

        int Width { get; }

        int Height { get; }

        Color Color { get; }
    }
}