using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using UnityEngine;

namespace Forge
{
    /// <summary>
    ///     Forge Compiler helps compiling mods on the fly.
    /// </summary>
    internal class ForgeCompiler
    {
        private readonly CSharpCodeProvider _codeProvider = new CSharpCodeProvider(new Dictionary<String, String> { { "CompilerVersion", "v3.5" } });
        private readonly CompilerParameters _compilerParameters = new CompilerParameters();
        private readonly string _assemblies = Path.Combine(Directory.GetCurrentDirectory(), "Binaries"); 

        public ForgeCompiler()
        {
            _compilerParameters.GenerateExecutable = false;
            _compilerParameters.GenerateInMemory = true;
            _compilerParameters.ReferencedAssemblies.Add("System.dll");
            _compilerParameters.ReferencedAssemblies.Add("System.Runtime.Serialization.dll");
            _compilerParameters.ReferencedAssemblies.Add("System.XML.dll");
            _compilerParameters.ReferencedAssemblies.Add(Path.Combine(_assemblies, "UnityEngine.dll"));
            _compilerParameters.ReferencedAssemblies.Add(Path.Combine(_assemblies, "Forge.dll"));
            _compilerParameters.ReferencedAssemblies.Add(Path.Combine(_assemblies, "ModdingAPI.dll"));
        }

        /// <summary>
        ///     Compiles .cs files from a given folder.
        /// </summary>
        /// <param name="modDir"></param>
        /// <returns>The newly compiled C# assembly</returns>
        public Assembly CompileMod(DirectoryInfo modDir)
        {
            string[] files = Directory.GetFiles(modDir.FullName, "*.cs", SearchOption.AllDirectories);

            CompilerResults results = _codeProvider.CompileAssemblyFromFile(_compilerParameters, files);

            foreach (CompilerError error in results.Errors)
            {
                Debug.Log(error.ErrorText);
                Debug.Log(error.FileName);
                Debug.Log(error.Line);
            }

            return results.CompiledAssembly;
        }
    }
}
