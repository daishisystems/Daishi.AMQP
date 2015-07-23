#region Includes

using System;

#endregion

namespace Daishi.Microservices.Web.Areas.HelpPage.ModelDescriptions {
    public class ParameterAnnotation {
        public Attribute AnnotationAttribute { get; set; }

        public string Documentation { get; set; }
    }
}