﻿#pragma checksum "..\..\Connect.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "37D0AD2BE221507E72D466667CC3BD5CC5EFA240"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using WpfMailDBManager;


namespace WpfMailDBManager {
    
    
    /// <summary>
    /// Connect
    /// </summary>
    public partial class Connect : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Connect_TBl_lable_ServerName;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Connect_TBl_lable_Authentication;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Connect_TBl_lable_UserName;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Connect_TBl_lable_Password;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Connect_TBox_dataSource;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Connect_ComboBox_Authentication;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Connect_TBox_UserName;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Connect_PassBox_Password;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Connect_button_OK;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Connect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Connect_button_Cancel;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfMailDBManipulator;component/connect.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Connect.xaml"
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
            this.Connect_TBl_lable_ServerName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Connect_TBl_lable_Authentication = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.Connect_TBl_lable_UserName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Connect_TBl_lable_Password = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Connect_TBox_dataSource = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Connect_ComboBox_Authentication = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\Connect.xaml"
            this.Connect_ComboBox_Authentication.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Connect_ComboBox_Authentication_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Connect_TBox_UserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.Connect_PassBox_Password = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 9:
            this.Connect_button_OK = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\Connect.xaml"
            this.Connect_button_OK.Click += new System.Windows.RoutedEventHandler(this.button_Click_OK);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Connect_button_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\Connect.xaml"
            this.Connect_button_Cancel.Click += new System.Windows.RoutedEventHandler(this.Connect_button_Cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

