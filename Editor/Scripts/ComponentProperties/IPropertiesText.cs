// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 11.05.2021
// =================================================================================================

namespace Plugins.GameUIBuilder.Editor.Scripts.ComponentProperties
{
    public interface IPropertiesText
    {
        string Name { get; }

        int Width { get; }

        int Height { get; }

        string Text { get; }
    }
}