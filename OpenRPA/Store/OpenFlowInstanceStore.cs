﻿using Newtonsoft.Json.Linq;
using OpenRPA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRPA.Store
{
    class OpenFlowInstanceStore : CustomInstanceStoreBase
    {
        private static Guid storeId = new Guid("0bfcc3a5-3c77-421b-b575-73533563a1f3");
        public string fqdn { get; set; }
        public string host { get; set; }
        public OpenFlowInstanceStore() : base(storeId)
        {
            fqdn = System.Net.Dns.GetHostEntry(Environment.MachineName).HostName.ToLower();
            host = Environment.MachineName.ToLower();
        }
        private static object _lock = new object();
        public override void Save(Guid instanceId, Guid storeId, string doc)
        {
            //try
            //{
            //    var folder = System.IO.Path.Combine(Interfaces.Extensions.ProjectsDirectory, "state");
            //    var filename = System.IO.Path.Combine(folder, instanceId.ToString() + ".xml");
            //    if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);
            //    System.IO.File.WriteAllText(filename, doc);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error("OpenFlowInstanceStore.save: " + ex.Message);
            //}
            try
            {
                var i = WorkflowInstance.Instances.Where(x => x.InstanceId == instanceId.ToString()).FirstOrDefault();
                if (i != null)
                {
                    i.xml = Interfaces.Extensions.Base64Encode(doc);
                    _ = i.Save<WorkflowInstance>(true);
                }
            }
            catch (Exception ex)
            {
                Log.Error("OpenFlowInstanceStore.save: " + ex.Message);
            }
        }
        public override string Load(Guid instanceId, Guid storeId)
        {
            //try
            //{
            //    var folder = System.IO.Path.Combine(Interfaces.Extensions.ProjectsDirectory, "state");
            //    var filename = System.IO.Path.Combine(folder, instanceId.ToString() + ".xml");
            //    if (System.IO.File.Exists(filename))
            //    {
            //        var _xml = System.IO.File.ReadAllText(filename) + "";
            //        if (!string.IsNullOrEmpty(_xml.Trim()))
            //        {
            //            return _xml;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error("OpenFlowInstanceStore.Load: " + ex.Message);
            //}
            try
            {
                var i = WorkflowInstance.Instances.Where(x => x.InstanceId == instanceId.ToString()).FirstOrDefault();
                if (i != null)
                {
                    if (string.IsNullOrEmpty(i.xml))
                    {
                        Log.Error("Error locating " + instanceId.ToString() + " in Instance Store ( found but state is empty!!!!) ");
                        return null;
                    }
                    Log.Debug("Loading " + instanceId.ToString() + " from Instance Store");
                    return Interfaces.Extensions.Base64Decode(i.xml);
                }
                Log.Error("Error locating " + instanceId.ToString() + " in Instance Store");
                return null;
            }
            catch (Exception ex)
            {
                Log.Error("OpenFlowInstanceStore.Load: " + ex.Message);
            }
            return null;
        }
    }
}
