using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Collections.Generic;


namespace FilteredElements
{
    public class FilteredElements
    {
        //Retrieve all the doors in the document and return the list of door elements.
        public ICollection<Element> CreateLogicAndFilter(Autodesk.Revit.DB.Document document)
        {
            // Find all door instances in the project by finding all elements that both belong to the door 
            // category and are family instances.
            ElementClassFilter familyInstanceFilter = new ElementClassFilter(typeof(FamilyInstance));

            // Create a category filter for Doors
            ElementCategoryFilter doorsCategoryfilter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);

            // Create a logic And filter for all Door FamilyInstances
            LogicalAndFilter doorInstancesFilter = new LogicalAndFilter(familyInstanceFilter, doorsCategoryfilter);

            // Apply the filter to the elements in the active document
            FilteredElementCollector collector = new FilteredElementCollector(document);
            IList<Element> doors = collector.WherePasses(doorInstancesFilter).ToElements();

            return doors;
        }
    }
}
