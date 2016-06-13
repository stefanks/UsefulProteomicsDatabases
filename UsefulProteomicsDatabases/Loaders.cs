// Copyright 2016 Stefan Solntsev
//
// This file (Loaders.cs) is part of UsefulProteomicsDatabases.
// 
// UsefulProteomicsDatabases is free software: you can redistribute it and/or modify it
// under the terms of the GNU Lesser General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UsefulProteomicsDatabases is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public
// License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with UsefulProteomicsDatabases. If not, see <http://www.gnu.org/licenses/>.

using Proteomics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace UsefulProteomicsDatabases
{
    public class Loaders
    {
        public static string unimodLocation;
        public static string psimodLocation;
        public static string elementLocation;
        public static string uniprotLocation;

        static bool FilesAreEqual_Hash(string first, string second)
        {
            var a = File.Open(first, FileMode.Open, FileAccess.Read);
            var b = File.Open(second, FileMode.Open, FileAccess.Read);
            byte[] firstHash = MD5.Create().ComputeHash(a);
            byte[] secondHash = MD5.Create().ComputeHash(b);
            a.Close();
            b.Close();
            for (int i = 0; i < firstHash.Length; i++)
            {
                if (firstHash[i] != secondHash[i])
                    return false;
            }
            return true;
        }

        public static void UpdateUniprot()
        {
            DownloadUniprot();
            if (!File.Exists(uniprotLocation))
            {
                Console.WriteLine("Uniprot database did not exist, writing to disk");
                File.Move(uniprotLocation + ".temp", uniprotLocation);
                return;
            }
            bool ye = FilesAreEqual_Hash(uniprotLocation + ".temp", uniprotLocation);
            if (ye)
            {
                Console.WriteLine("Uniprot database is up to date, doing nothing");
                File.Delete(uniprotLocation + ".temp");
            }
            else
            {
                Console.WriteLine("Uniprot database updated, saving old version as backup");
                File.Move(uniprotLocation, uniprotLocation + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss"));
                File.Move(uniprotLocation + ".temp", uniprotLocation);
            }
        }

        public static void UpdateUnimod()
        {
            DownloadUnimod();
            if (!File.Exists(unimodLocation))
            {
                Console.WriteLine("Unimod database did not exist, writing to disk");
                File.Move(unimodLocation + ".temp", unimodLocation);
                return;
            }
            bool ye = FilesAreEqual_Hash(unimodLocation + ".temp", unimodLocation);
            if (ye)
            {
                Console.WriteLine("Unimod database is up to date, doing nothing");
                File.Delete(unimodLocation + ".temp");
            }
            else
            {
                Console.WriteLine("Unimod database updated, saving old version as backup");
                File.Move(unimodLocation, unimodLocation + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss"));
                File.Move(unimodLocation + ".temp", unimodLocation);
            }
        }

        public static void UpdatePsiMod()
        {
            DownloadPsiMod();
            if (!File.Exists(psimodLocation))
            {
                Console.WriteLine("PSI-MOD database did not exist, writing to disk");
                File.Move(psimodLocation + ".temp", psimodLocation);
                return;
            }
            if (FilesAreEqual_Hash(psimodLocation + ".temp", psimodLocation))
            {
                Console.WriteLine("PSI-MOD database is up to date, doing nothing");
                File.Delete(psimodLocation + ".temp");
            }
            else
            {
                Console.WriteLine("PSI-MOD database updated, saving old version as backup");
                File.Move(psimodLocation, psimodLocation + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss"));
                File.Move(psimodLocation + ".temp", psimodLocation);
            }
        }

        public static void UpdateElements()
        {
            DownloadElements();
            if (!File.Exists(elementLocation))
            {
                Console.WriteLine("Element database did not exist, writing to disk");
                File.Move(elementLocation + ".temp", elementLocation);
                return;
            }
            if (FilesAreEqual_Hash(elementLocation + ".temp", elementLocation))
            {
                Console.WriteLine("Element database is up to date, doing nothing");
                File.Delete(elementLocation + ".temp");
            }
            else
            {
                Console.WriteLine("Element database updated, saving old version as backup");
                File.Move(elementLocation, elementLocation + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss"));
                File.Move(elementLocation + ".temp", elementLocation);
            }
        }

        public static void LoadElements()
        {
            if (!File.Exists(elementLocation))
                UpdateElements();
            PeriodicTableLoader.Load(elementLocation);

        }

        public static unimod LoadUnimod()
        {
            var unimodSerializer = new XmlSerializer(typeof(unimod));

            if (!File.Exists(unimodLocation))
                UpdateUnimod();
            return unimodSerializer.Deserialize(new FileStream(unimodLocation, FileMode.Open)) as unimod;
        }


        public static obo LoadPsiMod()
        {
            var psimodSerializer = new XmlSerializer(typeof(obo));

            if (!File.Exists(psimodLocation))
                UpdatePsiMod();
            return psimodSerializer.Deserialize(new FileStream(psimodLocation, FileMode.Open)) as obo;
        }

        private static readonly Regex PSI_MOD_ACCESSION_NUMBER_REGEX = new Regex(@"(.+); (\d+)\.");

        public static Dictionary<int, ChemicalFormulaModification> LoadUniprot()
        {
            if (!File.Exists(uniprotLocation))
                UpdateUniprot();
            Dictionary<int, ChemicalFormulaModification> modifications = new Dictionary<int, ChemicalFormulaModification>();
            using (StreamReader uniprot_mods = new StreamReader(uniprotLocation))
            {
                string feature_type = null;
                int psimod_accession_number = 0;
                string chemicalFormulaLine = null;
                while (uniprot_mods.Peek() != -1)
                {
                    string line = uniprot_mods.ReadLine();
                    if (line.Length >= 2)
                    {
                        switch (line.Substring(0, 2))
                        {
                            case "FT":
                                feature_type = line.Substring(5);
                                break;
                            case "CF":
                                chemicalFormulaLine = line.Substring(5);
                                break;
                            case "DR":
                                if (line.Contains("PSI-MOD"))
                                    psimod_accession_number = int.Parse(PSI_MOD_ACCESSION_NUMBER_REGEX.Match(line.Substring(5)).Groups[2].Value);
                                break;
                            case "//":
                                // Only mod_res, not intrachain. 
                                //Console.WriteLine("Considering adding, feature_type = " + feature_type + " chemical formula = " + chemicalFormulaLine);
                                if (feature_type == "MOD_RES" && !String.IsNullOrEmpty(chemicalFormulaLine) && !modifications.ContainsKey(psimod_accession_number))
                                    modifications.Add(psimod_accession_number, new ChemicalFormulaModification(chemicalFormulaLine));
                                feature_type = null;
                                chemicalFormulaLine = null;
                                break;
                        }
                    }
                }
            }
            return modifications;
        }

        private static void DownloadPsiMod()
        {
            WebClient Client = new WebClient();
            Client.DownloadFile(URLs.psimodURI, psimodLocation + ".temp");
        }

        private static void DownloadUnimod()
        {
            WebClient Client = new WebClient();
            Client.DownloadFile(URLs.unimodURI, unimodLocation + ".temp");
        }

        private static void DownloadElements()
        {
            WebClient Client = new WebClient();
            Client.DownloadFile(URLs.elementURI, elementLocation + ".temp");
        }

        private static void DownloadUniprot()
        {
            WebClient Client = new WebClient();
            Client.DownloadFile(URLs.uniprotURI, uniprotLocation + ".temp");
        }
    }
}
