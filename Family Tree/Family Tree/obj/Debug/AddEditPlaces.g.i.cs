﻿#pragma checksum "..\..\AddEditPlaces.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F36CFE2DAE90123209E7A2984185AE51C3F3B4B0FE90A90B31099314E95BF622"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Family_Tree;
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


namespace Family_Tree {
    
    
    /// <summary>
    /// AddEditPlace
    /// </summary>
    public partial class AddEditPlace : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\AddEditPlaces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name_textbox;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\AddEditPlaces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox abbr_textbox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\AddEditPlaces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox historicalname_textbox;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\AddEditPlaces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox typeTextBox;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\AddEditPlaces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addressTextBox;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\AddEditPlaces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descrTextBox;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\AddEditPlaces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox placeNotesTextBox;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\AddEditPlaces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image place_Photo;
        
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
            System.Uri resourceLocater = new System.Uri("/Family Tree;component/addeditplaces.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddEditPlaces.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.name_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.abbr_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.historicalname_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.typeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.addressTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.descrTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.placeNotesTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.place_Photo = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            
            #line 103 "..\..\AddEditPlaces.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.LoadPhoto);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 104 "..\..\AddEditPlaces.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeletePhoto);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 120 "..\..\AddEditPlaces.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPerson);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 129 "..\..\AddEditPlaces.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseForm);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

