#region Includes

using System.Collections.ObjectModel;

#endregion

namespace Daishi.Microservices.Web.Areas.HelpPage.ModelDescriptions {
    public class ComplexTypeModelDescription : ModelDescription {
        public ComplexTypeModelDescription() {
            Properties = new Collection<ParameterDescription>();
        }

        public Collection<ParameterDescription> Properties { get; private set; }
    }
}