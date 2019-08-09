using ReplaceWordServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Services
{
    public class ReplaceWordService
    {
        public String Replace(PrintModel printModel)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            String filePath = String.Format("{0}\\{1}", printModel.BasePath, printModel.InputFileName);
            String output = String.Format("{0}\\{1}\\{2}", printModel.BasePath, printModel.BasePathReplaced, printModel.InputFileName);

           // CreateDirIfNotExists(printModel.BasePath);
            CreateDirIfNotExists(string.Format("{0}\\{1}", printModel.BasePath, printModel.BasePathReplaced));

            object missing = System.Type.Missing;
            object fileName = filePath;
            object fileNameOutput = output;


            Microsoft.Office.Interop.Word.Document doc = word.Documents.Add(ref fileName, ref missing, ref missing, ref missing);

            try
            {
                doc.Activate();
                foreach (Microsoft.Office.Interop.Word.Range tmpRange in doc.StoryRanges)
                {
                    foreach (ReplaceModel replace in printModel.Replaces)
                    {
                        tmpRange.Find.Text = printModel.KeyPrefix + replace.Key;
                        tmpRange.Find.Replacement.Text = replace.Value;

                        tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                        object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

                        tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                             ref missing, ref missing, ref missing, ref missing,
                             ref missing, ref missing, ref missing, ref replaceAll,
                             ref missing, ref missing, ref missing, ref missing);
                    }
                }

                DeleteIfExists(output);
                doc.SaveAs2(ref fileNameOutput);
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseDoc(word, doc);
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

        private void CreateDirIfNotExists(String dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        private void DeleteIfExists(String file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);

            }
        }

    }
}
