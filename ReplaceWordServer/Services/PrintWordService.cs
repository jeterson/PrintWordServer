using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReplaceWordServer.Services
{
    public class PrintWordService
    {

        public void PrintDoc(String input, String printerName = null)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application() { Visible = false };
            Microsoft.Office.Interop.Word.Document doc = null;
            object saveChanges = false;

            object oMissing = System.Reflection.Missing.Value;
            object varFalse = false;
            object varTrue = true;
            object missing = System.Type.Missing;

            try
            {


                object fileName = input;
                doc = word.Documents.Open(FileName: fileName, ReadOnly: true, Visible: false);


                if (printerName != null)
                    word.Application.ActivePrinter = printerName;


                doc.Activate();
                doc.PrintOut();
                while (word.BackgroundPrintingStatus > 0)
                {
                    Thread.Sleep(500);
                }
                CloseDoc(word, doc);
            }
            catch (Exception ex)
            {
                CloseDoc(word, doc);
                throw ex;
            }
           
        }

        private void CloseDoc(Microsoft.Office.Interop.Word.Application app, Microsoft.Office.Interop.Word.Document doc)
        {
            Object saveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            object missing = System.Type.Missing;

            if (doc != null)
            {
                //((Microsoft.Office.Interop.Word._Document)doc).Close(ref saveChanges, ref missing, ref missing);
                //m_wordDoc = null;
                ((Microsoft.Office.Interop.Word._Document)app.ActiveDocument).Close(saveChanges, missing, missing);
            }
            if (app != null)
            {
                ((Microsoft.Office.Interop.Word._Application)app).Quit();
                //m_wordApp = null;
            }
        }

    }
}
