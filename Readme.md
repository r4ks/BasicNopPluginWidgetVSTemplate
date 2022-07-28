# Basic nopCommerce Plugin

You can install the nuget plugin inside the nupkg folder with the command:
```
dotnet new --install .\nupkg\ricks.dev.Template.NetCoreTool.nuspec.1.0.2.nupkg
```

or you can make a new package with the command:
```
nuget.exe pack .\Ricks.Template.NetCoreTool.nuspec -OutputDirectory .\nupkg
```


to uninstall the nuget:
```
dotnet new --uninstall ricks.dev.Template.NetCoreTool.nuspec
```

You can install directly into your command line with the command:
```
dotnet new -i .\Content\Nop.Plugin.Widgets.HumanResource
```

You can uninstall directly into your command line with the command:
```
dotnet new -u .\Content\Nop.Plugin.Widgets.HumanResource
```

When using the template you can find it on when creating a new project into your solution on console categories.
Be sure to choose the destination folder to be the src/Plugins
![adding project destination and project name](http://url/to/https://github.com/r4ks/BasicNopPluginWidgetVSTemplate/target.png)


![naming entity, module and title of menu item](http://url/to/https://github.com/r4ks/BasicNopPluginWidgetVSTemplate/renaming.png)