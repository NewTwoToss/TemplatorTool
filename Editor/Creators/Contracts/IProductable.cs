// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 09.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Creators.Contracts
{
    public interface IProductable
    {
        RectTransform Product { get; }
    }
}