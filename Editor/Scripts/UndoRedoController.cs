// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 17.05.2021
// =================================================================================================

using System;
using System.Collections.Generic;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes;
using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts
{
    [Serializable]
    public class UndoRedoController
    {
        private readonly DTossCreator _data;
        private readonly Stack<SourceNode> _undoStack;
        private readonly Stack<SourceNode> _redoStack;

        public bool IsUndoStack => _undoStack.Count != 0;
        
        public bool IsRedoStack => _redoStack.Count != 0;

        public UndoRedoController(DTossCreator data)
        {
            _data = data;
            _undoStack = new Stack<SourceNode>();
            _redoStack = new Stack<SourceNode>();
        }

        public void RegisterSnapshot()
        {
            _redoStack.Clear();
            var clone = _data.SourceNode.MyClone();
            _undoStack.Push(clone);
            Debug.Log($"_undoStack: {_undoStack.Count}");
        }

        public void Undo()
        {
            if (_undoStack.Count == 0) return;
            
            var clone = _data.SourceNode.MyClone();
            _redoStack.Push(clone);
            _data.SourceNode = _undoStack.Pop();
        }

        public void Redo()
        {
            if (_redoStack.Count == 0) return;

            var clone = _data.SourceNode.MyClone();
            _undoStack.Push(clone);
            _data.SourceNode = _redoStack.Pop();
        }

        public void ResetMechanics()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }
    }
}