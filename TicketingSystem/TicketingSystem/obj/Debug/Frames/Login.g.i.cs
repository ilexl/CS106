﻿#pragma checksum "..\..\..\Frames\Login.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BC2371BFE153DF2238E4F30E67D78CE954205F755C5300117332548247160A5E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TicketingSystem.Frames;


namespace TicketingSystem.Frames {
    
    
    /// <summary>
    /// Login
    /// </summary>
    public partial class Login : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Frames\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LoginUserName;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Frames\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LoginPassword;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Frames\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoginButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TicketingSystem;component/frames/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Frames\Login.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LoginUserName = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\..\Frames\Login.xaml"
            this.LoginUserName.KeyDown += new System.Windows.Input.KeyEventHandler(this.OnKeyDownHandler);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\Frames\Login.xaml"
            this.LoginUserName.GotFocus += new System.Windows.RoutedEventHandler(this.UTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\Frames\Login.xaml"
            this.LoginUserName.LostFocus += new System.Windows.RoutedEventHandler(this.UTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LoginPassword = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\..\Frames\Login.xaml"
            this.LoginPassword.KeyDown += new System.Windows.Input.KeyEventHandler(this.OnKeyDownHandler);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\Frames\Login.xaml"
            this.LoginPassword.GotFocus += new System.Windows.RoutedEventHandler(this.PTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\Frames\Login.xaml"
            this.LoginPassword.LostFocus += new System.Windows.RoutedEventHandler(this.PTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LoginButton = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Frames\Login.xaml"
            this.LoginButton.Click += new System.Windows.RoutedEventHandler(this.ButtonClick_Login);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

