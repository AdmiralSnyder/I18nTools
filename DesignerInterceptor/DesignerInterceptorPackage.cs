using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace DesignerInterceptor;

/// <summary>
/// This is the class that implements the package exposed by this assembly.
/// </summary>
/// <remarks>
/// <para>
/// The minimum requirement for a class to be considered a valid package for Visual Studio
/// is to implement the IVsPackage interface and register itself with the shell.
/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
/// to do it: it derives from the Package class that provides the implementation of the
/// IVsPackage interface and uses the registration attributes defined in the framework to
/// register itself and its components with the shell. These attributes tell the pkgdef creation
/// utility what data to put into .pkgdef file.
/// </para>
/// <para>
/// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
/// </para>
/// </remarks>
[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[ProvideAutoLoad(UIContextGuids.SolutionExists/*"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"*/, PackageAutoLoadFlags.BackgroundLoad)] // UIContextGuids80.WindowsFormsDesigner ist zu spät

[Guid(DesignerInterceptorPackage.PackageGuidString)]
public sealed class DesignerInterceptorPackage : AsyncPackage
{
    /// <summary>
    /// DesignerInterceptorPackage GUID string.
    /// </summary>
    public const string PackageGuidString = "57d8715d-1413-4e12-8d3b-0fa82f32a22d";

    #region Package Members

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
    /// <param name="progress">A provider for progress updates.</param>
    /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        // When initialized asynchronously, the current thread may be a background thread at this point.
        // Do any initialization that requires the UI thread after switching to the UI thread.
        await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);


        DTE = await this.GetServiceAsync<DTE>();
        Events = DTE.Events;
        DocumentEvents = Events.DocumentEvents;
        WindowEvents = Events.WindowEvents;
        //var d = Events.WindowEvents
        WindowEvents.WindowCreated += WindowEvents_WindowCreated;
        DocumentEvents.DocumentOpened += Events_DocumentOpened;

        //if (new ServiceProvider((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)dte) is { } sp)
        //{
        //}
        Console.Beep();
    }


    private void WindowEvents_WindowCreated(Window Window)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        if (!Window.Caption.EndsWith("[Design]")) return;
        //if (Window.Type != vsWindowType.vsWindowTypeDesigner) return;

        DesignerHost = Window.Object as IDesignerHost;

        ServiceProvider = DesignerHost;
        var typeDescriptorFilterService = ServiceProvider.GetService<ITypeDescriptorFilterService>();
        // ITypeDescriptorFilterService

        ShowMessageBox($"Window Created '{Window.Type}' LALA.");

        DesignerHost.RemoveService(typeof(ITypeDescriptorFilterService));
        var newService = new I18nTypeDescriptorFilterService(OldService);
        DesignerHost.AddService(typeof(ITypeDescriptorFilterService), newService);
        ServiceLoaded = true;
    }

    private ITypeDescriptorFilterService OldService;

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        DocumentEvents.DocumentOpened -= Events_DocumentOpened;
        WindowEvents.WindowCreated -= WindowEvents_WindowCreated;
        if (ServiceLoaded)
        {
            // TODO
            //DesignerHost.RemoveService(typeof(ITypeDescriptorFilterService));
            //if (OldService is not null)
            //{
            //    DesignerHost.AddService(typeof(ITypeDescriptorFilterService), OldService);
            //}
        }
    }

    private bool ServiceLoaded;
    private DTE DTE;
    private Events Events;
    private IServiceProvider ServiceProvider;
    private IDesignerHost DesignerHost;
    private DocumentEvents DocumentEvents;
    private WindowEvents WindowEvents;

    private void Events_DocumentOpened(Document Document)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        if (Document.ActiveWindow.Type == vsWindowType.vsWindowTypeDesigner)
        {
            ShowMessageBox($"Document of type '{Document.Type}' opened.");
        }
        
    }

    private void ShowMessageBox(string message) => VsShellUtilities.ShowMessageBox(this, message, "Int18 Designer Interceptor", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

    #endregion
}
