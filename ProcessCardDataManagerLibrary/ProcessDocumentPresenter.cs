using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProcessCardDataManagerLibrary
{
    public class ProcessDocumentPresenter
    {
        /// <summary>
        /// A List of Process Card Templates in Database.
        /// </summary>
        public List<string> TemplateTypes
        {
            get
            {
                return Templates.TemplateTypes;
            }
        }

        /// <summary>
        /// A List of Process Documents in Database.
        /// </summary>
        public List<string> DocumentTitles
        {
            get
            {
                return Documents.DocumentTitles;
            }
        }
        
        private DocumentManager documents;
        public DocumentManager Documents
        {
            get
            {
                if (documents == null)
                {
                    documents = new DocumentManager(Templates);
                }
                return documents;
            }
        }

        private TemplateManager templates;
        public TemplateManager Templates
        {
            get
            {
                if (templates == null)
                {
                    templates = new TemplateManager();
                }
                return templates;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProcessDocumentPresenter()
        {
        }
    }
}
