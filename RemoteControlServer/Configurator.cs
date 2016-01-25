using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Planning.Bindings;
using Ninject.Modules;
using RemoteControlServer.Definitions;

namespace RemoteControlServer
{
    class Configurator : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IClientRepository>().To<ClientRepository.ClientRepository>().InSingletonScope();
            this.Bind<IClientService>().To<ClientService.ClientService>().InSingletonScope();
            this.Bind<ICommandComposer>().To<Composer.CommandComposer>().InSingletonScope();

            this.Bind<ICommandParser>().To<Parser.CommandParser>().InSingletonScope();
            this.Bind<IInputHandler>().To<InputHandler.InputHandler>().InSingletonScope();
            this.Bind<IInputParser>().To<Parser.InputParser>().InSingletonScope();
            this.Bind<ILogger>().To<ConsoleLogger.ConsoleLogger>().InSingletonScope();
            this.Bind<IOutputComposer>().To<Composer.OutputComposer>().InSingletonScope();
            this.Bind<IOutputHandler>().To<OutputHandler.OutputHandler>().InSingletonScope();

            this.Bind<ICommandExecuter>().To<CommandExecuter.PDVDCommandExecuter>().InSingletonScope();
            this.Bind<ICommandExecuter>().To<CommandExecuter.WindowsCommandExecuter>().InSingletonScope();

        }
    }
}

