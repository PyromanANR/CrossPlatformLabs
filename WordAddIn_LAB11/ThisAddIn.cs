using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace WordAddIn_LAB11
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {

        }

        private async Task<string> ReadFileContentAsync(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error reading file: " + ex.Message);
                return string.Empty;
            }
        }



        public async void LoadLabContent(int labNumber)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"INPUTLab{labNumber}.txt");
            var content = await UploadFileAndGetResponse(filePath, labNumber);
            string inputData = await ReadFileContentAsync(filePath);
            InsertContentIntoWord(inputData, content, labNumber);
        }

        private async Task<string> UploadFileAndGetResponse(string filePath, int labNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var formData = new MultipartFormDataContent();
                    var fileContent = new StreamContent(File.OpenRead(filePath));
                    formData.Add(fileContent, "inputFile", Path.GetFileName(filePath));  
                    formData.Add(new StringContent(labNumber.ToString()), "labNumber");  
                    var response = await client.PostAsync("http://localhost:7253/Lab/ProcessLab", formData);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(result);
                        return jsonResponse.output; 
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Error processing the file.");
                        return string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
                    return string.Empty;
                }
            }
        }

        private void InsertContentIntoWord(string inputData, string content, int labNumber)
        {
            if (string.IsNullOrEmpty(inputData) && string.IsNullOrEmpty(content)) return;

            string inputLabel = $"\nInput for Lab {labNumber}:\n";
            string resultLabel = $"\nResultProcessLab{labNumber}: ";

            var wordApp = Globals.ThisAddIn.Application;
            var doc = wordApp.ActiveDocument;

            wordApp.Selection.TypeText(inputLabel);
            wordApp.Selection.TypeText(inputData);  

            wordApp.Selection.TypeText(resultLabel);
            wordApp.Selection.TypeText(content); 
        }





        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
