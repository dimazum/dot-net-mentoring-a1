﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp1.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Logs {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Logs() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ConsoleApp1.Resources.Logs", typeof(Logs).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A apppropriate rule was found: {0}.
        /// </summary>
        internal static string AppropriateRule {
            get {
                return ResourceManager.GetString("AppropriateRule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File Not Moved! Error :{0}.
        /// </summary>
        internal static string ErrorFileNotMoved {
            get {
                return ResourceManager.GetString("ErrorFileNotMoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press &apos;Ctrl+C&apos; or &apos;Ctrl+Break&apos; to quit.
        /// </summary>
        internal static string Exit {
            get {
                return ResourceManager.GetString("Exit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File was moved to : {0}.
        /// </summary>
        internal static string FileMoved {
            get {
                return ResourceManager.GetString("FileMoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A new file was created : {0}.
        /// </summary>
        internal static string NewFile {
            get {
                return ResourceManager.GetString("NewFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Start Watching....
        /// </summary>
        internal static string StartWatching {
            get {
                return ResourceManager.GetString("StartWatching", resourceCulture);
            }
        }
    }
}
