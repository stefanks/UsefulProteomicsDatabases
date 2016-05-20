﻿// opyright 2016 Stefan Solntsev
// 
// This file (ChemicalFormula.cs) is part of Chemistry Library.
// 
// Chemistry Library is free software: you can redistribute it and/or modify it
// under the terms of the GNU Lesser General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Chemistry Library is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public
// License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with Chemistry Library. If not, see <http://www.gnu.org/licenses/>

using UsefulProteomicsDatabases;
using NUnit.Framework;
using System.IO;

namespace Test
{
    [TestFixture]
    public class DatabaseLoaderTests { 
    
        [Test]
        public void TestUpdateUnimod()
        {
            Loaders.unimodLocation = Path.Combine(TestContext.CurrentContext.TestDirectory, "unimod_tables.xml");
            Loaders.UpdateUnimod();
            Loaders.UpdateUnimod();
        }

        [Test]
        public void TestUpdatePsiMod()
        {
            Loaders.psimodLocation = Path.Combine(TestContext.CurrentContext.TestDirectory, "PSI-MOD.obo.xml");
            Loaders.UpdatePsiMod();
            Loaders.UpdatePsiMod();
        }

        [Test]
        public void TestUpdateElements()
        {
            Loaders.elementLocation = Path.Combine(TestContext.CurrentContext.TestDirectory, "elements.dat");
            Loaders.UpdateElements();
            Loaders.UpdateElements();
        }

    }
}