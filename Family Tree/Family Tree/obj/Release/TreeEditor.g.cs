﻿#pragma checksum "..\..\TreeEditor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B2E63C0847B28AB41B45662844ABBFE68DB765F1EC13B4C440EB6E13BF2BA4B9"
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
    /// TreeEditor
    /// </summary>
    public partial class TreeEditor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 118 "..\..\TreeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid myDataGrid;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\TreeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rect;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\TreeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sortButton1;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\TreeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sortButton2;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\TreeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sortButton3;
        
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
            System.Uri resourceLocater = new System.Uri("/Family Tree;component/treeeditor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TreeEditor.xaml"
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
            
            #line 10 "..\..\TreeEditor.xaml"
            ((Family_Tree.TreeEditor)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 11 "..\..\TreeEditor.xaml"
            ((Family_Tree.TreeEditor)(target)).Activated += new System.EventHandler(this.Window_Activated);
            
            #line default
            #line hidden
            
            #line 13 "..\..\TreeEditor.xaml"
            ((Family_Tree.TreeEditor)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 81 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuSwitchPeople);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 82 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuSwitchPlaces);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 83 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuSwitchEvents);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 84 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuCreateTree);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 87 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_OpenTree);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 88 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_CreateTree);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 91 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Romanovs);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 92 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.British);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 93 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MagnCentury);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 98 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click_1);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 99 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 100 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit);
            
            #line default
            #line hidden
            return;
            case 14:
            this.myDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 118 "..\..\TreeEditor.xaml"
            this.myDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.myDataGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 15:
            this.rect = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 16:
            
            #line 172 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 173 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 174 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Edit);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 175 "..\..\TreeEditor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_Show);
            
            #line default
            #line hidden
            return;
            case 20:
            this.sortButton1 = ((System.Windows.Controls.Button)(target));
            
            #line 177 "..\..\TreeEditor.xaml"
            this.sortButton1.Click += new System.Windows.RoutedEventHandler(this.Sort1);
            
            #line default
            #line hidden
            return;
            case 21:
            this.sortButton2 = ((System.Windows.Controls.Button)(target));
            
            #line 180 "..\..\TreeEditor.xaml"
            this.sortButton2.Click += new System.Windows.RoutedEventHandler(this.Sort2);
            
            #line default
            #line hidden
            return;
            case 22:
            this.sortButton3 = ((System.Windows.Controls.Button)(target));
            
            #line 183 "..\..\TreeEditor.xaml"
            this.sortButton3.Click += new System.Windows.RoutedEventHandler(this.Sort3);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

