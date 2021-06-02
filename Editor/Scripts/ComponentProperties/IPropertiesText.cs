// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.ComponentProperties
{
    public interface IPropertiesText
    {
        string Name { get; }

        int Width { get; }

        int Height { get; }
        
        int IndexAnchor { get; }
        
        int IndexPivot { get; }

        string Text { get; }
        
        Color Color { get; }
    }
}