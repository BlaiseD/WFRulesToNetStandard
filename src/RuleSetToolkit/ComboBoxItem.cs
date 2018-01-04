using System.Workflow.Activities.Rules.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleSetToolkit
{
    internal class ComboBoxItem
    {
        internal ComboBoxItem(string text, DotNetPlatForm platForm)
        {
            this.text = text;
            this.PlatForm = platForm;
        }
        #region Variables
        private string text;
        #endregion Variables

        #region Properties
        internal DotNetPlatForm PlatForm { get; private set; }
        #endregion Properties

        #region Methods
        public override string ToString()
        {
            return text;
        }

        public override bool Equals(object comboItem)
        {
            if (comboItem == null)
                return false;
            if (this.GetType() != comboItem.GetType())
                return false;

            ComboBoxItem item = (ComboBoxItem)comboItem;
            return this.PlatForm == item.PlatForm;
        }

        public override int GetHashCode()
        {
            return this.PlatForm.GetHashCode();
        }
        #endregion Methods
    }
}
