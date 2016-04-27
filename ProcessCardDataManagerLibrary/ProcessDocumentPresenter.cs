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
        public List<string> Templates
        {
            get
            {
                return TemplateManager.TemplateTypes;
            }
        }

        /// <summary>
        /// A List of Process Documents in Database.
        /// </summary>
        public List<string> DocumentTitles
        {
            get
            {
                return DocumentManager.DocumentTitles;
            }
        }
        
        private DocumentManager documentManager;
        private DocumentManager DocumentManager
        {
            get
            {
                if (documentManager == null)
                {
                    documentManager = new DocumentManager(TemplateManager);
                }
                return documentManager;
            }
        }

        private TemplateManager templateManager;
        private TemplateManager TemplateManager
        {
            get
            {
                if (templateManager == null)
                {
                    templateManager = new TemplateManager();
                }
                return templateManager;
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
