﻿#pragma checksum "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2300625BABB4A8AA141099F264AE1721646EB6E3D829D0B5462F3319FC920966"
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
using Torun.Windows.WeeklyPlan;


namespace Torun.Windows.WeeklyPlan {
    
    
    /// <summary>
    /// EditPlanTransferBtn
    /// </summary>
    public partial class EditPlanTransferBtn : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label calendarTitle;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label dateChoosetxt;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar calendar;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save;
        
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
            System.Uri resourceLocater = new System.Uri("/Torun;component/windows/weeklyplan/editplantransferbtn.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
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
            
            #line 8 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
            ((Torun.Windows.WeeklyPlan.EditPlanTransferBtn)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.Window_KeyUp);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
            ((Torun.Windows.WeeklyPlan.EditPlanTransferBtn)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
            ((Torun.Windows.WeeklyPlan.EditPlanTransferBtn)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
            ((Torun.Windows.WeeklyPlan.EditPlanTransferBtn)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.calendarTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.BtnClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dateChoosetxt = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.calendar = ((System.Windows.Controls.Calendar)(target));
            
            #line 33 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
            this.calendar.SelectedDatesChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.Calendar_SelectedDatesChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.save = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\Windows\WeeklyPlan\EditPlanTransferBtn.xaml"
            this.save.Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

