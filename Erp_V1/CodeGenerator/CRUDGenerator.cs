using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Erp_V1.CodeGenerator
{
    public class CRUDGenerator
    {
        private const string NamespaceBLL = "Erp_V1.BLL";
        private const string NamespaceDAL_DAO = "Erp_V1.DAL.DAO";
        private const string NamespaceDAL_DTO = "Erp_V1.DAL.DTO";
        private const string DefaultPrimaryKeyName = "ID"; 

        public string GenerateDTO(JsonObject entityDefinition)
        {
            if (!entityDefinition.TryGetPropertyValue("entityName", out var entityNameNode))
            {
                return "// Error: entityName not found in definition.";
            }
            string entityName = entityNameNode.ToString();

            StringBuilder dtoCode = new StringBuilder();
            dtoCode.AppendLine("using System;");
            dtoCode.AppendLine();
            dtoCode.AppendLine($"namespace {NamespaceDAL_DTO}");
            dtoCode.AppendLine("{");
            dtoCode.AppendLine($"\tpublic class {entityName}DetailDTO // Detail DTO for lists/details");
            dtoCode.AppendLine("\t{");

            if (entityDefinition.TryGetPropertyValue("properties", out var propertiesNode) && propertiesNode is JsonArray properties)
            {
                foreach (var propertyNode in properties)
                {
                    if (propertyNode is JsonObject property &&
                        property.TryGetPropertyValue("name", out var propertyNameNode) &&
                        property.TryGetPropertyValue("dataType", out var dataTypeNode))
                    {
                        string propertyName = propertyNameNode.ToString();
                        string dataType = dataTypeNode.ToString();
                        dtoCode.AppendLine($"\t\tpublic {dataType} {propertyName} {{ get; set; }}");
                    }
                    else
                    {
                        dtoCode.AppendLine("\t\t// Error: Invalid property definition.");
                    }
                }
            }
            else
            {
                dtoCode.AppendLine("\t\t// Error: Properties not found or invalid in definition.");
            }
            dtoCode.AppendLine("\t}"); 
            dtoCode.AppendLine();
            dtoCode.AppendLine($"\tpublic class {entityName}DTO // For Select dropdowns or combined data");
            dtoCode.AppendLine("\t{");
            dtoCode.AppendLine($"\t\tpublic List<{entityName}DetailDTO> {entityName}List {{ get; set; }}"); 
            
            dtoCode.AppendLine("\t}"); 
            dtoCode.AppendLine("}");
            return dtoCode.ToString();
        }


        public string GenerateDAOInterface(JsonObject entityDefinition)
        {
            if (!entityDefinition.TryGetPropertyValue("entityName", out var entityNameNode))
            {
                return "// Error: entityName not found in definition.";
            }
            string entityName = entityNameNode.ToString();

            StringBuilder daoInterfaceCode = new StringBuilder();
            daoInterfaceCode.AppendLine("using System.Collections.Generic;");
            daoInterfaceCode.AppendLine($"using {NamespaceDAL_DTO};");
            daoInterfaceCode.AppendLine($"using {NamespaceDAL_DAO}; // Assuming base IDAO is in DAO namespace");
            daoInterfaceCode.AppendLine();
            daoInterfaceCode.AppendLine($"namespace {NamespaceDAL_DAO}");
            daoInterfaceCode.AppendLine("{");
            daoInterfaceCode.AppendLine($"\tpublic interface I{entityName}DAO : IDAO<{entityName.ToUpper()}, {entityName}DetailDTO>"); 
            daoInterfaceCode.AppendLine("\t{");
            daoInterfaceCode.AppendLine("\t\t// Add specific DAO methods if needed beyond standard CRUD");
            daoInterfaceCode.AppendLine("\t}");
            daoInterfaceCode.AppendLine("}");
            return daoInterfaceCode.ToString();
        }

        public string GenerateDAOImplementation(JsonObject entityDefinition)
        {
            if (!entityDefinition.TryGetPropertyValue("entityName", out var entityNameNode))
            {
                return "// Error: entityName not found in definition.";
            }
            string entityName = entityNameNode.ToString();
            string primaryKeyName = DefaultPrimaryKeyName; 

            StringBuilder daoImplementationCode = new StringBuilder();
            daoImplementationCode.AppendLine("using System;");
            daoImplementationCode.AppendLine("using System.Collections.Generic;");
            daoImplementationCode.AppendLine("using System.Data.Entity.Validation;");
            daoImplementationCode.AppendLine("using System.Linq;");
            daoImplementationCode.AppendLine($"using {NamespaceDAL_DTO};");
            daoImplementationCode.AppendLine();
            daoImplementationCode.AppendLine($"namespace {NamespaceDAL_DAO}");
            daoImplementationCode.AppendLine("{");
            daoImplementationCode.AppendLine($"\tpublic class {entityName}DAO : StockContext, I{entityName}DAO // Assuming StockContext is your DbContext");
            daoImplementationCode.AppendLine("\t{");
            daoImplementationCode.AppendLine("\t\t#region Database Operations");

            // --- Insert ---
            daoImplementationCode.AppendLine();
            daoImplementationCode.AppendLine($"\t\tpublic virtual bool Insert({entityName.ToUpper()} entity) // Assuming Entity Framework entity name is uppercase");
            daoImplementationCode.AppendLine("\t\t{");
            daoImplementationCode.AppendLine("\t\t\ttry");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\tDbContext.{entityName.ToUpper()}.Add(entity);");
            daoImplementationCode.AppendLine("\t\t\t\treturn DbContext.SaveChanges() > 0;");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (DbEntityValidationException ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine("\t\t\t\tvar errorMessages = ex.EntityValidationErrors");
            daoImplementationCode.AppendLine("\t\t\t\t\t.SelectMany(e => e.ValidationErrors)");
            daoImplementationCode.AppendLine("\t\t\t\t\t.Select(e => $\"{e.PropertyName}: {e.ErrorMessage}\");");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception($\"{entityName} insertion failed. Validation errors:\\n{{string.Join(\"\\n\", errorMessages)}}\");");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (Exception ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception(\"{entityName} insertion failed\", ex);");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t}");

            // --- Update ---
            daoImplementationCode.AppendLine();
            daoImplementationCode.AppendLine($"\t\tpublic virtual bool Update({entityName.ToUpper()} entity)");
            daoImplementationCode.AppendLine("\t\t{");
            daoImplementationCode.AppendLine("\t\t\ttry");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\t// Retrieve the existing {entityName.ToLower()} from the database.");
            daoImplementationCode.AppendLine($"\t\t\t\tvar existing{entityName} = DbContext.{entityName.ToUpper()}.First(x => x.{primaryKeyName} == entity.{primaryKeyName}); // Assuming ID is the primary key");
            daoImplementationCode.AppendLine();
            daoImplementationCode.AppendLine("\t\t\t\t// Update properties - Customize this based on your entity properties");
            if (entityDefinition.TryGetPropertyValue("properties", out var updatePropertiesNode) && updatePropertiesNode is JsonArray updateProperties)
            {
                foreach (var propertyNode in updateProperties)
                {
                    if (propertyNode is JsonObject property &&
                        property.TryGetPropertyValue("name", out var propertyNameNode))
                    {
                        string propertyName = propertyNameNode.ToString();
                        if (!propertyName.Equals(primaryKeyName, StringComparison.OrdinalIgnoreCase)) 
                        {
                            daoImplementationCode.AppendLine($"\t\t\t\texisting{entityName}.{propertyName} = entity.{propertyName};");
                        }
                    }
                }
            }
            daoImplementationCode.AppendLine("\t\t\t\treturn DbContext.SaveChanges() > 0;");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (DbEntityValidationException ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine("\t\t\t\tvar errorMessages = ex.EntityValidationErrors");
            daoImplementationCode.AppendLine("\t\t\t\t\t.SelectMany(e => e.ValidationErrors)");
            daoImplementationCode.AppendLine("\t\t\t\t\t.Select(e => $\"{e.PropertyName}: {e.ErrorMessage}\");");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception($\"{entityName} update failed. Validation errors:\\n{{string.Join(\"\\n\", errorMessages)}}\");");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (Exception ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception(\"{entityName} update failed\", ex);");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t}");

            // --- Delete ---
            daoImplementationCode.AppendLine();
            daoImplementationCode.AppendLine($"\t\tpublic virtual bool Delete({entityName.ToUpper()} entity)");
            daoImplementationCode.AppendLine("\t\t{");
            daoImplementationCode.AppendLine("\t\t\ttry");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\tvar existing{entityName} = DbContext.{entityName.ToUpper()}.FirstOrDefault(x => x.{primaryKeyName} == entity.{primaryKeyName}); // Assuming ID is primary key");
            daoImplementationCode.AppendLine($"\t\t\t\tif (existing{entityName} != null)");
            daoImplementationCode.AppendLine("\t\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\t\tDbContext.{entityName.ToUpper()}.Remove(existing{entityName}); // Hard delete - modify for soft delete if needed");
            daoImplementationCode.AppendLine("\t\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\t\treturn DbContext.SaveChanges() > 0;");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (DbEntityValidationException ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine("\t\t\t\tvar errorMessages = ex.EntityValidationErrors");
            daoImplementationCode.AppendLine("\t\t\t\t\t.SelectMany(e => e.ValidationErrors)");
            daoImplementationCode.AppendLine("\t\t\t\t\t.Select(e => $\"{e.PropertyName}: {e.ErrorMessage}\");");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception($\"{entityName} deletion failed. Validation errors:\\n{{string.Join(\"\\n\", errorMessages)}}\");");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (Exception ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception(\"{entityName} deletion failed\", ex);");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t}");


            // --- Select List (for Grid/Lists) ---
            daoImplementationCode.AppendLine();
            daoImplementationCode.AppendLine($"\t\tpublic virtual List<{entityName}DetailDTO> Select()");
            daoImplementationCode.AppendLine("\t\t{");
            daoImplementationCode.AppendLine("\t\t\ttry");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\treturn (from {entityName.ToLower()} in DbContext.{entityName.ToUpper()}");
            daoImplementationCode.AppendLine($"\t\t\t\t\tselect new {entityName}DetailDTO");
            daoImplementationCode.AppendLine("\t\t\t\t\t{");
            if (entityDefinition.TryGetPropertyValue("properties", out var selectPropertiesNode) && selectPropertiesNode is JsonArray selectProperties)
            {
                foreach (var propertyNode in selectProperties)
                {
                    if (propertyNode is JsonObject property &&
                        property.TryGetPropertyValue("name", out var propertyNameNode))
                    {
                        string propertyName = propertyNameNode.ToString();
                        daoImplementationCode.AppendLine($"\t\t\t\t\t\t{propertyName} = {entityName.ToLower()}.{propertyName},");
                    }
                }
            }
            // Remove trailing comma safely
            if (daoImplementationCode.ToString().EndsWith(",\r\n"))
            {
                daoImplementationCode.Length -= 3; 
            }
            daoImplementationCode.AppendLine("\t\t\t\t\t}).ToList();");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (DbEntityValidationException ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine("\t\t\t\tvar errorMessages = ex.EntityValidationErrors");
            daoImplementationCode.AppendLine("\t\t\t\t\t.SelectMany(e => e.ValidationErrors)");
            daoImplementationCode.AppendLine("\t\t\t\t\t.Select(e => $\"{e.PropertyName}: {e.ErrorMessage}\");");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception($\"{entityName} retrieval failed. Validation errors:\\n{{string.Join(\"\\n\", errorMessages)}}\");");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (Exception ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception(\"{entityName} retrieval failed\", ex);");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t}");

            // --- Select Single (by ID) ---
            daoImplementationCode.AppendLine();
            daoImplementationCode.AppendLine($"\t\tpublic virtual {entityName}DetailDTO Select(int ID) // Assuming ID is int and primary key");
            daoImplementationCode.AppendLine("\t\t{");
            daoImplementationCode.AppendLine("\t\t\ttry");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\treturn (from {entityName.ToLower()} in DbContext.{entityName.ToUpper()}.Where(x => x.{primaryKeyName} == ID)");
            daoImplementationCode.AppendLine($"\t\t\t\t\tselect new {entityName}DetailDTO");
            daoImplementationCode.AppendLine("\t\t\t\t\t{");
            if (entityDefinition.TryGetPropertyValue("properties", out var singleSelectPropertiesNode) && singleSelectPropertiesNode is JsonArray singleSelectProperties)
            {
                foreach (var propertyNode in singleSelectProperties)
                {
                    if (propertyNode is JsonObject property &&
                        property.TryGetPropertyValue("name", out var propertyNameNode))
                    {
                        string propertyName = propertyNameNode.ToString();
                        daoImplementationCode.AppendLine($"\t\t\t\t\t\t{propertyName} = {entityName.ToLower()}.{propertyName},");
                    }
                }
            }
            // Remove trailing comma safely
            if (daoImplementationCode.ToString().EndsWith(",\r\n"))
            {
                daoImplementationCode.Length -= 3; 
            }
            daoImplementationCode.AppendLine("\t\t\t\t\t}).FirstOrDefault();");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (DbEntityValidationException ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine("\t\t\t\tvar errorMessages = ex.EntityValidationErrors");
            daoImplementationCode.AppendLine("\t\t\t\t\t.SelectMany(e => e.ValidationErrors)");
            daoImplementationCode.AppendLine("\t\t\t\t\t.Select(e => $\"{e.PropertyName}: {e.ErrorMessage}\");");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception($\"{entityName} retrieval by ID failed. Validation errors:\\n{{string.Join(\"\\n\", errorMessages)}}\");");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t\tcatch (Exception ex)");
            daoImplementationCode.AppendLine("\t\t\t{");
            daoImplementationCode.AppendLine($"\t\t\t\tthrow new Exception(\"{entityName} retrieval by ID failed\", ex);");
            daoImplementationCode.AppendLine("\t\t\t}");
            daoImplementationCode.AppendLine("\t\t}");


            daoImplementationCode.AppendLine("\t\t#endregion");
            daoImplementationCode.AppendLine("\t}");
            daoImplementationCode.AppendLine("}");
            return daoImplementationCode.ToString();
        }

        public string GenerateBLLInterface(JsonObject entityDefinition)
        {
            if (!entityDefinition.TryGetPropertyValue("entityName", out var entityNameNode))
            {
                return "// Error: entityName not found in definition.";
            }
            string entityName = entityNameNode.ToString();

            StringBuilder bllInterfaceCode = new StringBuilder();
            bllInterfaceCode.AppendLine("using System.Collections.Generic;");
            bllInterfaceCode.AppendLine($"using {NamespaceDAL_DTO};");
            bllInterfaceCode.AppendLine();
            bllInterfaceCode.AppendLine($"namespace {NamespaceBLL}");
            bllInterfaceCode.AppendLine("{");
            bllInterfaceCode.AppendLine($"\tpublic interface I{entityName}BLL : IBLL<{entityName}DetailDTO, {entityName}DTO>"); 
            bllInterfaceCode.AppendLine("\t{");
            bllInterfaceCode.AppendLine("\t\t// Add specific BLL methods if needed beyond standard CRUD");
            bllInterfaceCode.AppendLine("\t}");
            bllInterfaceCode.AppendLine("}");
            return bllInterfaceCode.ToString();
        }

        public string GenerateBLLImplementation(JsonObject entityDefinition)
        {
            if (!entityDefinition.TryGetPropertyValue("entityName", out var entityNameNode))
            {
                return "// Error: entityName not found in definition.";
            }
            string entityName = entityNameNode.ToString();
            string entityNameLower = entityName.ToLowerInvariant();
            string entityNameUpper = entityName.ToUpperInvariant();
            string primaryKeyName = DefaultPrimaryKeyName;

            StringBuilder bllImplementationCode = new StringBuilder();
            bllImplementationCode.AppendLine("using System.Collections.Generic;");
            bllImplementationCode.AppendLine($"using {NamespaceDAL_DTO};");
            bllImplementationCode.AppendLine($"using {NamespaceDAL_DAO};");
            bllImplementationCode.AppendLine();
            bllImplementationCode.AppendLine($"namespace {NamespaceBLL}");
            bllImplementationCode.AppendLine("{");
            bllImplementationCode.AppendLine($"\tpublic class {entityName}BLL : I{entityName}BLL");
            bllImplementationCode.AppendLine("\t{");
            bllImplementationCode.AppendLine("\t\t#region Data Access Dependencies");
            bllImplementationCode.AppendLine($"\t\tprivate readonly I{entityName}DAO _{entityNameLower}Dao = new {entityName}DAO();"); 
           
            bllImplementationCode.AppendLine("\t\t#endregion");
            bllImplementationCode.AppendLine();
            bllImplementationCode.AppendLine("\t\t#region CRUD Operations");

            // --- Insert ---
            bllImplementationCode.AppendLine();
            bllImplementationCode.AppendLine($"\t\tpublic bool Insert({entityName}DetailDTO entity)");
            bllImplementationCode.AppendLine("\t\t{");
            bllImplementationCode.AppendLine($"\t\t\tvar {entityNameLower} = new {entityNameUpper}");
            bllImplementationCode.AppendLine("\t\t\t{");
            if (entityDefinition.TryGetPropertyValue("properties", out var insertPropertiesNode) && insertPropertiesNode is JsonArray insertProperties)
            {
                foreach (var propertyNode in insertProperties)
                {
                    if (propertyNode is JsonObject property &&
                        property.TryGetPropertyValue("name", out var propertyNameNode))
                    {
                        string propertyName = propertyNameNode.ToString();
                        if (!propertyName.Equals(primaryKeyName, StringComparison.OrdinalIgnoreCase)) 
                        {
                            bllImplementationCode.AppendLine($"\t\t\t\t{propertyName} = entity.{propertyName},");
                        }
                    }
                }
            }
            // Remove trailing comma safely
            if (bllImplementationCode.ToString().EndsWith(",\r\n"))
            {
                bllImplementationCode.Length -= 3; 
            }
            bllImplementationCode.AppendLine("\t\t\t};"); 
            bllImplementationCode.AppendLine($"\t\t\treturn _{entityNameLower}Dao.Insert({entityNameLower});");
            bllImplementationCode.AppendLine("\t\t}");


            // --- Update ---
            bllImplementationCode.AppendLine();
            bllImplementationCode.AppendLine($"\t\tpublic bool Update({entityName}DetailDTO entity)");
            bllImplementationCode.AppendLine("\t\t{");
            bllImplementationCode.AppendLine($"\t\t\tvar {entityNameLower} = new {entityNameUpper}");
            bllImplementationCode.AppendLine("\t\t\t{");
            bllImplementationCode.AppendLine($"\t\t\t\t{primaryKeyName} = entity.{primaryKeyName}, // Assuming DetailDTO has ID");
            if (entityDefinition.TryGetPropertyValue("properties", out var updatePropertiesNode) && updatePropertiesNode is JsonArray updateProperties)
            {
                foreach (var propertyNode in updateProperties)
                {
                    if (propertyNode is JsonObject property &&
                        property.TryGetPropertyValue("name", out var propertyNameNode))
                    {
                        string propertyName = propertyNameNode.ToString();
                        bllImplementationCode.AppendLine($"\t\t\t\t{propertyName} = entity.{propertyName},");
                    }
                }
            }
            // Remove trailing comma safely
            if (bllImplementationCode.ToString().EndsWith(",\r\n"))
            {
                bllImplementationCode.Length -= 3; 
            }
            bllImplementationCode.AppendLine("\t\t\t};");
            bllImplementationCode.AppendLine($"\t\t\treturn _{entityNameLower}Dao.Update({entityNameLower});");
            bllImplementationCode.AppendLine("\t\t}");

            // --- Delete ---
            bllImplementationCode.AppendLine();
            bllImplementationCode.AppendLine($"\t\tpublic bool Delete({entityName}DetailDTO entity)");
            bllImplementationCode.AppendLine("\t\t{");
            bllImplementationCode.AppendLine($"\t\t\tvar {entityNameLower} = new {entityNameUpper} {{ {primaryKeyName} = entity.{primaryKeyName} }}; // Assuming DetailDTO has ID");
            bllImplementationCode.AppendLine($"\t\t\treturn _{entityNameLower}Dao.Delete({entityNameLower});");
            bllImplementationCode.AppendLine("\t\t}");

            // --- Select (List) ---
            bllImplementationCode.AppendLine();
            bllImplementationCode.AppendLine($"\t\tpublic {entityName}DTO Select()");
            bllImplementationCode.AppendLine("\t\t{");
            bllImplementationCode.AppendLine($"\t\t\treturn new {entityName}DTO");
            bllImplementationCode.AppendLine("\t\t\t{");
            bllImplementationCode.AppendLine($"\t\t\t\t{entityName}List = _{entityNameLower}Dao.Select()");
            
            bllImplementationCode.AppendLine("\t\t\t};");
            bllImplementationCode.AppendLine("\t\t}");


            // --- GetBack (Example - Customize if needed, remove if not applicable) ---
            bllImplementationCode.AppendLine();
            bllImplementationCode.AppendLine($"\t\tpublic bool GetBack({entityName}DetailDTO entity)");
            bllImplementationCode.AppendLine("\t\t{");
            bllImplementationCode.AppendLine($"\t\t\t// Example - Customize GetBack logic in DAO if needed, this is a simple pass-through");
            bllImplementationCode.AppendLine($"\t\t\treturn _{entityNameLower}Dao.GetBack(entity.{primaryKeyName}); // Assuming DetailDTO has ID and GetBack takes ID");
            bllImplementationCode.AppendLine("\t\t}");


            bllImplementationCode.AppendLine("\t\t#endregion");
            bllImplementationCode.AppendLine("\t}");
            bllImplementationCode.AppendLine("}");
            return bllImplementationCode.ToString();
        }


        public void GenerateCRUDClasses(string entityDefinitionJson, out string dtoCode, out string daoInterfaceCode, out string daoImplementationCode, out string bllInterfaceCode, out string bllImplementationCode)
        {
            dtoCode = "";
            daoInterfaceCode = "";
            daoImplementationCode = "";
            bllInterfaceCode = "";
            bllImplementationCode = "";

            try
            {
                var entityDefinition = JsonNode.Parse(entityDefinitionJson)?.AsObject();
                if (entityDefinition == null)
                {
                    dtoCode = daoInterfaceCode = daoImplementationCode = bllInterfaceCode = bllImplementationCode = "// Error: Invalid JSON definition.";
                    return;
                }

                dtoCode = GenerateDTO(entityDefinition);
                daoInterfaceCode = GenerateDAOInterface(entityDefinition);
                daoImplementationCode = GenerateDAOImplementation(entityDefinition);
                bllInterfaceCode = GenerateBLLInterface(entityDefinition);
                bllImplementationCode = GenerateBLLImplementation(entityDefinition);
            }
            catch (JsonException ex)
            {
                
                dtoCode = daoInterfaceCode = daoImplementationCode = bllInterfaceCode = bllImplementationCode = $"// Error parsing JSON: {ex.Message}";
            }
            catch (Exception ex)
            {
                
                dtoCode = daoInterfaceCode = daoImplementationCode = bllInterfaceCode = bllImplementationCode = $"// Error generating code: {ex.Message}";
            }
        }
    }
}