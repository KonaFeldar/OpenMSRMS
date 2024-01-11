using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using StoreOperations_SDK;

namespace StoreOperations
{
    public class AddonManager
    {
        //Doesn't work: private DicAllAddons = new Dictionary<EnAddonType, List<Interface>>();
        private readonly List<IAppExtension> _addonList;
        private readonly List<IItemInterface> _itemExtensions;

        public enum EnAddonType
        {
            AppExtension,
            Item,
            Department,
            Category,
            Supplier,
            Customer
        }

        public AddonManager()
        {
            _addonList = new List<IAppExtension>();
            _itemExtensions = new List<IItemInterface>();
        }
        
        public List<IAppExtension> AvailableAddons => _addonList;
        public List<IItemInterface> ItemExtensions => _itemExtensions;

        public void LoadAllAddons(string strFolder)
        {
            if (Directory.Exists(strFolder))
            {
                //Only load files if the directory exists
                foreach (var dllFile in Directory.EnumerateFiles(strFolder, "*.dll", SearchOption.AllDirectories))
                {
                    //Load the assembly
                    var asmAddon = Assembly.LoadFrom(dllFile);
                    //See what interfaces it uses
                    foreach (var ti in asmAddon.GetTypes())
                    {
                        if(ti.IsInterface)
                        {
                            //Add the addon to the relevant list (Can be in multiple lists)
                            if (typeof(IAppExtension).IsAssignableFrom(ti))
                            {
                                //Load the addon
                                var appExtInstance = (IAppExtension)Activator.CreateInstance(ti);
                                _addonList.Add(appExtInstance);
                            } 
                            if (typeof(IItemInterface).IsAssignableFrom(ti))
                            {
                                //Load the item interface
                                var itemInstance = (IItemInterface)Activator.CreateInstance(ti);
                                _itemExtensions.Add(itemInstance);
                            }
                        } 
                    }
                }
            }
        }
    }
}