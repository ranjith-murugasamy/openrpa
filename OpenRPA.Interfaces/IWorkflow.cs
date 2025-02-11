﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRPA.Interfaces
{
    public delegate void idleOrComplete(IWorkflowInstance sender, EventArgs e);
    public delegate void VisualTrackingHandler(IWorkflowInstance Instance, string ActivityId, string ChildActivityId, string State);
    public interface IWorkflow : INotifyPropertyChanged, IBase
    {
        [JsonIgnore]
        long current_version { get; set; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        string queue { get; set; }
        string Xaml { get; set; }
        string projectid { get; set; }
        [JsonIgnore]
        string RelativeFilename { get; }
        string FilePath { get; }
        string Filename { get; set; }
        bool Serializable { get; set; }
        //IProject Project { get; }
        string ProjectAndName { get; set; }
        List<workflowparameter> Parameters { get; set; }
        IWorkflowInstance CreateInstance(Dictionary<string, object> Parameters, string queuename, string correlationId, idleOrComplete idleOrComplete, VisualTrackingHandler VisualTracking);
        Task Delete();
        string UniqueFilename();
        Task Save();
        Task UpdateImagePermissions();
        Task ExportFile(string filepath);
        void ParseParameters();
        void NotifyUIState();
    }
    public enum workflowparameterdirection
    {
        @in = 0,
        @out = 1,
        inout = 2,
    }
    public class workflowparameter
    {
        public string name { get; set; }
        public string type { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public workflowparameterdirection direction { get; set; }
    }

}
