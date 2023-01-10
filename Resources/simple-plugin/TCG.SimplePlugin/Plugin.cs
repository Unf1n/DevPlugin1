using System;
using System.Collections.Generic;

using Slb.Ocean.Core;

namespace TCG.SimplePlugin
{
    public class Plugin : Slb.Ocean.Core.Plugin
    {
        public override string AppVersion
        {
            get { return "1.2.3"; }
        }

        public override string Author
        {
            get { return "Timal Consulting Group"; }
        }

        public override string Contact
        {
            get { return "plugins@timal.kz"; }
        }

        public override IEnumerable<PluginIdentifier> Dependencies
        {
            get { return null; }
        }

        public override string Description
        {
            get { return ""; }
        }

        public override string ImageResourceName
        {
            get { return null; }
        }

        public override Uri PluginUri
        {
            get { return new Uri("http://www.timal.kz"); }
        }

        public override IEnumerable<ModuleReference> Modules
        {
            get 
            {
                // Please fill this method with your modules with lines like this:
                yield return new ModuleReference(typeof(Module));

            }
        }

        public override string Name
        {
            get { return "TCG.SimplePlugin"; }
        }

        public override PluginIdentifier PluginId
        {
            get { return new PluginIdentifier(typeof(Plugin).FullName, typeof(Plugin).Assembly.GetName().Version); }
        }

        public override ModuleTrust Trust
        {
            get { return new ModuleTrust("Default"); }
        }
    }
}
