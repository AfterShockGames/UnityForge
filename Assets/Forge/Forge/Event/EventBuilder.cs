using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Forge.Forge.Event
{
    /// <summary>
    ///     Responsible for building all event related functions
    /// </summary>
    internal class EventBuilder
    {
        /// <summary>
        ///     Class type used during building
        /// </summary>
        private readonly Type _classType;

        /// <summary>
        /// 
        /// </summary>
        private TypeBuilder _typeBuilder;

        /// <summary>
        ///     
        /// </summary>
        /// <param name="classType">The class type to build on</param>
        /// <param name="typeBuilder">The typeBuilder to build on</param>
        public EventBuilder(Type classType, TypeBuilder typeBuilder)
        {
            _classType = classType;
            _typeBuilder = typeBuilder;
        }

        /// <summary>
        ///     Adds all event related functions to the typeBuilder
        /// </summary>
        /// <returns>A TypeBuilder with the new event functions</returns>
        public TypeBuilder BuildEvents()
        {


            return _typeBuilder;
        }

        /// <summary>
        ///     Builds all property events
        /// </summary>
        private void BuildPropertyEvents()
        {
            IEnumerable<PropertyInfo> propertyEvents = _classType
                .GetProperties()
                .Where(property => property.IsDefined(typeof(PropertyEvent), false));

            foreach (PropertyInfo property in propertyEvents)
            {
                AddPropertyEventOverride(property);
            }
        }

        /// <summary>
        ///     Adds the property overrides to accomodate for the event
        /// </summary>
        /// <param name="property"></param>
        private void AddPropertyEventOverride(PropertyInfo property)
        {
            
        }
    }
}
