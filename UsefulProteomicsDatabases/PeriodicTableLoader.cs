// Copyright 2016 Stefan Solntsev
//
// This file (PeriodicTable.cs) is part of UsefulProteomicsDatabases.
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

using Chemistry;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace UsefulProteomicsDatabases
{
    /// <summary>
    /// The Periodic Table of Elements.
    /// </summary>
    public static class PeriodicTableLoader
    {
        public static void Load(string elementLocation)
        {
            if (PeriodicTable.Count() > 0)
                return;
            using (StreamReader sr = new StreamReader(elementLocation))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadLine();
                while (!line.Contains("Atomic Number"))
                {
                    line = sr.ReadLine();
                }
                var prevAtomicNumber = -1;
                Element element = null;
                do
                {
                    //Console.WriteLine(line);
                    int atomicNumber = Convert.ToInt32(Regex.Match(line, @"\d+").Value);
                    //Console.WriteLine("atomicNumber = " + atomicNumber);

                    line = sr.ReadLine();
                    //Console.WriteLine(line);
                    string atomicSymbol = Regex.Match(line, @"[A-Za-z]+$").Value;
                    //Console.WriteLine("atomicSymbol = " + atomicSymbol);

                    line = sr.ReadLine();
                    //Console.WriteLine(line);
                    int massNumber = Convert.ToInt32(Regex.Match(line, @"\d+").Value);
                    //Console.WriteLine("massNumber = " + massNumber);

                    line = sr.ReadLine();
                    //Console.WriteLine(line);
                    double atomicMass = Convert.ToDouble(Regex.Match(line, @"[\d\.]+").Value);
                    //Console.WriteLine("atomicMass = " + atomicMass);

                    line = sr.ReadLine();
                    //Console.WriteLine(line);
                    double abundance = -1;
                    if (Regex.Match(line, @"[\d\.]+").Success == true)
                    {
                        abundance = Convert.ToDouble(Regex.Match(line, @"[\d\.]+").Value);
                        //Console.WriteLine("abundance = " + abundance);
                    }
                    else
                    {
                        line = sr.ReadLine();
                        line = sr.ReadLine();
                        line = sr.ReadLine();
                        line = sr.ReadLine();
                        continue;
                    }

                    line = sr.ReadLine();
                    //Console.WriteLine(line);
                    double averageMass = -1;
                    if (Regex.Match(line, @"\[").Success == true)
                    {
                        double averageMass1 = Convert.ToDouble(Regex.Match(line, @"(?<=\[)[\d\.]+").Value);
                        var kkajsdf = Regex.Match(line, @"(?<=,)[\d\.]+").Value;
                        double averageMass2 = Convert.ToDouble(kkajsdf);
                        averageMass = (averageMass1 + averageMass2) / 2;
                    }
                    else
                        averageMass = Convert.ToDouble(Regex.Match(line, @"[\d\.]+").Value);
                    //Console.WriteLine("averageMass = " + averageMass);

                    
                    if (atomicNumber != prevAtomicNumber)
                    {
                        // New Element!
                        element = new Element(atomicSymbol, atomicNumber, averageMass);
                        PeriodicTable.Add(element.AtomicSymbol, element);
                    }
                    //Console.WriteLine("Trying to add isotope with mass number " + massNumber + " to element " + element);
                    element.AddIsotope(massNumber, atomicMass, abundance);

                    line = sr.ReadLine();
                    line = sr.ReadLine();
                    line = sr.ReadLine();
                    prevAtomicNumber = atomicNumber;
                } while (line.Contains("Atomic Number"));
            }

        }
    }
}