/* ====================================================================
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for Additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
==================================================================== */
namespace NPOI.XWPF.UserModel
{
    using System;
    using NUnit.Framework;

    using NPOI.XWPF;
    using NPOI.OpenXmlFormats.Wordprocessing;
    using System.Collections.Generic;


    /**
     * Tests for XWPF Run
     */
    [TestFixture]
    public class TestXWPFTable
    {

        protected void SetUp()
        {
            /*
              XWPFDocument doc = new XWPFDocument();
              p = doc.CreateParagraph();

              this.ctRun = CTR.Factory.NewInstance();
           */
        }

        [Test]
        public void TestConstructor()
        {
            XWPFDocument doc = new XWPFDocument();
            CT_Tbl ctTable = new CT_Tbl();
            XWPFTable xtab = new XWPFTable(ctTable, doc);
            Assert.IsNotNull(xtab);
            Assert.AreEqual(1, ctTable.SizeOfTrArray());
            Assert.AreEqual(1, ctTable.GetTrArray(0).SizeOfTcArray());
            Assert.IsNotNull(ctTable.GetTrArray(0).GetTcArray(0).GetPArray(0));

            ctTable = new CT_Tbl();
            xtab = new XWPFTable(ctTable, doc, 3, 2);
            Assert.IsNotNull(xtab);
            Assert.AreEqual(3, ctTable.SizeOfTrArray());
            Assert.AreEqual(2, ctTable.GetTrArray(0).SizeOfTcArray());
            Assert.IsNotNull(ctTable.GetTrArray(0).GetTcArray(0).GetPArray(0));
        }


        [Test]
        public void TestGetText()
        {
            XWPFDocument doc = new XWPFDocument();
            CT_Tbl table = new CT_Tbl();
            CT_Row row = table.AddNewTr();
            CT_Tc cell = row.AddNewTc();
            CT_P paragraph = cell.AddNewP();
            CT_R run = paragraph.AddNewR();
            CT_Text text = run.AddNewT();
            text.Value = ("finally I can Write!");

            XWPFTable xtab = new XWPFTable(table, doc);
            Assert.AreEqual("finally I can Write!\n", xtab.GetText());
        }


        [Test]
        public void TestCreateRow()
        {
            XWPFDocument doc = new XWPFDocument();

            CT_Tbl table = new CT_Tbl();
            CT_Row r1 = table.AddNewTr();
            r1.AddNewTc().AddNewP();
            r1.AddNewTc().AddNewP();
            CT_Row r2 = table.AddNewTr();
            r2.AddNewTc().AddNewP();
            r2.AddNewTc().AddNewP();
            CT_Row r3 = table.AddNewTr();
            r3.AddNewTc().AddNewP();
            r3.AddNewTc().AddNewP();

            XWPFTable xtab = new XWPFTable(table, doc);
            Assert.AreEqual(3, xtab.GetNumberOfRows());
            Assert.IsNotNull(xtab.GetRow(2));

            //add a new row
            xtab.CreateRow();
            Assert.AreEqual(4, xtab.GetNumberOfRows());

            //check number of cols
            Assert.AreEqual(2, table.GetTrArray(0).SizeOfTcArray());

            //check creation of first row
            xtab = new XWPFTable(new CT_Tbl(), doc);
            Assert.AreEqual(1, xtab.GetCTTbl().GetTrArray(0).SizeOfTcArray());
        }

