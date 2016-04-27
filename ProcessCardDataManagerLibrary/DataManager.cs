using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProcessCardDataManagerLibrary
{
    public class DataManager
    {
        /// <summary>
        /// A List of Process Card Templates in Database.
        /// </summary>
        public List<string> Templates { get; private set; }

        /// <summary>
        /// A List of Process Documents in Database.
        /// </summary>
        public List<string> ProcessDocuments { get; private set; }
        
        /// <summary>
        /// Creates New Template
        /// </summary>
        /// <param name="Type">The Name of the Template to Add.</param>
        /// <param name="DataValues">The Dictionary of DataValues to apply to Template.</param>
        public void CreateNewTemplate(string Type, DataTemplateDictionary DataValues)
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                
                var templateName = new Template();
                templateName.Type = Type;
                SQLDB.AddToTemplates(templateName);
                foreach (var dataValue in DataValues)
                {
                    var templateData = new DataTemplate();
                    templateData.Template = templateName;
                    templateData.Name = dataValue.Key;
                    templateData.Type = dataValue.Value.ToString();
                    SQLDB.AddToDataTemplates(templateData);
                }
                SQLDB.SaveChanges();
            }
        }

        public void AddDocumentVariables(string DocumentName, List<DataValues> Variables)
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                if (!DocumentExists(DocumentName))
                {
                    throw new Exceptions.ObjectDoesNotExistException(DocumentName);
                }
                var tmpDocument = SQLDB.Documents.Where(x => x.Name == DocumentName).FirstOrDefault();
                foreach (var variable in Variables)
                {
                    var tmpVariable = tmpDocument.Data.Where(x => x.Template.Name == variable.Variable).First();
                    tmpVariable.Value = ObjectXmlSerializer.ObjectToXMLGeneric<object>(variable.Value);
                }
            }
        }

        
        private bool DocumentExists(string Name)
        {
            if (ProcessDocuments.Select(x => x == Name).Any())
            {
                return true;
            }
            return false;
        }
        private bool TemplateExists(string Template)
        {
            if (Templates.Select(x => x == Template).Any())
            {
                return true;
            }
            return false;
        }

        public void CreateNewProcessDocument(string DocumentName, string TemplateName)
        {
            if (DocumentExists(DocumentName))
            {
                throw new Exceptions.ObjectExistsException(DocumentName);
            }
            if (!TemplateExists(TemplateName))
            {
                throw new Exceptions.ObjectDoesNotExistException(TemplateName);
            }
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                var tmpTemplate = SQLDB.Templates.Where(x => x.Type == TemplateName).FirstOrDefault();
                var doc = new Document();
                doc.Name = DocumentName;
                doc.Template = tmpTemplate;
                foreach (var DataTemplate in tmpTemplate.DataTemplate)
                {
                    var tmpData = new Data();
                    tmpData.Template = DataTemplate;
                    var tmpRevision = new Revision();
                    tmpRevision.Author = "None";
                    tmpRevision.Date = DateTime.Now;
                    tmpData.Revision = tmpRevision;
                    doc.Data.Add(tmpData);

                }
                SQLDB.SaveChanges();
                RefreshDataFromDatabase();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DataManager()
        {
            RefreshDataFromDatabase();
        }

        /// <summary>
        /// Refresh Data From Database.
        /// </summary>
        private void RefreshDataFromDatabase()
        {
            Templates = GetTemplatesFromDatabase();
            ProcessDocuments = GetProcessCardsFromDatabase();
        }

        /// <summary>
        /// Gets Process Card List from Database.
        /// </summary>
        /// <returns>List of Process Card Names.</returns>
        private List<string> GetProcessCardsFromDatabase()
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                var p = SQLDB.Documents;
                if (!p.Any())
                {
                    return null;
                }

                return p.Select(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// Gets Template List from Database.
        /// </summary>
        /// <returns>List of Templates.</returns>
        private List<string> GetTemplatesFromDatabase()
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                var p = SQLDB.Templates;
                if (!p.Any())
                {
                    return null;
                }
                
                return p.Select(x => x.Type).ToList();
            }
        }
    }
}
