﻿using OpenRPA.Interfaces;
using System;
using System.Activities.Presentation.Toolbox;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenRPA.Views
{
    /// <summary>
    /// Interaction logic for Snippets.xaml
    /// </summary>
    public partial class WorkflowInstances : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        public WorkflowInstances()
        {
            InitializeComponent();
            DataContext = this;
        }
        public static readonly DependencyProperty WorkflowProperty =
            DependencyProperty.Register("Workflow", typeof(Workflow), typeof(WorkflowInstances), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
                DefaultValue = null,
                PropertyChangedCallback = OnWorkflowPropertyChanged
            });
        private static void OnWorkflowPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as WorkflowInstances;
            if (me != null) me.Workflow = e.NewValue as Workflow;
        }
        private Workflow workflow = null;
        public Workflow Workflow
        {
            get
            {
                return workflow;
            }
            set
            {
                workflow = value;
                if (workflow != null)
                {
                    Task.Run(() =>
                    {
                        _Instances = workflow.Instances;
                        NotifyPropertyChanged("Instances");
                    });
                }
                else
                {
                    _Instances = new WorkflowInstance[] { };
                    NotifyPropertyChanged("Instances");
                }
            }
        }
        private WorkflowInstance[] _Instances = new WorkflowInstance[] { };
        public WorkflowInstance[] Instances
        {
            get
            {
                return _Instances;
            }
            set
            {

            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Reload();
        }
    }
}
