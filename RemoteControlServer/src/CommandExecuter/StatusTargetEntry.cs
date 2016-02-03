using RemoteControlServer.CommandExecuter;
using RemoteControlServer.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.CommandExecuter
{
    public class StatusTargetEntry
    {
        private ICommandTarget targetObject;
        private MethodInfo targetMethod;
        private StatusRegistration targetInfo;
        private ParameterInfo[] arguments;

        public StatusTargetEntry(ICommandTarget targetObject_, MethodInfo targetMethod_, StatusRegistration targetInfo_)
        {
            targetObject = targetObject_;
            targetMethod = targetMethod_;
            targetInfo = targetInfo_;
            arguments = targetMethod.GetParameters();
        }

        public object execute()
        {
            return targetMethod.Invoke(targetObject, new object[0]);
        }

        public string getStatusName()
        {
            return targetInfo.getStatusName();
        }
    }

}
