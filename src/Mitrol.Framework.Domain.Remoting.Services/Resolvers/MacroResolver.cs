namespace Mitrol.Framework.Domain.Remoting.Services
{
    using Mitrol.Framework.Domain.Configuration.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Macro;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public sealed class RemoteMacroResolver : IResolver<IMacroProcessing, MacroTypeEnum>
    {
        private readonly IRemoteMachineConfigurationService MachineConfigurationService;
        private readonly IRemoteMachineParameterService ParameterService;

        public RemoteMacroResolver(IRemoteMachineConfigurationService machineConfigurationService
                            , IRemoteMachineParameterService parameterService)
        {
            MachineConfigurationService = machineConfigurationService;
            ParameterService = parameterService;
        }


        public IMacroProcessing Resolve(MacroTypeEnum serviceKind)
        {
            // se le macro di taglio sono abilitate allora carico la DLL corrispondente
            if (MachineConfigurationService.ConfigurationRoot.Machine.IsMacroSupported(serviceKind))
            {

                var relativePath = serviceKind == MacroTypeEnum.MacroCut ? MachineConfigurationService.ConfigurationRoot.Programming.MacroCut.BaseFolder
                                                                      : MachineConfigurationService.ConfigurationRoot.Programming.MacroMill.BaseFolder;


                var path = DomainExtensions.GetStartUpDirectoryInfo().FullName + $"\\{relativePath}";

                var libraryName = serviceKind == MacroTypeEnum.MacroCut ? MachineConfigurationService.ConfigurationRoot.Programming.MacroCut.AssemblyFilename
                                                                : MachineConfigurationService.ConfigurationRoot.Programming.MacroMill.AssemblyFilename;
                //verifico se è presente la DLL delle macro 
                try
                {
                    var assemblyPath = Directory.GetFiles(path, libraryName);
                    if (assemblyPath.Length > 0)
                    {
                        //carico i tipi presenti nella DLL corrispondente

                        //Questa carica gli assembly all'occorrenza 
                        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                        var assemblyTypes = Assembly.LoadFrom(assemblyPath[0]).GetTypes();
                        foreach (var type in assemblyTypes)
                        {
                            // cerco la classe che implementa l'interfaccia IMacroProcessing, cioè quella che ha i metodi per elaborare la Macro
                            var implementedInterfaces = type.GetInterfaces().ToList();
                            var foundInterface = implementedInterfaces.Where(i => i.Name == typeof(IMacroProcessing).Name).SingleOrDefault();
                            if (foundInterface != null)
                            {
                                try
                                {
                                    //verifica di avere tutti i riferimenti necessari per poter usare la DLL.. non applicato perchè viene usato l'evento
                                    //if (!LoadReferenced(type))
                                    //    return null;

                                    var obj = (IMacroProcessing)Activator.CreateInstance(type, new object[] { MachineConfigurationService
                                                                                                , ParameterService});

                                    if (obj.MacroType == serviceKind)
                                        return obj;
                                }
                                catch (Exception e)
                                {
                                    return null;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            return null;
        }

        //Verifica che ci siano tutti i riferimenti necessari 
        private bool LoadReferenced(Type type)
        {
            var assembly = Assembly.GetAssembly(type);
            var refassemblies = assembly.GetReferencedAssemblies();
            foreach (var reference in refassemblies)
            {
                try
                {
                    var sd = Assembly.Load(reference);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //Per gli assembly della DLL da caricare all'occorrenza          
            var assembly = ((AppDomain)sender).GetAssemblies().FirstOrDefault(assemb => assemb.FullName == args.Name);
            if (assembly == null)
            {
                throw new Exception($"Can't find assembly {args.Name}");
            }
            return assembly;
        }
    }
}