using System;
using System.Reflection;

namespace ApiFramework.Areas.HelpPage.ModelDescriptions {
  public interface IModelDocumentationProvider {
    string GetDocumentation(MemberInfo member);

    string GetDocumentation(Type type);
  }
}