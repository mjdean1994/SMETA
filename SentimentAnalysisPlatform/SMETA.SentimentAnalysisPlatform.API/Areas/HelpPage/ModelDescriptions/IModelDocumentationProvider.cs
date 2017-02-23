using System;
using System.Reflection;

namespace SMETA.SentimentAnalysisPlatform.API.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}