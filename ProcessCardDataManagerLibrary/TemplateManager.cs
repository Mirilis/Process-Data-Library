using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary
{
    public class TemplateManager
    {
        /// <summary>
        /// Private storage for Document List
        /// </summary>
        private List<string> templateTypes;

        private int currentTemplateTypeCount
        {
            get
            {
                return TemplateTypes.Count();
            }
        }

        private bool TemplateListHasChanged
        {
            get
            {
                using (var SQLDB = new ProcessDocumentDataContainer())
                {
                    var actualTemplateTypeCount = SQLDB.Templates.Count();
                    if (currentTemplateTypeCount != actualTemplateTypeCount)
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
        public List<string> TemplateTypes
        {
            get
            {
                if (templateTypes == null || TemplateListHasChanged)
                {
                    templateTypes = GetTemplatesFromDatabase();
                }

                return templateTypes;
            }
        }

        public bool TemplateExists(string Template)
        {
            if (TemplateTypes.Select(x => x == Template).Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates New Template
        /// </summary>
        /// <param name="Type">The Name of the Template to Add.</param>
        /// <param name="DataTemplateValues">The Dictionary of DataValues to apply to Template.</param>
        public void CreateNewTemplate(string Type, DataTemplateDictionary DataTemplateValues)
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {

                var templateName = new Template();
                templateName.TemplateType = Type;
                SQLDB.AddToTemplates(templateName);
                foreach (var dataValue in DataTemplateValues)
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

                return p.Select(x => x.TemplateType).ToList();
            }
        }
    }
}
