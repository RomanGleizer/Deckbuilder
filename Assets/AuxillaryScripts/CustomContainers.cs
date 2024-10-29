using System;
using System.Collections;
using System.Collections.Generic;

namespace Custom.Collections
{
    /// <summary>
    /// Queue that automatically removes commands from itself if necessary
    /// </summary>
    public class CommandQueue : IEnumerable<Command>
    {
        private CustomPriorityQueue<Command> _commands;

        private int _executeCount = 1;

        public IEnumerator<Command> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            while (_executeCount > 0)
            {
                _executeCount--;
                if (_executeCount > 0)
                {
                    yield return EnumeratorForSeveralExecutes();
                }
            }
            _commands.Clear();
        }

        private IEnumerator EnumeratorForSeveralExecutes()
        {
            foreach (Command command in _commands)
            {
                if (command.CommandType != CommandType.Attack) _commands.Remove(command);
                yield return command;
            }
        }

        public void Enqueue(Command command)
        {
            _commands.InsertByPriority(command);
        }

        public void IncreaseExecuteCount()
        {
            _executeCount++;
        }
    }


    public class Node<T> where T : IComparable
    {
        public Node<T> PrevNode { get; set; }
        public Node<T> NextNode { get; set; }

        public readonly T Value;

        public Node(T value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Queue that maintains order by priority and supports deletion during iteration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomPriorityQueue<T> : IEnumerable<T> where T : IComparable, IPriorityObj
    {
        private Node<T> _startNode;
        private Node<T> _lastNode;

        private Node<T> _currentNode;

        public int Count { get; private set; }

        public void InsertByPriority(T value)
        {
            _currentNode = _startNode;

            while (_currentNode.Value.Priority <= value.Priority)
            {
                _currentNode = _currentNode.NextNode;
            }

            var node = new Node<T>(value);

            node.PrevNode = _currentNode.PrevNode;
            node.NextNode = _currentNode;

            _currentNode.PrevNode.NextNode = node;
            _currentNode.PrevNode = node;
        }

        public T PopFront()
        {
            var value = _startNode.Value;
            Remove(_startNode.Value);
            return value;
        }

        public void Remove(T value)
        {
            if (Count == 0) throw new IndexOutOfRangeException("Container is empty!");

            if (_currentNode.Value.CompareTo(value) != 0) _currentNode = FindNodeByValue(value);

            if (_currentNode == _startNode)
            {
                _startNode = _startNode.NextNode;
                _currentNode.NextNode.PrevNode = null;
            }
            else if (_currentNode == _lastNode)
            {
                _lastNode = _lastNode.PrevNode;
                _currentNode.PrevNode.NextNode = null;
            }
            else
            {
                _currentNode.PrevNode.NextNode = _currentNode.NextNode;
                _currentNode.NextNode.PrevNode = _currentNode.PrevNode;
            }

            _currentNode = _currentNode.NextNode;
        }

        private Node<T> FindNodeByValue(T value)
        {
            _currentNode = _startNode;
            while (_currentNode != null)
            {
                if (_currentNode.Value.CompareTo(value) == 0) return _currentNode;
                _currentNode = _currentNode.NextNode;
            }

            throw new ArgumentNullException("Value doesn't exist in container!");
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var node = _startNode;
            while (node != null)
            {
                yield return node.Value;
                node = node.NextNode;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _startNode = null;
            _currentNode = null;
            _lastNode = null;
        }
    }
}
