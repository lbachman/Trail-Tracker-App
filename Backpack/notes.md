# Install  NuGet Packages

```Install-Package Microsoft.EntityFrameworkCore```
```Install-Package Microsoft.EntityFrameworkCore.Tools```
```Install-Package Microsoft.EntityFrameworkCore.Design```
```Install-Package Microsoft.EntityFrameworkCore.Sqlite```


# Create Context
- remember that the scaffolding command doesnt work in a maui proj, you will need to create another project and copy the created classes over
```dotnet ef dbcontext scaffold "Data Source=BackpackSQLite.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Entities --context BackpackAppContext --context-dir Contexts --force```


