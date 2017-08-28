using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DropDownListItemAttributess
{
    /// <summary>
    /// inheriting the DropDownList to save attributes
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class DropDownListAttributes : DropDownList
    {
        /// <summary>
        /// By using this method save the attributes data in view state
        /// </summary>
        /// <returns></returns>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        protected override object SaveViewState()
        {
            var allStates = new object[Items.Count + 1];

            var baseState = base.SaveViewState();
            allStates[0] = baseState;

            var i = 1;

            foreach (ListItem li in Items)
            {
                var j = 0;
                var attributes = new string[li.Attributes.Count][];
                foreach (string attribute in li.Attributes.Keys)
                    attributes[j++] = new[] { attribute, li.Attributes[attribute] };

                allStates[i++] = attributes;
            }
            return allStates;
        }

        /// <summary>
        /// /// By using this method Load the attributes data from view state
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                var myState = (object[])savedState;

                if (myState[0] != null)
                    base.LoadViewState(myState[0]);

                var i = 1;
                foreach (ListItem li in Items)
                    foreach (var attribute in (string[][])myState[i++])
                        li.Attributes[attribute[0]] = attribute[1];
            }
        }
    }
}