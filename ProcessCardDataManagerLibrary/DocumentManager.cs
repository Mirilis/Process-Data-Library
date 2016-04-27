using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary
{
    public class DocumentManager
    {
        /// <summary>
        /// Private storage for Document List
        /// </summary>
        private List<string> documentTitles;
        
        /// <summary>
        /// Current Count of Document Titles in Database.
        /// </summary>
        private int currentDocumentCount
        {
            get
            {
                return documentTitles.Count();
            }
        }

        /// <summary>
        /// Returns true if document list count does not match count in database.
        /// </summary>
        private bool documentListHasChanged
        {
            get
            {
                using (var SQLDB = new ProcessDocumentDataContainer())
                {
                    var actualDocumentCount = SQLDB.Documents.Count();
                    if (currentDocumentCount != actualDocumentCount)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// A List of Process Documents in Database.
        /// </summary>
        public List<string> DocumentTitles
        {
            get
            {
                if (documentTitles == null || documentListHasChanged)
                {
                    documentTitles = GetDocumentTitleList();
                }

                return documentTitles;
            }
        }

        /// <summary>
        /// Gets Process Card List from Database.
        /// </summary>
        /// <returns>List of Process Card Names.</returns>
        private List<string> GetDocumentTitleList()
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                var p = SQLDB.Documents;
                if (!p.Any())
                {
                    CreateBlankDocument("Blank", "Blank");
                }

                return p.Select(x => x.Title).ToList();
            }
        }

        public bool DocumentExists(string Name)
        {
            if (DocumentTitles.Where(x => x == Name).Any())
            {
                return true;
            }
            return false;
        }

        public void AddDocumentVariables(string DocumentTitle, List<DataValues> Variables)
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                if (!DocumentExists(DocumentTitle))
                {
                    throw new Exceptions.ObjectDoesNotExistException(DocumentTitle);
                }
                
                var tmpDocument = GetDocument(DocumentTitle);
                
                foreach (var variable in Variables)
                {
                    var tmpVariable = tmpDocument.Data.Where(x => x.Template.Name == variable.Variable).First();
                    tmpVariable.Value = XMLSerializer.ObjectToXML<object>(variable.Value);
                }
            }
        }

        public Document GetDocument(string Title)
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                var documentRequested = SQLDB.Documents.Where(x => x.Title == Title);
                if (documentRequested.Any())
                {
                    var document = documentRequested.First();
                    SQLDB.Detach(document);
                    return document;
                }
                else
                {
                    throw new Exceptions.ObjectDoesNotExistException(Title);
                }
            }
        }

        private TemplateManager DocumentTemplates;
        private DataManager documentDataManager;



        public DocumentManager(TemplateManager DocumentTemplates)
        {
            this.DocumentTemplates = DocumentTemplates;
        }

        public void CopyDocument(string DocumentTitle)
        {
            throw new NotImplementedException();
        }

        public void DeleteDocument(string DocumentTitle)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(string DocumentTitle)
        {
            throw new NotImplementedException();
        }



        public void CreateNewDocument(string DocumentTitle, string TemplateName)
        {
            if (DocumentExists(DocumentTitle))
            {
                throw new Exceptions.ObjectExistsException(DocumentTitle);
            }
            if (!DocumentTemplates.TemplateExists(TemplateName))
            {
                throw new Exceptions.ObjectDoesNotExistException(TemplateName);
            }

            CreateBlankDocument(DocumentTitle,TemplateName);
        }

        public void CreateBlankDocument(string DocumentTitle, string TemplateName)
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                var tmpTemplate = SQLDB.Templates.Where(x => x.TemplateType == TemplateName).FirstOrDefault();
                var doc = new Document();
                doc.Title = DocumentTitle;
                doc.Template = tmpTemplate;
                foreach (var DataTemplate in tmpTemplate.DataTemplate)
                {
                    var tmpData = new Data();
                    tmpData.Template = DataTemplate;
                    var tmpRevision = new Revision();
                    tmpRevision.Author = "None";
                    tmpRevision.Date = DateTime.Now;
                    tmpData.Revision = tmpRevision;
                    var t = new DataType(DataTemplate.Type);

                    tmpData.Value = SerializeDataValue(t.Blank);
                    doc.Data.Add(tmpData);
                }
                SQLDB.SaveChanges();
            }
        }
        private string SerializeDataValue(object ObjectToSerialize)
        {
            return XMLSerializer.ObjectToXML<object>(ObjectToSerialize);
        }
        
    }
}