        [Test]
        public void TestSetGetWidth()
        {
            XWPFDocument doc = new XWPFDocument();

            CT_Tbl table = new CT_Tbl();
            table.AddNewTblPr().AddNewTblW().w = "1000";

            XWPFTable xtab = new XWPFTable(table, doc);

            Assert.AreEqual(1000, xtab.GetWidth());

            xtab.SetWidth(100);
            Assert.AreEqual(100, int.Parse(table.tblPr.tblW.w));
        }
        [Test]
        public void TestSetGetHeight()
        {
            XWPFDocument doc = new XWPFDocument();

            CT_Tbl table = new CT_Tbl();

            XWPFTable xtab = new XWPFTable(table, doc);
            XWPFTableRow row = xtab.CreateRow();
            row.SetHeight(20);
            Assert.AreEqual(20, row.GetHeight());
        }
         [Ignore]
        public void TestSetGetMargins()
        {
            // instantiate the following class so it'll Get picked up by
            // the XmlBean process and Added to the jar file. it's required
            // for the following XWPFTable methods.
            CT_TblCellMar ctm = new CT_TblCellMar();
            Assert.IsNotNull(ctm);
            // create a table
            XWPFDocument doc = new XWPFDocument();
            CT_Tbl ctTable = new CT_Tbl();
            XWPFTable table = new XWPFTable(ctTable, doc);
            // Set margins
            table.SetCellMargins(50, 50, 250, 450);
            // Get margin components
            int t = table.GetCellMarginTop();
            Assert.AreEqual(50, t);
            int l = table.GetCellMarginLeft();
            Assert.AreEqual(50, l);
            int b = table.GetCellMarginBottom();
            Assert.AreEqual(250, b);
            int r = table.GetCellMarginRight();
            Assert.AreEqual(450, r);
        }
        [Ignore]
        public void TestSetGetHBorders()
        {
            // instantiate the following classes so they'll Get picked up by
            // the XmlBean process and Added to the jar file. they are required
            // for the following XWPFTable methods.
            CT_TblBorders cttb = new CT_TblBorders();
            Assert.IsNotNull(cttb);
            ST_Border stb = new ST_Border();
            Assert.IsNotNull(stb);
            // create a table
            XWPFDocument doc = new XWPFDocument();
            CT_Tbl ctTable = new CT_Tbl();
            XWPFTable table = new XWPFTable(ctTable, doc);
            // Set inside horizontal border
            table.SetInsideHBorder(NPOI.XWPF.UserModel.XWPFTable.XWPFBorderType.SINGLE, 4, 0, "FF0000");
            // Get inside horizontal border components
            int s = table.GetInsideHBorderSize();
            Assert.AreEqual(4, s);
            int sp = table.GetInsideHBorderSpace();
            Assert.AreEqual(0, sp);
            String clr = table.GetInsideHBorderColor();
            Assert.AreEqual("FF0000", clr);
            NPOI.XWPF.UserModel.XWPFTable.XWPFBorderType bt = table.GetInsideHBorderType();
            Assert.AreEqual(NPOI.XWPF.UserModel.XWPFTable.XWPFBorderType.SINGLE, bt);
        }
         [Ignore]
        public void TestSetGetVBorders()
        {
            // create a table
            XWPFDocument doc = new XWPFDocument();
            CT_Tbl ctTable = new CT_Tbl();
            XWPFTable table = new XWPFTable(ctTable, doc);
            // Set inside vertical border
            table.SetInsideVBorder(NPOI.XWPF.UserModel.XWPFTable.XWPFBorderType.DOUBLE, 4, 0, "00FF00");
            // Get inside vertical border components
            NPOI.XWPF.UserModel.XWPFTable.XWPFBorderType bt = table.GetInsideVBorderType();
            Assert.AreEqual(NPOI.XWPF.UserModel.XWPFTable.XWPFBorderType.DOUBLE, bt);
            int sz = table.GetInsideVBorderSize();
            Assert.AreEqual(4, sz);
            int sp = table.GetInsideVBorderSpace();
            Assert.AreEqual(0, sp);
            String clr = table.GetInsideVBorderColor();
            Assert.AreEqual("00FF00", clr);
        }
         [Ignore]
        public void TestSetGetRowBandSize()
        {
            XWPFDocument doc = new XWPFDocument();
            CT_Tbl ctTable = new CT_Tbl();
            XWPFTable table = new XWPFTable(ctTable, doc);
            table.SetRowBandSize(12);
            int sz = table.GetRowBandSize();
            Assert.AreEqual(12, sz);
        }
         [Ignore]
        public void TestSetGetColBandSize()
        {
            XWPFDocument doc = new XWPFDocument();
            CT_Tbl ctTable = new CT_Tbl();
            XWPFTable table = new XWPFTable(ctTable, doc);
            table.SetColBandSize(16);
            int sz = table.GetColBandSize();
            Assert.AreEqual(16, sz);
        }
         [Ignore]
        public void TestCreateTable()
        {
            // open an empty document
            XWPFDocument doc = XWPFTestDataSamples.OpenSampleDocument("sample.docx");

            // create a table with 5 rows and 7 coloumns
            int noRows = 5;
            int noCols = 7;
            XWPFTable table = doc.CreateTable(noRows, noCols);

            // assert the table is empty
            List<XWPFTableRow> rows = table.GetRows();
            Assert.AreEqual(noRows, rows.Count, "Table has less rows than requested.");
            foreach (XWPFTableRow xwpfRow in rows)
            {
                Assert.IsNotNull(xwpfRow);
                for (int i = 0; i < 7; i++)
                {
                    XWPFTableCell xwpfCell = xwpfRow.GetCell(i);
                    Assert.IsNotNull(xwpfCell);
                    Assert.AreEqual(1, xwpfCell.Paragraphs.Count, "Empty cells should not have one paragraph.");
                    xwpfCell = xwpfRow.GetCell(i);
                    Assert.AreEqual(1, xwpfCell.Paragraphs.Count, "Calling 'getCell' must not modify cells content.");
                }
            }
            doc.Package.Revert();
        }
    }
}