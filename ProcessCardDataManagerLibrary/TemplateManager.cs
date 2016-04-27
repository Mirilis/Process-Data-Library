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
                return templateTypes.Count();
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
                        return true;
                    }
                    return false;
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
                if (TemplateListHasChanged)
                {
                    templateTypes = GetTemplatesFromDatabase();
                }

                return templateTypes;
            }
        }

        public bool TemplateExists(string Template)
        {
            if (TemplateTypes.Where(x => x == Template).Any())
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
                SQLDB.SaveChanges();
                Console.WriteLine(SQLDB.Templates.Count().ToString());
                AddDataTemplateValues(templateName.TemplateType, DataTemplateValues);
            }
        }

        public void AddDataTemplateValues(string templateName, DataTemplateDictionary DataTemplateValues)
        {
            var template = GetTemplate(templateName);
            using (var context = new ProcessDocumentDataContainer())
            {
                context.Templates.Attach(template);
                foreach (var dataValue in DataTemplateValues)
                {

                    var templateData = new DataTemplate();
                    templateData.Template = template;
                    templateData.Name = dataValue.Key;
                    templateData.Type = dataValue.Value.CurrentType.ToString();
                    context.DataTemplates.AddObject(templateData);
                }
            context.SaveChanges();
            }
        }

        public void CreateNewTemplate(string Type)
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                var templateName = new Template();
                templateName.TemplateType = Type;
                SQLDB.AddToTemplates(templateName);
                SQLDB.SaveChanges();
            }
        }


        public TemplateManager()
        {
            templateTypes = GetTemplatesFromDatabase();
        }

        public Template GetTemplate(string TemplateType)
        {
            using (var SQLDB = new ProcessDocumentDataContainer())
            {
                var templateRequested = SQLDB.Templates.Where(x => x.TemplateType == TemplateType);
                if (templateRequested.Any())
                {
                    var template = templateRequested.First();
                    SQLDB.Detach(template);
                    return template;
                }
                else
                {
                    throw new Exceptions.ObjectDoesNotExistException(TemplateType);
                }
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
                    var dtd = new DataTemplateDictionary() 
                    { 
                        {"Name",new DataType("String")},
                        {"Number",new DataType("Int")},
                        {"Double",new DataType("Double")}
                    };
                    CreateNewTemplate("Blank", dtd);
                    
                }

                return p.Select(x => x.TemplateType).ToList();
            }
        }
    }
}
