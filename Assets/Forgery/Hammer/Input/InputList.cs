using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Forgery.Hammer.Input
{
    /// <summary>
    ///     TODO::Finish this.
    /// </summary>
    [Obsolete]
    public class InputList
    {
        private static Dictionary<KeyCode, Dictionary<string, Action>> _inputEvents = new Dictionary<KeyCode, Dictionary<string, Action>>();

        public static void AddKeybind(KeyCode key, Action action, string description)
        {
            if (_inputEvents.ContainsKey(key))
            {
                _inputEvents[key].Add(description, action);
            }
            else
            {
                _inputEvents[key] = new Dictionary<string, Action>
                {
                    {description, action}
                };
            }
        }

        public List<Action> GetAllActions()
        {
            List<Action> actions = new List<Action>();

            _inputEvents.ToList().ForEach(x =>
            {
                if (UnityEngine.Input.GetKey(x.Key) && _inputEvents.ContainsKey(x.Key))
                {
                    actions.AddRange(_inputEvents[x.Key].Values);
                }
            });

            return actions;
        }
    }
}
