#region Includes

using System;
using System.Reflection;

#endregion

namespace Daishi.Microservices.TestHarness.Areas.HelpPage.ModelDescriptions {
    public interface IModelDocumentationProvider {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}