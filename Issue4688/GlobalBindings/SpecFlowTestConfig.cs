using System;
using System.Linq;
using System.Configuration;
using System.IO;
using NUnit.Framework;

namespace GlobalBindings
{
    public class SpecFlowTestConfig : ConfigurationSection
    {
        private Configuration configuration;
        public Configuration Config => configuration;

        /// <summary>
        /// get the default configuration instance
        /// </summary>
        protected T GetSection<T>(string section) where T : SpecFlowTestConfig
        {
#if (NETFRAMEWORK)

            return ((Config != null) ? Config.GetSection(section) : ConfigurationManager.GetSection(section)) as T;
#else
            if (Config == null)
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.EntryPoint != null && File.Exists(Path.Combine(TestContext.CurrentContext.WorkDirectory, $"{a.GetName().Name}.dll.config")));
                var assemblyName = assembly?.GetName().Name;

                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"{assemblyName}.dll.config");
                configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            }
            return (configuration.GetSection(section)) as T;
#endif
        }

        /// <summary>
        /// General settings for test execution
        /// </summary>
        [ConfigurationProperty("test", IsRequired = true, IsKey = false)]
        public RunningTestConfig RunningTest => (RunningTestConfig)this["test"];


        //  ======= Create Singleton => provide a static 'Instance' property ===========
        private static SpecFlowTestConfig instance = null;
        private static readonly object padlock = new object();
        public static SpecFlowTestConfig Instance
        {
            get
            {
                lock (padlock)
                {
                    //TODO: Optional: Add a breakpoint here (O)
                    if (instance == null)
                    {
                        var x = 0;
                        x++;
                    }
                    return instance ?? (instance = new SpecFlowTestConfig().GetSection<SpecFlowTestConfig>("testSettings"));
                }
            }
        }
    }
}
