# Reactive Calculator 

This project was originally a workshop for ALA (Abstraction Layered Architecture) reference here: (https://abstractionlayeredarchitecture.com)

The exercise was to write a calculator using ALA (in C# for Visual Studio on Windows).

# The calculator

I have had a long standing interest in calculators, and over the years owned many HP RPN calculators. 

I eventually became despondent with them when they had large dot matrix displays. I felt they never used the display real estate well. They didn't show you the formula that you had used to get the answer so you could check what you had done. They didn't allow you to edit the formula in situ. They didn't allow you to have multiple formulas at the same time. They didn't let you string multiple formulas together, referring to the labels of other formula results. They didn't re-evaluate through all the dependencies if you changed an input value or formula. In short, a calculator should be a cross between a calulator and a spreadsheet.

The calculator we develop here is the calculator that these calculators should have been, and what all computer or phone calculators should be at a minimum:

![Reactive calculator](/images/CalculatorScreenshot2.png)

Click on the Wiki for how you use the calculator. Also documentation on how to understand the ALA architecture code. https://github.com/johnspray74/ReactiveCalculator/wiki

# Folders and files

The project contains the four folders that correspond to the four normal layers of ALA:

* Application
* Domain Abstractions
* Programming Paradigms
* Library

Inside the Application folder is a Hello world application, which is the starting point for writing the calculators, and development versions of the calculator.
Inside the Domain Abstractions folder are a few pre-written reuseable abstractions for the Application and Requirements Abstractions layers to use.
Inside the ProgrammingParadigms folder are some interfaces that allow instances of abstractions to be wired together in different ways and execute at run-time.
Inside the Library folder is the WireTo extension method that the Applications and Requirements abstractions layers use to wire up those instances. 


## Getting started

1. If you don't already have Visual Studio, install Visual Studio Community 2019 from https://visualstudio.microsoft.com/vs for C# development on a Windows PC.
    1. Once the installer loads select the ".NET desktop development" from the options and then click Install
    ![Install_VisualStudio](/images/Install_VisualStudio.PNG)

2. If you don't already have Git installed, install Git from https://git-scm.com/download/win. It is worth making the effort to use Git to clone the repository because then you can get the latest version easily. We may be adding new material up until the workshop starts. It will also allow you to contribute your changes back to the repository for others to use.
    1. In the Select Components window, leave all default options checked and check any other additional components you want installed.
    1. Next, in the Choosing the default editor, used by Git unless you're familiar with Vim we highly recommend using a text editor you're comfortable using. If Notepad++ is installed, we suggest using it as your editor. If Notepad++ is not installed, you can cancel the install and [install Notepad++](https://notepad-plus-plus.org/) and then restart the GitHub install.
    1. Leave all other options as the recommended defaults

    If you can't make Git work, you can download a zip file from this repository.

3. Using Windows Explorer, go to a folder on your PC to clone the project.

4. Inside the folder right click and select "Git Bash" from the context menu
    ![Open_GitBash](/images/Open_GitBash.PNG)
    
5. Inside the Git Bash terminal clone the repository with the command:
    ```
    $ git clone https://github.com/john-spray/ReactiveCalculator.git
    ```
    ![Git_Clone](/images/Git_Clone.PNG)

    Alternatively, unzip the downloaded zip file into the folder.

6. Double click the file ReactiveCalculator.sln, which will open in Visual Studio.
    1. If you get a pop up asking "How do you want to open this file?" select either "Microsoft Visual Studio Version Selector" Or "Visual Studio 2019"
    ![Version_Selector](/images/Version_Selector.PNG)

7. Press F5 to run the default application which is the latest version of the calculator. The calculator uses a package which it should get from Nuget automatically. If you get an error about a package missing, try right cliking on the solution in Solution Explorer the Restore Nuget packages. If that fails, the are instruction below to manually install the needed package to the solution.

8. If you want to view or change the diagrams, install Xmind from https://www.xmind.net/xmind2020/

    I am in the process of transitioning to a new diagramming tool specifically written to support ALA application diagrams. So Xmind is a temporary tool.
    
9. Click on 'release' on the main ReactiveCalculator page of Github or click on this link:

    https://github.com/john-spray/ReactiveCalculator/releases

    Click "Download the XMindParser here" to download it. Save it anywhere on your computer. 
    In a Windows Explorer window, unzip it so that the tool's executable is ready to use.
	
10. The workshop project uses a package that doesn't come installed with Visual Studio. It should isntall automatically when you press F5 to run the program. If it doesn't install, these are the manual instracyions. 

    1. In Visual Studio, click Tools, Nuget Packet Manager, Manage Nuget Packages for Solution
	
	![Nuget_Package_Manager](/images/ScriptingNuget01.png)
	
	2. Click Browse and enter CodeAnalysis into the search box. Select "Microsoft.CodeAnalysis.CSharp.Scripting". Tick ReactiveCalculator and Click Install.
	
	![CodeAnalysis](/images/ScriptingNuget02.png)
	
	3. Allow it to install dependencies and Accept the licenses.
	
	![CodeAnalysis_installed](/images/ScriptingNuget03.png)

11. You will need to pull the latest changes in the repository on the morning of the workshop. 

    Inside the project folder right click and select "Git Bash" from the context menu.
    
    Inside the Git Bash terminal pull the latest changes with this command:
    ```
    $ git pull
    ```
