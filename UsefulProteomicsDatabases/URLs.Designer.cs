﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UsefulProteomicsDatabases {
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
    internal class URLs {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal URLs() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("UsefulProteomicsDatabases.URLs", typeof(URLs).Assembly);
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
        ///   Looks up a localized string similar to http://www.physics.nist.gov/cgi-bin/Compositions/stand_alone.pl?ele=&amp;all=all&amp;ascii=ascii2&amp;isotype=some.
        /// </summary>
        internal static string elementURI {
            get {
                return ResourceManager.GetString("elementURI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://psidev.cvs.sourceforge.net/viewvc/psidev/psi/mod/data/PSI-MOD.obo.xml.
        /// </summary>
        internal static string psimodURI {
            get {
                return ResourceManager.GetString("psimodURI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://www.unimod.org/xml/unimod_tables.xml.
        /// </summary>
        internal static string unimodURI {
            get {
                return ResourceManager.GetString("unimodURI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://www.uniprot.org/docs/ptmlist.txt.
        /// </summary>
        internal static string uniprotURI {
            get {
                return ResourceManager.GetString("uniprotURI", resourceCulture);
            }
        }
    }
}
