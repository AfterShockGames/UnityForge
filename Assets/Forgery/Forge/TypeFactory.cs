using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Forgery.Forge
{
    /// <summary>
    ///     The TypeFactory is responsible for creating and fixing Forgery Types.
    ///     It intercepts and edits types to be suitible for the factory to forge the object correctly
    /// </summary>
    public class TypeFactory
    {
        private const string FORGERY_ASSEMBLY = "DynamicForgeryAssembly";

        private static ModuleBuilder _moduleBuilder;

        /// <summary>
        ///     Creates a new type from the given type.
        ///     Resolves all attributes and replaces any functions
        /// </summary>
        /// <typeparam name="T">The type to start building on</typeparam>
        /// <returns>The newly created type</returns>
        public Type Create<T>()
        {
            return typeof(T);
            
        }

        /// <summary>
        ///     Get a new TypeBuilder instance
        /// </summary>
        /// <param name="type">The type to start building on</param>
        /// <returns>A new TypeBuilder instance</returns>
        protected TypeBuilder GetTypeBuilder(Type type)
        {
            return _moduleBuilder.DefineType(
                type.Name,
                TypeAttributes.Public |
                TypeAttributes.Class |
                TypeAttributes.AutoClass |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit |
                TypeAttributes.AutoLayout,
                type
            );
        }

        /// <summary>
        ///     Generates the moduleBuilder
        /// </summary>
        protected ModuleBuilder GetModuleBuilder()
        {
            if (_moduleBuilder != null)
            {
                return _moduleBuilder;
            }

            AssemblyName assemblyName = new AssemblyName(FORGERY_ASSEMBLY);
            AssemblyBuilder assemblyBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    assemblyName,
                    AssemblyBuilderAccess.RunAndSave
                );

            _moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll");

            return _moduleBuilder;
        }
    }
}
