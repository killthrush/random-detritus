﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Subscriber {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NServiceBusResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal NServiceBusResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Subscriber.NServiceBusResources", typeof(NServiceBusResources).Assembly);
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
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;license id=&quot;fc94f5c9-e543-4617-8501-870f6d576602&quot; expiration=&quot;9999-12-31T23:59:59.9999999&quot; type=&quot;Standard&quot; LicenseType=&quot;Basic8&quot; MaxMessageThroughputPerSecond=&quot;8&quot; LicenseVersion=&quot;4.3&quot; WorkerThreads=&quot;Max&quot; AllowedNumberOfWorkerNodes=&quot;2&quot; Quantity=&quot;1&quot; UpgradeProtectionExpiration=&quot;2014-08-01&quot;&gt;
        ///  &lt;name&gt;ProTeck Services&lt;/name&gt;
        ///  &lt;Signature xmlns=&quot;http://www.w3.org/2000/09/xmldsig#&quot;&gt;
        ///    &lt;SignedInfo&gt;
        ///      &lt;CanonicalizationMethod Algorithm=&quot;http://www.w3.org/TR/2001/REC-x [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string NServiceBusLicense {
            get {
                return ResourceManager.GetString("NServiceBusLicense", resourceCulture);
            }
        }
    }
}
