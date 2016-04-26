using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessCardDataManagerLibrary
{
    public class DataManager
    {
        public void CreateNewTemplate(string Type, DataTemplateDictionary TypeToAdd)
        {
            using (var SQLDB = new ProcessCardDataFrameworkContainer())
            {
                var templateName = new Template();
                templateName.Type = Type;
                SQLDB.AddToTemplates(templateName);
                foreach (var item in TypeToAdd)
                {
                    var templateData = new DataTemplate();
                    templateData.Template = templateName;
                    templateData.Name = item.Key;
                    templateData.Type = item.Value.ToString();
                    SQLDB.AddToDataTemplates(templateData);
                }
                SQLDB.SaveChanges();
            }
        }

        public List<Template> GetTempalateFromDatabase(string ProcessCardName)
        {
            using (var SQLDB = new ProcessCardDataFrameworkContainer())
            {
                var p = SQLDB.Templates;
                return p.ToList();
            }
        }
    }
}
