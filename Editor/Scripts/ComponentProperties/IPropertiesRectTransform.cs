// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 07.05.2021
// =================================================================================================

namespace Plugins.Templator.Editor.Scripts.ComponentProperties
{
    public interface IPropertiesRectTransform
    {
        string Name { get; }

        int Width { get; }

        int Height { get; }
        
        int IndexAnchor { get; }
        
        int IndexPivot { get; }
    }
}