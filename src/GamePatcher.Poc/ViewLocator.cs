namespace PleOps.GamePatcher.Poc;

using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using FluentAvalonia.UI.Controls;
using PleOps.GamePatcher.Poc.Pages;

public class ViewLocator : IDataTemplate, INavigationPageFactory
{
    public Control? Build(object? data)
    {
        if (data is null)
        {
            return null;
        }

        Type dataType = data.GetType();
        string assemblyName = dataType.Assembly.FullName!;
        string typeName = dataType.FullName!.Replace("ViewModel", "View");
        string qualifiedName = $"{typeName}, {assemblyName}";
        var type = Type.GetType(qualifiedName);

        if (type != null) {
            return (Control)Activator.CreateInstance(type)!;
        } else {
            return new TextBlock { Text = "Not Found: " + qualifiedName };
        }
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }

    public Control GetPage(Type srcType)
    {
        return null!;
    }

    public Control GetPageFromObject(object target)
    {
        Type srcType = target.GetType();
        string assemblyName = srcType.Assembly.FullName!;
        string typeName = srcType.FullName!.Replace("ViewModel", "View");
        string qualifiedName = $"{typeName}, {assemblyName}";
        var type = Type.GetType(qualifiedName);

        if (type != null) {
            Control control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = target;
            return control;
        } else {
            throw new InvalidOperationException("Not found: " + qualifiedName);
        }
    }
}
