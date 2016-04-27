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
                return DocumentTitles.Count();
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
                        return false;
                    }
                    return true;
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
                    return null;
                }

                return p.Select(x => x.Title).ToList();
            }
        }

        public bool DocumentExists(string Name)
        {
            if (DocumentTitles.Select(x => x == Name).Any())
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
                    tmpVariable.Value = ObjectXmlSerializer.ObjectToXMLGeneric<object>(variable.Value);
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
        private DocumentDataManager documentDataManager;



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



        public void NewDocument(string DocumentTitle, string TemplateName)
        {
            if (DocumentExists(DocumentTitle))
            {
                throw new Exceptions.ObjectExistsException(DocumentTitle);
            }
            if (!DocumentTemplates.TemplateExists(TemplateName))
            {
                throw new Exceptions.ObjectDoesNotExistException(TemplateName);
            }

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
                    doc.Data.Add(tmpData);
                }
                SQLDB.SaveChanges();
            }
        }

        
    }
}
