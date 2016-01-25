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
            this.Bind<IClient>().To<Listener.Client>();
            this.Bind<IClientRepository>().To<ClientRepository.ClientRepository>();
            this.Bind<IClientService>().To<ClientService.ClientService>();
            this.Bind<ICommandComposer>().To<Composer.CommandComposer>();

            this.Bind<ICommandParser>().To<Parser.CommandParser>();
            this.Bind<IInputHandler>().To<InputHandler.InputHandler>();
            this.Bind<IInputParser>().To<Parser.InputParser>();
            this.Bind<ILogger>().To<ConsoleLogger.ConsoleLogger>();
            this.Bind<IOutputComposer>().To<Composer.OutputComposer>();
            this.Bind<IOutputHandler>().To<OutputHandler.OutputHandler>();

            this.Bind<ICommandExecuter>().To<CommandExecuter.PDVDCommandExecuter>();

        }
    }
}

