using OpenStrata.Strati.Manifest.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest
{

    

    public class StratiSequenceFactory 
    {

        public delegate void LogMessage(string message);

        private LogMessage Log = LogMessageToConsole;

        private ImportStrataManifestXDocument StrataManifestXDoc;

        private ImportStrataManifestXElement ImportStrataManifest => StrataManifestXDoc.Root;

        private static void LogMessageToConsole (string message)
        {
            Console.WriteLine(message);
        }
  

        private StratiSequenceFactory(ImportStrataManifestXDocument strataManifestXDoc, LogMessage logger)
        {

            Log = logger;
            this.StrataManifestXDoc = strataManifestXDoc;

        }

        private List<string> StrataSequence { get; } = new List<string>();

        private void GenerateImportStrataStratiSequence()
        {

            Log($"Running GenerateImportStrataStratiSequence() for {StrataSequence.Count} sequence items.");

            ImportStrataManifest.StratiSequence.RemoveNodes();

            foreach (string uniqueName in StrataSequence)
            {
                Log($"Adding {uniqueName} to the sequence");
                ImportStrataManifest.StratiSequence.Add( new StratiSequenceXElement(uniqueName));
            }
        }

        private void DetermineStrataSequence()
        {

            var importStrata = ImportStrataManifest.ImportStrata.Elements("StratiManifest").ToArray();

            Log($"Running DetermineStrataSequence() of {importStrata.Count()} StratiManifest elements.");

            foreach (XElement stratiManifest in importStrata)
            {

                DetermineStrataSequence(stratiManifest);

                Log($"Continuing DetermineStrataSequence() of {importStrata.Count()} StratiManifest elements.");

            }
        }


        private void DetermineStrataSequence (XElement stratiManifest)
        {
            var uniqueName = stratiManifest.Attribute("UniqueName").Value;

            Log($"Running DetermineStrataSequence(StratiManifest: {uniqueName}).");

            if (StrataSequence.Contains(uniqueName))
            {
                Log($"Sequence for StratiManifest {uniqueName} has previously been determined.  No further processing for this item is required.");
                return;
            }

            foreach (XElement strati in stratiManifest.Elements("Strata").Elements("Strati").ToArray())
            {
                DetermineStrataSequenceFromStratiElement(strati);
            }

            Log($"Sequence for {uniqueName} has been determined.");
            StrataSequence.Add(uniqueName);

        }

        private void DetermineStrataSequenceFromStratiElement(XElement stratiXElement)
        {

            var uniqueName = stratiXElement.Attribute("UniqueName").Value;

            // If the strati has already been added to the sequence, no further processing is required.
            Log($"Running DetermineStrataSequenceFromStratiElement(StratiElement: {uniqueName}).");

            if (StrataSequence.Contains(uniqueName))
            {
                Log($"Sequence for StratiElement {uniqueName} has previously been determined.  No further processing for this item is required.");
                return;
            }

            var stratiManifest = ImportStrataManifest.ImportStrata
                   .Elements("StratiManifest")
                   .Where(sm => sm.Attribute("UniqueName").Value == uniqueName)
                   .FirstOrDefault();

            if (stratiManifest != null)
            {
                DetermineStrataSequence(stratiManifest);
            }
            else
            {
                Log($"Unable to locate StratiManifest with the unique name \"{uniqueName}\"");
            }
        }


        public static void Generate(ImportStrataManifestXDocument strataManifestXDoc, LogMessage logger)
        {
            var factory = new StratiSequenceFactory(strataManifestXDoc,logger);
            factory.DetermineStrataSequence();
            factory.GenerateImportStrataStratiSequence();
        }



    }
}
