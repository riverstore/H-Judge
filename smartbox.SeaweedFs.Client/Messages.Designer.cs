﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace smartbox.SeaweedFs.Client {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    internal class Messages {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("smartbox.SeaweedFs.Client.Messages", typeof(Messages).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connection is closed..
        /// </summary>
        internal static string ConnectionClosedError {
            get {
                return ResourceManager.GetString("ConnectionClosedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value must be between 0 and 9..
        /// </summary>
        internal static string CountOutOfRange {
            get {
                return ResourceManager.GetString("CountOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not fetch server cluster status..
        /// </summary>
        internal static string FetchClusterStatusError {
            get {
                return ResourceManager.GetString("FetchClusterStatusError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fetch file from [{0}/{1}] has not been found, response stats code is [{2}].
        /// </summary>
        internal static string FetchFileNotFoundError {
            get {
                return ResourceManager.GetString("FetchFileNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fetch file from [{0}/{1}] has been redirected, response stats code is [{2}]..
        /// </summary>
        internal static string FetchFileRedirectError {
            get {
                return ResourceManager.GetString("FetchFileRedirectError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fetch file from [{0}/{1}] has returned request error, response stats code is [{2}].
        /// </summary>
        internal static string FetchFileRequestError {
            get {
                return ResourceManager.GetString("FetchFileRequestError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File does not exist..
        /// </summary>
        internal static string FileDoesNotExist {
            get {
                return ResourceManager.GetString("FileDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not parse last modified time [{0}] to long value..
        /// </summary>
        internal static string FileStatusParseException {
            get {
                return ResourceManager.GetString("FileStatusParseException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not find the volume server..
        /// </summary>
        internal static string VolumeServerNotFound {
            get {
                return ResourceManager.GetString("VolumeServerNotFound", resourceCulture);
            }
        }
    }
}
