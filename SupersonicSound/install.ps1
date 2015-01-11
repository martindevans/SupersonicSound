param($installPath, $toolsPath, $package, $project)

Function MakeCopyToOutput($project, $path)
{
    $item = $project.ProjectItems.Item($path)
    $item.Properties.Item("BuildAction").Value = [int]0
    $item.Properties.Item("CopyToOutputDirectory").Value = [int]2
}

MakeCopyToOutput($project, "Wrapper/Dependencies/x86/fmod.dll")
MakeCopyToOutput($project, "Wrapper/Dependencies/x86/fmodstudio.dll")
MakeCopyToOutput($project, "Wrapper/Dependencies/x86_64/fmod.dll")
MakeCopyToOutput($project, "Wrapper/Dependencies/x86_64/fmodstudio.dll")