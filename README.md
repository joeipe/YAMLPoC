# YAMLPoC

## Topic covered
 
- YAML
- Azure Vault
- Devops Library
- Logging (Azure Log Analytics)
> - Query commands 
> - `some_CL | project LogMessage_s | where LogMessage_s contains "somevalue"`
> - `some_CL | project LogMessage_s, LogProperties_Scope_s, Timestamp_t | where LogProperties_Scope_s has "1234" | order by Timestamp_t desc`
