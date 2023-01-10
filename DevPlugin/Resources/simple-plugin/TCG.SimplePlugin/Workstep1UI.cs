using Slb.Ocean.Core;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Units;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace TCG.SimplePlugin
{
    /// <summary>
    /// This class is the user interface which forms the focus for the capabilities offered by the process.  
    /// This often includes UI to set up arguments and interactively run a batch part expressed as a workstep.
    /// </summary>
    partial class Workstep1UI : UserControl
    {
        private Workstep1 workstep;
        /// <summary>
        /// The argument package instance being edited by the UI.
        /// </summary>
        private Workstep1.Arguments args;
        /// <summary>
        /// Contains the actual underlaying context.
        /// </summary>
        private WorkflowContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Workstep1UI"/> class.
        /// </summary>
        /// <param name="workstep">the workstep instance</param>
        /// <param name="args">the arguments</param>
        /// <param name="context">the underlying context in which this UI is being used</param>
        public Workstep1UI(Workstep1 workstep, Workstep1.Arguments args, WorkflowContext context)
        {
            InitializeComponent();

            this.workstep = workstep;
            this.args = args;
            this.context = context;
        }
    }
}
