﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Unidades_de_Negocio_Sucursales.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.1.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DSN=SIESAPOB;Uid=GT;Pwd=D1sl1c0r3s;")]
        public string strConexionODBC {
            get {
                return ((string)(this["strConexionODBC"]));
            }
            set {
                this["strConexionODBC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Server = 192.168.0.54;Database=Commercial_Effectiveness;User Id = sa; Password = " +
            "D1sl1c0r3s; Integrated Security = False")]
        public string strConexionSQL {
            get {
                return ((string)(this["strConexionSQL"]));
            }
            set {
                this["strConexionSQL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("WSREAL")]
        public string ConexionUNOEE {
            get {
                return ((string)(this["ConexionUNOEE"]));
            }
            set {
                this["ConexionUNOEE"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public string CiaUNOEE {
            get {
                return ((string)(this["CiaUNOEE"]));
            }
            set {
                this["CiaUNOEE"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("servi.siesa")]
        public string UsuarioUNOEE {
            get {
                return ((string)(this["UsuarioUNOEE"]));
            }
            set {
                this["UsuarioUNOEE"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Servi2021")]
        public string ClaveUNOEE {
            get {
                return ((string)(this["ClaveUNOEE"]));
            }
            set {
                this["ClaveUNOEE"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.0.172:8051/WSUNOEE_ATLAS_CE/WSUNOEE.ASMX")]
        public string Unidades_de_Negocio_Sucursales_WSUNOEE_WSUNOEE {
            get {
                return ((string)(this["Unidades_de_Negocio_Sucursales_WSUNOEE_WSUNOEE"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://192.168.0.54:8050/GTIntegration_Heros_CE/ServiciosWeb/wsGenerarPlano.asmx")]
        public string Unidades_de_Negocio_Sucursales_WSGT_wsGenerarPlano {
            get {
                return ((string)(this["Unidades_de_Negocio_Sucursales_WSGT_wsGenerarPlano"]));
            }
        }
    }
}
