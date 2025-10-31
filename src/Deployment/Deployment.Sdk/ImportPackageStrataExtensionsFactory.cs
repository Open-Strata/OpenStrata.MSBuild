using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStrata.Deployment.Sdk
{
    public class ImportPackageStrataExtensionsFactory : IDisposable
    {

        CompositionContainer ImportPackageStrataExtensionsContainer;

        ComposableExtensions composableExtensions;

        public class ComposableExtensions : IDisposable, IPartImportsSatisfiedNotification
        {

            [ImportMany(typeof(IImportPackageStrataFeatureExtension), AllowRecomposition = false)]
            public IEnumerable<IImportPackageStrataFeatureExtension> FeatureExtensionList { get; set; }


            [ImportMany(typeof(IImportPackageStratiExtension), AllowRecomposition = false)]
            public IEnumerable<IImportPackageStratiExtension> StratiExtensionList { get; set; }

            public void Dispose()
            {
                //do nothing
            }

            public void OnImportsSatisfied()
            {
                //do nothing
            }
        }

        public List<IImportPackageStrataExtension> InstantiateExtensions(ImportPackageStrataBase package)
        {

            package.PackageLog.Log($"OpenStrata : ImportPackageStrataExtensionsFactory : Starting InstantiateExtensions");

            /// <summary>Composition Container.</summary>

            composableExtensions = new ComposableExtensions();

            DirectoryCatalog directoryCatalog;

            package.PackageLog.Log($"OpenStrata : ImportPackageStrataExtensionsFactory : InstantiateExtensions : Current package location is {package.CurrentPackageLocation}");


            directoryCatalog = new DirectoryCatalog(package.CurrentPackageLocation);


            if (directoryCatalog != null)
            {
                package.PackageLog.Log($"OpenStrata : ImportPackageStrataExtensionsFactory : InstantiateExtensions : Creating CompositionContainer");
                ImportPackageStrataExtensionsContainer = new CompositionContainer((ComposablePartCatalog)directoryCatalog,
                    Array.Empty<ExportProvider>());
                try
                {
                    package.PackageLog.Log($"OpenStrata : ImportPackageStrataExtensionsFactory : InstantiateExtensions : Attempting to Compose Extensions");
                    ImportPackageStrataExtensionsContainer.ComposeParts((object)composableExtensions);
                }
                catch (Exception ex)
                {
                    package.PackageLog.Log(
                        "ImportPackageStrataExtensionsFactory.InstantiateExtensions - ComposeParts Exception : " + ex.Message,
                        TraceEventType.Error, ex);
                }
            }
            else
            {
                package.PackageLog.Log($"OpenStrata : ImportPackageStrataExtensionsFactory : InstantiateExtensions : Directory Catalog Is Null");
            }


            var composedExtensions = new List<IImportPackageStrataExtension>();

            foreach(IImportPackageStrataExtension extension in composableExtensions.FeatureExtensionList)
            {
                package.PackageLog.Log($"OpenStrata : Loaded Feature Extension {extension.GetType().FullName}");
                composedExtensions.Add(extension);
            }

            foreach (IImportPackageStrataExtension extension in composableExtensions.StratiExtensionList)
            {
                package.PackageLog.Log($"OpenStrata : Loaded Strati Extension {extension.GetType().FullName}");
                composedExtensions.Add(extension);
            }

            package.PackageLog.Log($"OpenStrata : ImportPackageStrataExtensionsFactory : InstantiateExtensions : {composedExtensions.Count} exensions were found.");

            return composedExtensions;

        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).


                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ImportPackageStrataExtensionsFactory() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
