using System.Diagnostics;

namespace System.Workflow.Activities
{
    internal static class WorkflowActivityTrace
    {
        static TraceSource rules;

        internal static TraceSource Rules
        {
            get { return rules; }
        }

        /// <summary>
        /// Statically set up trace sources
        /// 
        /// To enable logging to a file, add lines like the following to your app config file.
        /*
            <system.diagnostics>
                <switches>
                    <add name="System.Workflow LogToFile" value="1" />
                </switches>
            </system.diagnostics>
        */
        /// To enable tracing to default trace listeners, add lines like the following
        /*
            <system.diagnostics>
                <switches>
                    <add name="System.Workflow LogToTraceListener" value="1" />
                </switches>
            </system.diagnostics>
        */
        /// </summary>
        static WorkflowActivityTrace()
        {

            rules = new TraceSource("System.Workflow.Activities.Rules")
            {
                Switch = new SourceSwitch("System.Workflow.Activities.Rules", SourceLevels.Off.ToString())
            };

            foreach (TraceListener listener in Trace.Listeners)
            {
                if (!(listener is DefaultTraceListener))
                {
                    rules.Listeners.Add(listener);
                }
            }
        }
    }
}
