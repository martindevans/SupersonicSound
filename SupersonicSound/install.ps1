param($installPath, $toolsPath, $package, $project)

Function MakeCopyToOutput($item)
{
    $item.Properties.Item("BuildAction").Value = [int]0
    $item.Properties.Item("CopyToOutputDirectory").Value = [int]2
}

$x86dir = $project.ProjectItems.Item("Dependencies").ProjectItems.Item("x86")
MakeCopyToOutput($x86dir.ProjectItems.Item("fmod.dll"))
MakeCopyToOutput($x86dir.ProjectItems.Item("fmodstudio.dll"))

$x86_64dir = $project.ProjectItems.Item("Dependencies").ProjectItems.Item("x86_64")
MakeCopyToOutput($x86_64dir.ProjectItems.Item("fmod.dll"))
MakeCopyToOutput($x86_64dir.ProjectItems.Item("fmodstudio.dll"))